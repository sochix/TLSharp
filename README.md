#TLSharp

![](https://telegram.org/favicon.ico)

Telegram (http://telegram.org) client library implemented in C#. Only basic functionality is currently implemented. **Consider donation to speed up development process.**

It's a perfect fit for any developer who would like to send data directly to Telegram users.

**Please, don't use it for SPAM!**

#Table of contents

- [How do I add this to my project?](#how-do-i-add-this-to-my-project)
- [Dependencies](#dependencies)
- [Starter Guide](#starter-guide)
  - [Quick configuration](#quick-configuration)
  - [Sending messages set-up](#sending-messages-set-up)
  - [Using TLSharp](#using-tlsharp)
- [Contributing](#contributing)
- [FAQ](#faq)
- [License](#license)

#How do I add this to my project?

There currenrly is no Nu-Get package available, so you need to clone it from GitHub and compile in VS2015.

#Dependencies

TLSharp has a few dependenices, most of functionality implemented from scratch.
All dependencies listed in [package.conf file](https://github.com/sochix/TLSharp/blob/master/TLSharp.Core/packages.config).

#Starter Guide

## Quick Configuration
1. To start using TLSharp you need to create a [developer account](https://my.telegram.org/) with Telegram. 
1. After registering, copy API_ID and API_HASH from your account to [TelegramClient.cs](https://github.com/sochix/TLSharp/blob/master/TLSharp.Core/TelegramClient.cs)
1. When you're done you should specify a phone number in international format for Test purposes in [app.config file]( https://github.com/sochix/TLSharp/blob/master/TLSharp.Tests/app.config)
1. Run the test TestConnection(), if it passed than you successfully configured TLSharp.

##Sending messages set-up

1. First, create a valid session. Run the `AuthUser` test, and set a breakpoint on `var code = "123"; // you can change code in debugger line`. 
2. Replace value of `code` variable with code from Telegram. 
3. Continue execution. You'll see created session.dat file.
4. Try to run `SendMessage` test

## Using TLSharp

See tests to undertsand how TLSharp works.

## Contributing

You can contribute! If you have any questions don't be afraid to ask!

# FAQ

#### I get an error MIGRATE_X?

You should change the telegram server address to X. X server address you can get from InitResponse or from Server addresses list. Address should be changed in [this file](https://github.com/sochix/TLSharp/blob/master/TLSharp.Core/Network/TcpTransport.cs)

**Server addresses:**
* Server 1: 149.154.175.50:443
* Server 2: 149.154.167.51:443
* Server 3: 149.154.175.100:443
* Server 4: 149.154.167.91:443
* Server 5: 91.108.56.165:443

#### I get an exception: System.IO.EndOfStreamException: Unable to read beyond the end of the stream. All test methos except that AuthenticationWorks and TestConnection return same error. I did every thing including setting api id and hash, and setting server address.

You should create a Telegram session. See [configuration guide](#sending-messages-set-up)

#### Why I get FLOOD_WAIT error?
It's Telegram restrictions. See [this](https://core.telegram.org/api/errors#420-flood)

#### Why does TLSharp lacks future XXXX?

Now TLSharp is basic realization of Telegram protocol, you can be a contributor or a sponsor to speed-up developemnt of any feature.

# License

**Please, provide link to an author when you using library**

The MIT License

Copyright (c) 2015 Ilya Pirozhenko http://www.sochix.ru/

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
