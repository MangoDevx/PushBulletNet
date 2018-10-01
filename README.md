This is my first time trying to make a wrapper. This is supposed to be for the PushBullet API.
------
![Nuget Listing](https://discord-is-down.party/77pGgQLL.png "Nuget listing")
Getting started
------
1. Establish your PushBullet client. This will retrieve all your information.
```cs
PushBullientClient client = new PushBulletClient("YourAPIToken");
```
Call your desired information
```cs
var data = await client.GetUserDataAsync();
var devices = await client.GetDevicesAsync();
var pushes = await client.GetPushesAsync();
```
3. For proper documentation, visit the [wiki](https://github.com/Adomix/PushBulletNet/wiki).
