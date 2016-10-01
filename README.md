#TLSharp

[![Build status](https://ci.appveyor.com/api/projects/status/95rl618ch5c4h2fa?svg=true)](https://ci.appveyor.com/project/sochix/tlsharp)

Telegram (http://telegram.org) client library implemented in C#. Only basic functionality is currently implemented. **Consider donation to speed up development process.** Bitcoin wallet: **3K1ocweFgaHnAibJ3n6hX7RNZWFTFcJjUe**

It's a perfect fit for any developer who would like to send data directly to Telegram users.

:star: If you :heart: library, please star it! :star:

:exclamation: **Please, don't use it for SPAM!**

[How-To: Send messages to Telegram from C#](http://www.sochix.ru/how-to-send-messages-to-telegram-from-c/)

:ru: Russian description you can find [here](https://habrahabr.ru/post/277079/)

#Table of contents?

- [How do I add this to my project?](#how-do-i-add-this-to-my-project)
- [Dependencies](#dependencies)
- [Starter Guide](#starter-guide)
  - [Quick configuration](#quick-configuration)
  - [Using TLSharp](#using-tlsharp)
- [Contributing](#contributing)
- [FAQ](#faq)
- [Donations](#donations)
- [License](#license)

#How do I add this to my project?

Library isn't ready for production usage, that's why no Nu-Get package available.

To use it follow next steps:

1. Clone TLSharp from GitHub
1. Compile source with VS2015
1. Add reference to ```TLSharp.Core.dll``` to your awesome project.

#Dependencies

TLSharp has a few dependenices, most of functionality implemented from scratch.
All dependencies listed in [package.conf file](https://github.com/sochix/TLSharp/blob/master/TLSharp.Core/packages.config).

#Starter Guide

## Quick Configuration
Telegram API isn't that easy to start. You need to do some configuration first.

1. Create a [developer account](https://my.telegram.org/) in Telegram. 
1. Goto [API development tools](https://my.telegram.org/apps) and copy **API_ID** and **API_HASH** from your account. You'll need it later.

## Using TLSharp

###Initializing client

To initialize client you need to create a store in which TLSharp will save Session info.

```
var store = new FileSessionStore();
```

Next, create client instance and connect to Telegram server. You need your **API_ID** and **API_HASH** for this step.

```
var client = new TelegramClient(store, "session", API_ID, "API_HASH");
await client.Connect();
```
Now, you can call methods.

All methods except [IsPhoneRegistered](#IsPhoneRegistered) requires to authenticated user. Example usage of all methods you can find in [Tests].

###Supported methods
Currently supported methods:
 - [IsPhoneRegistered - Check if phone is registered in Telegram](#isphoneregistered)
 - [Authenticate user](#authenticate-user)
 - [SignUp user](#signup-user)

####IsPhoneRegistered
Check if phone number registered to Telegram.

_Example_:

```
var result = await client.IsPhoneRegistered(phoneNumber)
```

* phoneNumber - **string**, phone number in international format (eg. 791812312323)

**Returns:** **bool**, is phone registerd in Telegram or not.


####Authenticate user
Authenticate user by phone number and secret code.

_Example_:

```
	var hash = await client.SendCodeRequest(phoneNumber);
    
	var code = "1234"; //code that you receive from Telegram 

	var user = await client.MakeAuth(phoneNumber, hash, code); 
```
* phoneNumber - **string**, phone number in international format (eg. 791812312323)

**Returns:** **User**, authenticated User.

####SignUp user
Register new user by phone number, secret code and first/last name.

_Example_:

```
	var hash = await client.SendCodeRequest(phoneNumber);
    
	var code = "1234"; //code that you receive from Telegram 

	var user = await client.SignUp(phoneNumber, hash, code, "TLSharp", "User"); 
```
* phoneNumber - **string**, phone number in international format (eg. 791812312323) that is not yet registered in Telegram.
* firstName - **string**, new user first name
* lastName - **string**, new user last name

**Returns:** **User**, authenticated User.

## Contributing

Contributing is highly appreciated!

###What things can I Implement (Project Roadmap)?

* Add Updates handling

# FAQ

#### I get an error MIGRATE_X?

TLSharp library should automatically handle this errors. If you see such errors, pls create a new issue.

#### I get an exception: System.IO.EndOfStreamException: Unable to read beyond the end of the stream. All test methos except that AuthenticationWorks and TestConnection return same error. I did every thing including setting api id and hash, and setting server address.

You should create a Telegram session. See [configuration guide](#sending-messages-set-up)

#### Why I get FLOOD_WAIT error?
It's Telegram restrictions. See [this](https://core.telegram.org/api/errors#420-flood)

#### Why does TLSharp lacks feature XXXX?

Now TLSharp is basic realization of Telegram protocol, you can be a contributor or a sponsor to speed-up developemnt of any feature.

#### Nothing helps
Create an issue in project bug tracker.

**Attach this information**:

* Full problem description and exception message
* Stack-trace
* Your code that runs in to this exception

Without information listen above your issue will be closed. 
# Donations
Thanks for donations! It's highly appreciated. 
Bitcoin wallet: **3K1ocweFgaHnAibJ3n6hX7RNZWFTFcJjUe**

List of donators:
* [mtbitcoin](https://github.com/mtbitcoin)

# License

**Please, provide link to an author when you using library**

The MIT License

Copyright (c) 2015 Ilya Pirozhenko http://www.sochix.ru/

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
