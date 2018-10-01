This is my first time trying to make a wrapper. This is supposed to be for the PushBullet API.
------
![Nuget Listing](https://discord-is-down.party/77pGgQLL.png "Nuget listing")
Getting started
------
1. Establish your PushBullet client. This will retrieve all your information.
```cs
PBClient client = await PBClient.GetClientAsync("YourAPIToken");
```
2. Update your information (devices/pushes) if needed
```cs
await client.UpdateDevicesAsync();
await client.UpdatePushesAsync();
```
3. For proper documentation, visit the [wiki](https://github.com/Adomix/PushBulletNet/wiki/Getting-Started-With-PB.Net).
