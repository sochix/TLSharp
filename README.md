# (LIBRARY IS ARCHIVED AND NOT MAINTAINED) 

TLSharp
-------------------------------

🚩🚩🚩 **WARNING** 🚩🚩🚩🚩 
## LIBRARY IS NO LONGER MAINTAINED    

As a workaround you can use -> [WTelegramClient](https://github.com/wiz0u/WTelegramClient)

WTelegramClient is another C#/.NET open-source library for accessing Telegram Client API and is:
- offering up-to-date API (latest layer)
- safer (latest MTProto v2 implementation and many security checks)
- feature-complete (handling of updates, multiple-DC connections)
- easy-to-use (API calls are direct methods with fully documented parameters in VS)
- designed for .NET 5.0+, but also available for .NET Standard 2.0 (.NET Framework 4.6.1+ & .NET Core 2.0+)

-------------------------------

![Build status](https://github.com/sochix/TLSharp/workflows/CI/badge.svg?branch=master&event=push)
[![NuGet version](https://badge.fury.io/nu/TLSharp.svg)](https://badge.fury.io/nu/TLSharp)

_Unofficial_ Telegram (http://telegram.org) client library implemented in C#.

It's a perfect fit for any developer who would like to send data directly to Telegram users or write own custom Telegram client.

:star2: If you :heart: library, please star it! :star2:

# Table of contents

- [How do I add this to my project?](#how-do-i-add-this-to-my-project)
- [Starter Guide](#starter-guide)
  - [Quick configuration](#quick-configuration)
  - [First requests](#first-requests)
  - [Working with files](#working-with-files)
- [Available Methods](#available-methods)
- [Contributors](#contributors)
- [FAQ](#faq)
- [Donations](#donations)
- [Support](#support)
- [License](#license)

# How do I add this to my project?

Install via NuGet

```
	> Install-Package TLSharp
```

or build from source

1. Clone TLSharp from GitHub
1. Compile source with VS2015 or MonoDevelop
1. Add reference to ```TLSharp.Core.dll``` to your awesome project.

# Starter Guide

## Quick Configuration
Telegram API isn't that easy to start. You need to do some configuration first.

1. Create a [developer account](https://my.telegram.org/) in Telegram. 
1. Goto [API development tools](https://my.telegram.org/apps) and copy **API_ID** and **API_HASH** from your account. You'll need it later.

## First requests
To start work, create an instance of TelegramClient and establish connection

```csharp 
   var client = new TelegramClient(apiId, apiHash);
   await client.ConnectAsync();
```
Now you can work with Telegram API, but ->
> Only a small portion of the API methods are available to unauthorized users. ([full description](https://core.telegram.org/api/auth)) 

For authentication you need to run following code
```csharp
  var hash = await client.SendCodeRequestAsync("<user_number>");
  var code = "<code_from_telegram>"; // you can change code in debugger

  var user = await client.MakeAuthAsync("<user_number>", hash, code);
``` 

Full code you can see at [AuthUser test](https://github.com/sochix/TLSharp/blob/master/TLSharp.Tests/TLSharpTests.cs#L70)

When user is authenticated, TLSharp creates special file called _session.dat_. In this file TLSharp store all information needed for user session. So you need to authenticate user every time the _session.dat_ file is corrupted or removed.

You can call any method on authenticated user. For example, let's send message to a friend by his phone number:

```csharp
  //get available contacts
  var result = await client.GetContactsAsync();

  //find recipient in contacts
  var user = result.Users
	  .Where(x => x.GetType() == typeof (TLUser))
	  .Cast<TLUser>()
	  .FirstOrDefault(x => x.Phone == "<recipient_phone>");
	
  //send message
  await client.SendMessageAsync(new TLInputPeerUser() {UserId = user.Id}, "OUR_MESSAGE");
```

Full code you can see at [SendMessage test](https://github.com/sochix/TLSharp/blob/master/TLSharp.Tests/TLSharpTests.cs#L87)

To send message to channel you could use the following code:
```csharp
  //get user dialogs
  var dialogs = (TLDialogsSlice) await client.GetUserDialogsAsync();

  //find channel by title
  var chat = dialogs.Chats
    .Where(c => c.GetType() == typeof(TLChannel))
    .Cast<TLChannel>()
    .FirstOrDefault(c => c.Title == "<channel_title>");

  //send message
  await client.SendMessageAsync(new TLInputPeerChannel() { ChannelId = chat.Id, AccessHash = chat.AccessHash.Value }, "OUR_MESSAGE");
```
Full code you can see at [SendMessageToChannel test](https://github.com/sochix/TLSharp/blob/master/TLSharp.Tests/TLSharpTests.cs#L107)
## Working with files
Telegram separate files to two categories -> big file and small file. File is Big if its size more than 10 Mb. TLSharp tries to hide this complexity from you, thats why we provide one method to upload files **UploadFile**.

```csharp
	var fileResult = await client.UploadFile("cat.jpg", new StreamReader("data/cat.jpg"));
```

TLSharp provides two wrappers for sending photo and document

```csharp
	await client.SendUploadedPhoto(new TLInputPeerUser() { UserId = user.Id }, fileResult, "kitty");
	await client.SendUploadedDocument(
                new TLInputPeerUser() { UserId = user.Id },
                fileResult,
                "some zips", //caption
                "application/zip", //mime-type
                new TLVector<TLAbsDocumentAttribute>()); //document attributes, such as file name
```
Full code you can see at [SendPhotoToContactTest](https://github.com/sochix/TLSharp/blob/master/TLSharp.Tests/TLSharpTests.cs#L125) and [SendBigFileToContactTest](https://github.com/sochix/TLSharp/blob/master/TLSharp.Tests/TLSharpTests.cs#L143)

To download file you should call **GetFile** method
```csharp
	await client.GetFile(
                new TLInputDocumentFileLocation()
                {
                    AccessHash = document.AccessHash,
                    Id = document.Id,
                    Version = document.Version
                },
                document.Size); //size of fileChunk you want to retrieve
```

Full code you can see at [DownloadFileFromContactTest](https://github.com/sochix/TLSharp/blob/master/TLSharp.Tests/TLSharpTests.cs#L167)

# Events Sample code
```csharp
using System;
using System.Threading.Tasks;
using TeleSharp.TL;
using TLSharp.Core;
using System.Linq;
using TeleSharp.TL.Messages;

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

        private async Task MainAsync(string[] args)
        {
            TelegramClient client = null;

            client = new TelegramClient(APIId, APIHash);
            // subscribe an event to receive live messages
            client.Updates += ClientUpdates;
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
                    if (p is TLChatParticipant chatParticipant)
                    {
                        Console.WriteLine($"\t{chatParticipant.UserId}");
                    }
                    else if (p is TLChatParticipantAdmin chatParticipantAdmin)
                    {
                        Console.WriteLine($"\t{chatParticipantAdmin.UserId}**");
                    }
                    else if (p is TLChatParticipantCreator chatParticipantCreator)
                    {
                        Console.WriteLine($"\t{chatParticipantCreator.UserId}**");
                    }
                }

                var peer = new TLInputPeerChat() { ChatId = chat.Id };
                var msg = await client.GetHistoryAsync(peer, 0, 0, 0);
                Console.WriteLine(msg);
                if (msg is TLMessages messages)
		{
                    foreach (var message in messages.Messages)
                    {
                        if (message is TLMessage m1)
                        {
                            Console.WriteLine($"\t\t{m1.Id} {m1.Message}");
                        }
                        else if (message is TLMessageService msgService)
                        {
                            Console.WriteLine($"\t\t{msgService.Id} {msgService.Action}");
                        }
                    }
                }
		else if (msg is TLMessagesSlice messagesSlice)
                {
                    bool done = false;
                    int total = 0;
                    while (!done)
                    {
                        foreach (var m1 in messagesSlice.Messages)
                        {
                            if (m1 is TLMessage message)
                            {
                                Console.WriteLine($"\t\t{message.Id} {message.Message}");
                                ++total;
                            }
                            else if (m1 is TLMessageService messageService)
                            {
                                Console.WriteLine($"\t\t{messageService.Id} {messageService.Action}");
                                ++total;
                                done = messageService.Action is TLMessageActionChatCreate;
                            }
                        }
                        msg = await client.GetHistoryAsync(peer, total, 0, 0);
                    }
                }
            }

            // -- Wait in a loop to handle incoming updates. No need to poll.
            while(true)
            {
                await client.WaitEventAsync(TimeSpan.FromSeconds(1));
            }
        }

        private void ClientUpdates(TelegramClient client, TLAbsUpdates updates)
        {
            Console.WriteLine($"Got update: {updates}");
            if (updates is TLUpdateShort updateShort)
            {
                Console.WriteLine($"Short: {updateShort.Update}");
                if (updateShort.Update is TLUpdateUserStatus status)
                {
                    Console.WriteLine($"User {status.UserId} is {status.Status}");
                    if (status.Status is TLUserStatusOnline)
                    {
                        var peer = new TLInputPeerUser() { UserId = status.UserId };
                        client.SendMessageAsync(peer, "Você está online.").Wait();
                    }
                }
            }
            else if (updates is TLUpdateShortMessage message)
            {
                Console.WriteLine($"Message: {message.Message}");
                MarkMessageRead(client, new TLInputPeerUser() { UserId = message.UserId }, message.Id);
            }
            else if (updates is TLUpdateShortChatMessage shortChatMessage)
            {
                Console.WriteLine($"Chat Message: {shortChatMessage.Message}");
                MarkMessageRead(client, new TLInputPeerChat() { ChatId = shortChatMessage.ChatId }, shortChatMessage.Id);
            }
            else if (updates is TLUpdates allUpdates)
            {
                foreach (var update in allUpdates.Updates)
                {
                    Console.WriteLine($"\t{update}");
                    if (update is TLUpdateNewChannelMessage metaMessage)
                    {
                        var channelMsg = metaMessage.Message as TLMessage;
                        Console.WriteLine($"Channel message: {channelMsg.Message}");
                        var channel = allUpdates.Chats[0] as TLChannel;
                        MarkMessageRead(client,
                                        new TLInputPeerChannel() { ChannelId = channel.Id, AccessHash = channel.AccessHash.Value },
                                        channelMsg.Id);
                    }
                }

                foreach (var user in allUpdates.Users)
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
            try
            {
                var request = new TLRequestReadHistory();
                request.MaxId = id;
                request.Peer = peer;
                client.SendRequestAsync<bool>(request).Wait();
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine($"MarkMessageRead Error: {e.Message}");
            }
        }
    }
}
```

# Available Methods

For your convenience TLSharp have wrappers for several Telegram API methods. You could add your own, see details below.

1. IsPhoneRegisteredAsync
1. SendCodeRequestAsync
1. MakeAuthAsync
1. SignUpAsync
1. GetContactsAsync
1. SendMessageAsync
1. SendTypingAsync
1. GetUserDialogsAsync
1. SendUploadedPhoto
1. SendUploadedDocument
1. GetFile
1. UploadFile
1. SendPingAsync
1. GetHistoryAsync

**What if you can't find needed method at the list?**

Don't panic. You can call any method with help of `SendRequestAsync` function. For example, send user typing method: 

```csharp

  //Create request 
  var req = new TLRequestSetTyping()
  {
    Action = new TLSendMessageTypingAction(),
    Peer = new TLInputPeerUser() { UserId = user.Id }
  };

  //run request, and deserialize response to Boolean
  return await client.SendRequestAsync<Boolean>(req);
``` 

**Where you can find a list of requests and its params?**

The only way is [Telegram API docs](https://core.telegram.org/methods). Latest scheme in JSON format you can find [here](https://core.telegram.org/schema/json).

## What things can I Implement (Project Roadmap)?

### Latest Release

* [DONE] Add PHONE_MIGRATE handling
* [DONE] Add FILE_MIGRATE handling
* [DONE] Add NuGet package
* [DONE] Add wrappers for media uploading and downloading
* Add Updates handling ([WIP](https://github.com/sochix/TLSharp/pull/940))
* Store user session as JSON ([WIP](https://github.com/nblockchain/TgSharp/pull/18))
* Upgrade MTProto protocol version to 2.0 ([WIP](https://github.com/nblockchain/TgSharp/pull/23))
* SRP/2FA support ([WIP](https://github.com/nblockchain/TgSharp/pull/17))

# FAQ

#### What API layer is supported?
The latest layer supported by TLSharp is 66. If you need a higher layer, help us test the preview version of [TgSharp](https://github.com/nblockchain/TgSharp) (your feedback is welcome!)

#### I get a xxxMigrationException or a MIGRATE_X error!

TLSharp library should automatically handle these errors. If you see such errors, please open a new Github issue with the details (include a stacktrace, etc.).

#### I get an exception: System.IO.EndOfStreamException: Unable to read beyond the end of the stream. All test methos except that AuthenticationWorks and TestConnection return same error. I did every thing including setting api id and hash, and setting server address.-

You should create a Telegram session. See [configuration guide](#sending-messages-set-up)

#### Why do I get a FloodException/FLOOD_WAIT error?
After you get this, you cannot use Telegram's API for a while. You can know the time to wait by accessing the FloodException::TimeToWait property.

If this happens too often and/or the TimeToWait value is too long, there may be something odd going on. First and foremost, are you using TLSharp to manage more than one telegram account from the same host(server)? If yes, it's likely that you're hitting [Telegram restrictions](https://core.telegram.org/api/errors#420-flood). We recommend that you use TLSharp in a standalone-device app (so that each instance of your program only uses one telegram account), so for example a mobile app, not a web app.
If, on the other hand, you're completely sure that you found a bug in TLSharp about this, please open a Github issue.

#### Why does TLSharp lacks feature XXXX?

TLSharp only covers basic functionality of the Telegram protocol, you can be a contributor or a sponsor to speed-up developemnt of any more new features.

#### Where else to ask for help?
If you think you have found a bug in TLSharp, create a github issue. But if you just have questions about how to use TLSharp, use our gitter channel (https://gitter.im/TLSharp/Lobby) or our Telegram channel (https://t.me/joinchat/AgtDiBEqG1i-qPqttNFLbA).

**Attach following information**:

* Full problem description and exception message
* Stack-trace
* Your code that runs in to this exception

Without information listen above your issue will be closed. 

# Donations
Thanks for donations! It's highly appreciated. 
<a href="https://www.paypal.me/IPirozhenko" title="Support project"><img src="https://img.shields.io/badge/Support%20project-paypal-brightgreen.svg"></a>

List of donators:
* [mtbitcoin](https://github.com/mtbitcoin)

# Support
If you have troubles while using TLSharp, I can help you for an additional fee. 

My pricing is **219$/hour**. I accept PayPal. To request a paid support write me at Telegram @sochix, start your message with phrase [PAID SUPPORT].

# Contributors
* [Afshin Arani](http://aarani.ir) - TLGenerator, and a lot of other usefull things
* [knocte](https://github.com/knocte)

# License

**Please, provide link to an author when you using library**

The MIT License

Copyright (c) 2015 Ilya Pirozhenko

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
