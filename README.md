#TLSharp

[![Build status](https://ci.appveyor.com/api/projects/status/1vm2nj8lr1p8d8mv)](https://ci.appveyor.com/project/aarani/tlsharp)


Telegram (http://telegram.org) client library implemented in C#. Only basic functionality is currently implemented.

It's a perfect fit for any developer who would like to send data directly to Telegram users.

:star: If you :heart: library, please star it! :star:

:exclamation: **Please, don't use it for SPAM!**

#Table of contents?

- [How do I add this to my project?](#how-do-i-add-this-to-my-project)
- [Dependencies](#dependencies)
- [Starter Guide](#starter-guide)
  - [Quick configuration](#quick-configuration)
  - [Using TLSharp](#using-tlsharp)
- [Contributing](#contributing)
- [FAQ](#faq)
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

## Contributing

Contributing is highly appreciated!

###How to add new functions

Adding new functions is easy.

* Just create a new Request class in Requests folder.
* Derive it from MTProtoRequest.

Requests specification you can find in [Telegram API](https://core.telegram.org/#api-methods) reference.

_Example_:

```
public class ExampleRequest : MTProtoRequest
{
    private int _someParameter;

    // pass needed parameters through constructor, and save it to private vars
    public InitConnectionRequest(int someParameter)
    {
        _someParameter = someParameter;
    }

    // send all needed params to Telegram
    public override void OnSend(BinaryWriter writer)
    {
        writer.Write(_someParameter); 
    }

    // read a received data from Telegram 
    public override void OnResponse(BinaryReader reader)
    {
        _someParameter = reader.ReadUInt32();
    }

    public override void OnException(Exception exception)
    {
        throw new NotImplementedException();
    }

    public override bool Responded { get; }

    public override bool Confirmed => true;
}
```

More advanced examples you can find in [Requests folder](https://github.com/sochix/TLSharp/tree/master/TLSharp.Core/Requests). 

###What things can I Implement (Project Roadmap)?

* Factor out current TL language implementation, and use [this one](https://github.com/everbytes/SharpTL)
* Add possibility to get current user Chats and Users
* Fix Chat requests (Create, AddUser) 
* Add Updates handling
* Add possibility to work with Channels

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
