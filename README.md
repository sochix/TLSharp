TLSharp
-------------------------------

<a href="https://www.paypal.me/IPirozhenko" title="Support project"><img src="https://img.shields.io/badge/Support%20project-paypal-brightgreen.svg"></a>
[![Join the chat at https://gitter.im/TLSharp/Lobby](https://badges.gitter.im/TLSharp/Lobby.svg)](https://gitter.im/TLSharp/Lobby?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)
[![Build status](https://ci.appveyor.com/api/projects/status/95rl618ch5c4h2fa?svg=true)](https://ci.appveyor.com/project/sochix/tlsharp)
[![NuGet version](https://badge.fury.io/nu/TLSharp.svg)](https://badge.fury.io/nu/TLSharp)
<a href="https://github.com/sochix/telegram-tools"><img src=https://img.shields.io/badge/Telegram%20Tools-1.0.0-blue.svg /></a>

_Unofficial_ Telegram (http://telegram.org) client library implemented in C#. Please refer to (https://github.com/sochix/TLSharp) for the original version and further documentation.

# Sample code
```csharp
using System;
using System.Threading.Tasks;
using TeleSharp.TL;
using TLSharp.Core;
using System.Linq;
using TeleSharp.TL.Messages;
using System.Collections.Generic;

namespace TLSharpPOC
{
    class MainClass
    {
        const int APIId = 0;
        const string APIHash = "???";
        const string phone = "???";
        public static void Main(string[] args)
        {
            new MainClass().MainAsync(args).Wait();
        }

        private readonly Dictionary<int, TLUser> ContactList;

        private async Task MainAsync(string[] args)
        {
            TelegramClient client = null;
            try
            {
	    	// -- if necessary, IP can be changed so the client can connect to the test network.
                Session session = null;
                //    new Session(new FileSessionStore(), "session")
                //{
                //    ServerAddress = "149.154.175.10",
                //    Port = 443
                //};
                //Console.WriteLine($"{session.ServerAddress}:{session.Port} {phone}");
                client = new TelegramClient(APIId, APIHash, session);
		// subscribe an event to receive live messages
                client.Updates += Client_Updates;
                await client.ConnectAsync();
                Console.WriteLine($"Authorised: {client.IsUserAuthorized()}");
                TLUser user = null;
		// -- If the user has already authenticated, this step will prevent account from being blocked as it
		// -- reuses the data from last authorisation.
                if (client.IsUserAuthorized())
                    user = client.Session.TLUser;
                else
                {
                    var registered = await client.IsPhoneRegisteredAsync(phone);
                    var hash = await client.SendCodeRequestAsync(phone);
                    Console.Write("Code: ");
                    var code = Console.ReadLine();
                    if (!registered)
                    {
                        Console.WriteLine($"Sign up {phone}");
                        user = await client.SignUpAsync(phone, hash, code, "First", "Last");
                    }
                    Console.WriteLine($"Sign in {phone}");
                    user = await client.MakeAuthAsync(phone, hash, code);
                }

                var contacts = await client.GetContactsAsync();
                Console.WriteLine("Contacts:");
                foreach (var contact in contacts.Users.OfType<TLUser>())
                {
                    var contactUser = contact as TLUser;
                    Console.WriteLine($"\t{contact.Id} {contact.Phone} {contact.FirstName} {contact.LastName}");
                }


                var dialogs = (TLDialogs) await client.GetUserDialogsAsync();
                Console.WriteLine("Channels: ");
                foreach (var channelObj in dialogs.Chats.OfType<TLChannel>())
                {
                    var channel = channelObj as TLChannel;
                    Console.WriteLine($"\tChat: {channel.Title}");
                }

                Console.WriteLine("Groups:");
                TLChat chat = null;
                foreach (var chatObj in dialogs.Chats.OfType<TLChat>())
                {
                    chat = chatObj as TLChat;
                    Console.WriteLine($"Chat name: {chat.Title}");
                    var request = new TLRequestGetFullChat() { ChatId = chat.Id };
                    var fullChat = await client.SendRequestAsync<TeleSharp.TL.Messages.TLChatFull>(request);

                    var participants = (fullChat.FullChat as TeleSharp.TL.TLChatFull).Participants as TLChatParticipants;
                    foreach (var p in participants.Participants)
                    {
                        if (p is TLChatParticipant)
                        {
                            var participant = p as TLChatParticipant;
                            Console.WriteLine($"\t{participant.UserId}");
                        }
                        else if (p is TLChatParticipantAdmin)
                        {
                            var participant = p as TLChatParticipantAdmin;
                            Console.WriteLine($"\t{participant.UserId}**");
                        }
                        else if (p is TLChatParticipantCreator)
                        {
                            var participant = p as TLChatParticipantCreator;
                            Console.WriteLine($"\t{participant.UserId}**");
                        }
                    }

                    var peer = new TLInputPeerChat() { ChatId = chat.Id };
                    var m = await client.GetHistoryAsync(peer, 0, 0, 0);
                    Console.WriteLine(m);
                    if (m is TLMessages)
                    {
                        var messages = m as TLMessages;


                        foreach (var message in messages.Messages)
                        {
                            if (message is TLMessage)
                            {
                                var m1 = message as TLMessage;
                                Console.WriteLine($"\t\t{m1.Id} {m1.Message}");
                            }
                            else if (message is TLMessageService)
                            {
                                var m1 = message as TLMessageService;
                                Console.WriteLine($"\t\t{m1.Id} {m1.Action}");
                            }
                        }
                    }
                    else if (m is TLMessagesSlice)
                    {
                        bool done = false;
                        int total = 0;
                        while (!done)
                        {
                            var messages = m as TLMessagesSlice;

                            foreach (var m1 in messages.Messages)
                            {
                                if (m1 is TLMessage)
                                {
                                    var message = m1 as TLMessage;
                                    Console.WriteLine($"\t\t{message.Id} {message.Message}");
                                    ++total;
                                }
                                else if (m1 is TLMessageService)
                                {
                                    var message = m1 as TLMessageService;
                                    Console.WriteLine($"\t\t{message.Id} {message.Action}");
                                    ++total;
                                    done = message.Action is TLMessageActionChatCreate;
                                }
                            }
                            m = await client.GetHistoryAsync(peer, total, 0, 0);
                        }
                    }
                }
		
		// -- Wait in a loop to handle incoming updates. No need to poll.
                for (;;)
                {
                    await client.WaitEventAsync();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void Client_Updates(TelegramClient client, TLAbsUpdates updates)
        {
            Console.WriteLine($"Got update: {updates}");
            if (updates is TLUpdateShort)
            {
                var updateShort = updates as TLUpdateShort;
                Console.WriteLine($"Short: {updateShort.Update}");
                if (updateShort.Update is TLUpdateUserStatus)
                {
                    var status = updateShort.Update as TLUpdateUserStatus;
                    Console.WriteLine($"User {status.UserId} is {status.Status}");
                    if (status.Status is TLUserStatusOnline)
                    {
                        try
                        {
                            var peer = new TLInputPeerUser() { UserId = status.UserId };
                            client.SendMessageAsync(peer, "Você está online.").Wait();
                        } catch {}
                    }
                }
            }
            else if (updates is TLUpdateShortMessage)
            {
                var message = updates as TLUpdateShortMessage;
                Console.WriteLine($"Message: {message.Message}");
                MarkMessageRead(client, new TLInputPeerUser() { UserId = message.UserId }, message.Id);
            }
            else if (updates is TLUpdateShortChatMessage)
            {
                var message = updates as TLUpdateShortChatMessage;
                Console.WriteLine($"Chat Message: {message.Message}");
                MarkMessageRead(client, new TLInputPeerChat() { ChatId = message.ChatId }, message.Id);
            }
            else if (updates is TLUpdates)
            {
                var allUpdates = updates as TLUpdates;
                foreach (var update in allUpdates.Updates)
                {
                    Console.WriteLine($"\t{update}");
                    if (update is TLUpdateNewChannelMessage)
                    {
                        var metaMessage = update as TLUpdateNewChannelMessage;
                        var message = metaMessage.Message as TLMessage;
                        Console.WriteLine($"Channel message: {message.Message}");
                        var channel = allUpdates.Chats[0] as TLChannel;
                        MarkMessageRead(client, 
                                        new TLInputPeerChannel() { ChannelId = channel.Id, AccessHash = channel.AccessHash.Value }, 
                                        message.Id );
                    }
                }

                foreach(var user in allUpdates.Users)
                {
                    Console.WriteLine($"{user}");
                }

                foreach (var chat in allUpdates.Chats)
                {
                    Console.WriteLine($"{chat}");
                }
            }
        }

        private void MarkMessageRead(TelegramClient client, TLAbsInputPeer peer, int id)
        {
	    // An exception happens here but it's not fatal.
            try
            {
                var request = new TLRequestReadHistory();
                request.MaxId = id;
                request.Peer = peer;
                client.SendRequestAsync<bool>(request).Wait();
            }
            catch {}

        }
    }
}

```


# License

**Please, provide link to an author when you using library**

The MIT License

Copyright (c) 2015 Ilya Pirozhenko http://www.sochix.ru/

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
