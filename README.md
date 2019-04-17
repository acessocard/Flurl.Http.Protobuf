# Flurl.Http.Protobuf

Protobuf extension to [Flurl](https://github.com/tmenier/Flurl) library, using [Protobuf-net](https://github.com/mgravell/protobuf-net) to serialization

This is a community project, free and open source. Everyone is invited to contribute, fork, share and use the code.

[![Build status](https://ci.appveyor.com/api/projects/status/3l2t4y48a7ejar68/branch/master?svg=true)](https://ci.appveyor.com/project/Acesso/flurl-http-protobuf/branch/master)
[![NuGet](https://img.shields.io/nuget/v/Flurl.Http.Protobuf.svg?maxAge=86400)](https://www.nuget.org/packages/Flurl.Http.Protobuf/)
---

## Features
* Get, post, put and receive protobuf models

---

## Usage
* Create your class or use .proto file
~~~~
[ProtoContract]
class Person {
    [ProtoMember(1)]
    public int Id {get;set;}
    [ProtoMember(2)]
    public string Name {get;set;}
}
~~~~

* Get an Protobuf:
~~~~
var result = await "https://some.url".WithClient(client).GetProtobufAsync<Person>();
~~~~

* Post and receive a model:
~~~~
var obj = new Person
{
    Id = 1,
    Name = "Foo"
};

var result = await "https://some.url".PostProtobufAsync(obj);
~~~~

* Put a model and receive:
~~~~
 var obj = new Person
{
    Id = 2,
    Name = "Foo"
};

var result = await "https://some.url".PutProtobufAsync(obj).ReceiveProtobuf<Person>();
~~~~

---

## Support
To report a bug or request a feture, [open an issue](https://github.com/acessocard/Flurl.Http.Protobuf/issues) on GitHub.

---

## Contributing

* Start [creating an issue](https://github.com/acessocard/Flurl.Http.Protobuf/issues/new) and describing your proposed fix/enhancement.
* Fork the project and make your change.
* Write tests to cover all new/changed functionality.

---

## Maintainers/Core team

* [Guilherme Baldini](https://github.com/Baldini)
* [Fabricio Santos Hilário do Nascimento](https://github.com/fabricio0915)

Contributors can be found at the [contributors](https://github.com/acessocard/Flurl.Http.Protobuf/graphs/contributors) page on Github.

---

## Thanks
* [protobuf-net](https://github.com/mgravell/protobuf-net)
* [Flurl](https://github.com/tmenier/Flurl)