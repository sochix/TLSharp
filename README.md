TgSharp
-------------------------------

![GithubCI Build status](https://github.com/nblockchain/TgSharp/workflows/CI/badge.svg?branch=master&event=push)
[![GitLabCI Linux build status badge](https://gitlab.com/nblockchain/TgSharp/badges/master/pipeline.svg)](https://gitlab.com/nblockchain/TgSharp/commits/master)
[![NuGet version](https://badge.fury.io/nu/TgSharp.svg)](https://badge.fury.io/nu/TgSharp)

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
	> Install-Package TgSharp
```

or build from source

1. Clone TgSharp from GitHub
1. Compile source with VS2015 or MonoDevelop
1. Add reference to ```TgSharp.Core.dll``` to your awesome project.

# Starter Guide

## Quick Configuration
Telegram API isn't that easy to start. You need to do some configuration first.

1. Create a [developer account](https://my.telegram.org/) in Telegram. 
1. Goto [API development tools](https://my.telegram.org/apps) and copy **API_ID** and **API_HASH** from your account. You'll need it later.


## First requests

To start work, create an instance of TelegramClient and establish connection:

```csharp 
    var client = new TelegramClient(apiId, apiHash);
    await client.ConnectAsync();
```

Now you can work with Telegram API, but ->

> Only a small portion of the API methods are available to unauthorized users. ([full description](https://core.telegram.org/api/auth)) 

For authentication you need to run following code:

```csharp
    var hash = await client.SendCodeRequestAsync("<user_number>");
    var code = "<code_from_telegram>"; // you can change code in debugger

    var user = await client.MakeAuthAsync("<user_number>", hash, code);
``` 

You can see the full code at [AuthUser test](https://github.com/nblockchain/TgSharp/blob/master/src/TgSharp.Tests/TgSharpTests.cs#L70)

When the user is authenticated, TgSharp creates special file called _session.dat_. In this file TgSharp stores all information needed for the user's session. So you need to authenticate user every time the _session.dat_ file is corrupted or removed.

You can call any method on authenticated users. For example, let's send message to a friend by his phone number:

```csharp
    //get available contacts
    var result = await client.GetContactsAsync();

    //find recipient in contacts
    var user = result.Users
                     .OfType<TLUser>()
                     .FirstOrDefault(x => x.Phone == "<recipient_phone>");

    //send message
    await client.SendMessageAsync(new TLInputPeerUser() { UserId = user.Id }, "OUR_MESSAGE");
```

You can see the full code at [SendMessage test](https://github.com/nblockchain/TgSharp/blob/master/src/TgSharp.Tests/TgSharpTests.cs#L87)

To send a message to a channel you could use the following code:

```csharp
    //get user dialogs
    var dialogs = (TLDialogsSlice) await client.GetUserDialogsAsync();

    //find channel by title
    var chat = dialogs.Chats
                      .OfType<TLChannel>()
                      .FirstOrDefault(c => c.Title == "<channel_title>");

    //send message
    await client.SendMessageAsync(new TLInputPeerChannel() { ChannelId = chat.Id, AccessHash = chat.AccessHash.Value }, "OUR_MESSAGE");
```

You can see the full code at [SendMessageToChannel test](https://github.com/nblockchain/TgSharp/blob/master/src/TgSharp.Tests/TgSharpTests.cs#L107)


## Working with files

Telegram separate files to two categories -> big file and small file. File is Big if its size more than 10 Mb. TgSharp tries to hide this complexity from you, thats why we provide one method to upload files **UploadFile**.

```csharp
    var fileResult = await client.UploadFile("cat.jpg", new StreamReader("data/cat.jpg"));
```

TgSharp provides two wrappers for sending photo and document:

```csharp
    await client.SendUploadedPhoto(new TLInputPeerUser() { UserId = user.Id }, fileResult, "kitty");
    await client.SendUploadedDocument(new TLInputPeerUser() { UserId = user.Id },
                                      fileResult,
                                      "application/zip", //mime-type

                                      //document attributes, such as file name
                                      new TLVector<TLAbsDocumentAttribute>());
```

You can see the Full code at [SendPhotoToContactTest](https://github.com/nblockchain/TgSharp/blob/master/src/TgSharp.Tests/TgSharpTests.cs#L125) and [SendBigFileToContactTest](https://github.com/nblockchain/TgSharp/blob/master/src/TgSharp.Tests/TgSharpTests.cs#L143)

To download a file you should call the **GetFile** method:

```csharp
    await client.GetFile(new TLInputDocumentFileLocation()
                         {
                             AccessHash = document.AccessHash,
                             Id = document.Id,
                             FileReference = document.FileReference,
                             ThumbSize = "250x250"
                         },

                         //size of fileChunk you want to retrieve
                         document.Size);
```

You can see the Full code at [DownloadFileFromContactTest](https://github.com/nblockchain/TgSharp/blob/master/src/TgSharp.Tests/TgSharpTests.cs#L167)


# Available Methods

For your convenience TgSharp have wrappers for several Telegram API methods. You could add your own, see details below.

1. SendCodeRequestAsync
1. MakeAuthAsync
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


**Where can you find a list of requests and its parameters?**

The only way is [Telegram API docs](https://core.telegram.org/methods). Yes, it's no longer outdated.
Latest scheme in JSON format you can find [here](https://core.telegram.org/schema/json)


## What things can I help on (Project Roadmap)?

* Add Updates handling via events (WIP PR: https://github.com/sochix/TLSharp/pull/892 )
* GithubActions CI job for running tests
* Upgrade to .NETStandard 2.0.


# FAQ

#### What API layer is supported?

Layer 108. Thanks to Afshin Arani for his TLGenerator


#### I get a xxxMigrationException or a MIGRATE_X error!

TgSharp library should automatically handle these errors. If you see such errors, please open a new Github issue with the details (include a stacktrace, etc.).


#### I get an exception: System.IO.EndOfStreamException: Unable to read beyond the end of the stream. All test methos except that AuthenticationWorks and TestConnection return same error. I did every thing including setting api id and hash, and setting server address.-

You should create a Telegram session. See [configuration guide](#sending-messages-set-up)


#### Why do I get a FloodException/FLOOD_WAIT error?

After you get this, you cannot use Telegram's API for a while. You can know the time to wait by accessing the FloodException::TimeToWait property.

If this happens too often and/or the TimeToWait value is too long, there may be something odd going on. First and foremost, are you using TgSharp to manage more than one telegram account from the same host? If yes, it's likely that you're hitting [Telegram restrictions](https://core.telegram.org/api/errors#420-flood). We recommend that you use TgSharp in a standalone-device app (so that each instance of your program only uses one telegram account), so for example a mobile app, not a web app.

If, on the other hand, you're completely sure that you found a bug in TgSharp about this, please open a Github issue.


#### Why does TgSharp lacks feature XXXX?

TgSharp only covers basic functionality of the Telegram protocol, you can be a contributor or a sponsor to speed-up developemnt of any more new features.


#### Where else to ask for help?

If you just have questions about how to use TgSharp, use our Telegram channel (https://t.me/joinchat/AgtDiBEqG1i-qPqttNFLbA) first. Don't create a github issue until you have confirmed with the maintainer and/or contributors if what you're experiencing is really a bug. When this has been confirmed, then:

**Attach following information**:

* Steps to reproduce the issue (including your code)
* Expected outcome.
* Current outcome: if it's a crash, attach the full exception details (to get this, just paste what you get from `ex.ToString()`, which gives you exception type, exception message, stacktrace, and inner exceptions recursively).

Without information listed above your issue will be closed.


# Donations

Please send your donation to this ETH address (owned by Afshin Arani): 0xbfd1b684e0DdA5C219e11315682a9722b3194131


# Support

If you have troubles while using TgSharp, we may be able to help you; access our telegram channel first, and ask for "Paid support".


# Contributors

* [knocte](https://github.com/knocte) - Maintainer
* [Afshin Arani](https://github.com/aarani) - Main contributor
* [CheshireCaat](http://github.com/CheshireCaat) - Occasional contributor
* [sochix](https://github.com/sochix) - Original author


# License

**Please, provide link to the original authors when using library**

The MIT License


Copyright (c) 2015 Ilya Pirozhenko ([sochix](https://github.com/sochix))

Copyright (c) 2015-2020 TLSharp/TgSharp contributors


Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
