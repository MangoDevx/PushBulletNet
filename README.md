### This is my first time trying to make a wrapper. This is supposed to be for the PushBullet API.
![Nuget Listing](https://discord-is-down.party/77pGgQLL.png "Nuget listing")
### Getting started
1. Establish your PushBullet client.
```cs
PBClient client = await PBClient.GetInstance("YourAPIToken");
```
2. Retrive your UserData or Devices (Optional)
```cs
client.UserData.x
client.UserDevices.Devices
```
3. How to select a certain device
```cs
/* We may use LINQ to find a certain device.
If you are unsure of the exact manufacturer name, use StringComparison.OrdinalIgnoreCase
This will get the ID of the first device made by Samsung
*/
var device = client.UserDevices.Devices.FirstOrDefault(x => x.Manufacturer.Equals("Samsung", StringComparison.OrdinalIgnoreCase).Iden);
```
4. Retrieve your push history (Optional)
```cs
await client.GetPushesAsync();
```
5. Sending a push notification
```cs
// First we need to create a new PushRequest
PushRequest push = new PushRequest
            {
                // TargetDeviceIdentity needs to be the Iden of the device you want to receive the push notification
                TargetDeviceIdentity = device,
                Title = "Push notification title",
                Content = "The body of the notification"
            };
// Now we need to send the request
            await _client.PushRequestAsync(push);
```
