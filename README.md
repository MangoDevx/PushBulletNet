This is my first time making an API wrapper. This is for the PushBullet API.
------
https://www.nuget.org/packages/PushBulletNet/
Getting started
------
1. Establish your client
```cs
PushBullientClient client = new PushBulletClient("YourAPIToken");
```
2. Call your desired information
```cs
var data = await client.GetUserDataAsync();
var devices = await client.GetDevicesAsync();
var pushes = await client.GetPushesAsync();
var chats = await client.GetChatsAsync();
```
3. For proper documentation, visit the [wiki](https://github.com/Adomix/PushBulletNet/wiki).
