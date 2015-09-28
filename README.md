#TLSharp

![](https://telegram.org/favicon.ico)

Telegram (http://telegram.org) client library implemented in C#. Only basic functionality are implemented now. **Consider donation to speed up development process.**

It's a perfect fit for any developer who need to sends data directly to Telegram users.

**Please, don't use it for SPAM!**

#Table of contents

- [How do I add this to my project?](#how-do-i-add-this-to-my-project)
- [Dependencies](#dependencies)
- [Starter Guide](#starter-guide)
  - [Quick configuration](#quick-configuration)
  - [Using TLSharp](#using-tlsharp)
- [Contributing](#contributing)
- [FAQ](#faq)
- [License](#license)

#How do I add this to my project?

Library don't have Nu-Get package, so you need to download it from GitHub and compile in VS2015.

#Dependencies

TLSharp have a few dependenices, most of functionality implemented from scratch.
All dependencies listed in [package.conf file](https://github.com/sochix/TLSharp/blob/master/TLSharp.Core/packages.config).

#Starter Guide

## Quick Configuration
1. To start using TLSharp you need to create a developer account in Telegram. Start from https://my.telegram.org/
1. After registering copy API_ID and API_HASH from your account to [TelegramClient.cs](https://github.com/sochix/TLSharp/blob/master/TLSharp.Core/TelegramClient.cs)
1. When you're done you should specify phone number in international format for Test purposes in [app.config file]( https://github.com/sochix/TLSharp/blob/master/TLSharp.Tests/app.config)
1. Run the test TestConnection(), if it passed than you successfully setted up a library.

## Using TLSharp

See tests to undertsand how TLSharp works.

## Contributing

You can contribute! If you have any questions don't afraid to ask!

# FAQ

#### I get an error MIGRATE_XX?

You should change the telegram server address to XX. XX server address you can get from InitResponse. Address should be changed in [this file](https://github.com/sochix/TLSharp/blob/master/TLSharp.Core/Network/TcpTransport.cs)

#### Why does TLSharp lacks future XXXX?

Now TLSharp is basic realization of Telegram protocol, you can be a contributor or a sponsor to speed-up developemnt of any feature.

# License

**Please, provide link to an author when you using library**

The MIT License

Copyright (c) 2015 Ilya Pirozhenko http://www.sochix.ru/

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
