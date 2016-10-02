OTServer Info
======
Catch information from servers' response by ip and port

Install
------
Just download this repo, and add reference OTInfo.dll
```csharp
using OTInfo;
```

Get started
------
**To instantiate an otserv, create a new OTInfo object**
```csharp
ServerInformation server = new ServerInformation("www.aurera-global.com");
```
The first parameter is the server `host/ip`, and the second is the `port` (defined 7171 as default)

**Get informations**
```csharp
if (server.Execute()) {
  Console.WriteLine("Players online: " + server.info.tsqp.players.online);
  Console.WriteLine("Server location: " + server.info.tsqp.serverinfo.location);
  Console.WriteLine("Client version: " + server.info.tsqp.serverinfo.version);
  // these are just a few examples
} else {
  Console.WriteLine("Server offline.");
  // if execute() returns false, the server are offline
}
```
The `Execute()' method catch/parse the responses returned by server and return false if server are offline.

Possible responses
------
**Each server has its own response, and may be different from the others. This means that not all respond with the same information, and a server may have information that others do not have.**

Here are some possible answers nodes
* `players`
* `serverinfo`
* `motd`
* `owner`
* `monsters`
* `map`
* `npcs`
* maybe others

Cache
------
OTInfo cache itself not only for performance, but also to avoid empty responses, caused due to the protection of tfs.
As said before, you must have a writable `cache` folder in the same level of Application or set a new Path by function setPath(string path)

Set Path
------
```csharp
server.setPath(@"C:\Users\Bruno\Documents\Cache");
```

**Special Thanks**
* Renato Ribeiro (renatorib)
