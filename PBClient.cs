using System;
using System.Threading.Tasks;
using PushBulletNet.GET.ClientModels;
using PushBulletNet.POST;

namespace PushBulletNet
{
    public class PBClient : Base
    {
        internal static string Token;

        public PBClient(string token)
        {
            Token = token;
        }

        public string Name { get; internal set; }
        public string Email { get; set; }
        public string EmailNormalized { get; internal set; }
        public string Identity { get; internal set; }
        public long MaxUploadSize { get; internal set; }
        public double Created { get; internal set; }
        public double Modified { get; internal set; }
        public Uri ImageUrl { get; internal set; }
        public Uri UploadUrl { get; internal set; }
        public Device[] Devices { get; internal set; }
        public Push[] Pushes { get; internal set; }

        /// <summary>
        /// Finds your PB client
        /// </summary>
        /// <returns>Your PushBullet Client</returns>
        public async Task FindClient()
        {
            var cl = await GetReqAsync(Token, "https://api.pushbullet.com/v2/users/me", new ClientData())
                .ConfigureAwait(false);
            Name = cl.Name;
            Created = cl.Created;
            Email = cl.Email;
            EmailNormalized = cl.EmailNormalized;
            Identity = cl.Iden;
            ImageUrl = cl.ImageUrl;
            MaxUploadSize = cl.MaxUploadSize;
            Modified = cl.Modified;
        }

        /// <summary>
        /// Finds your active devices
        /// </summary>
        /// <returns>Your PushBullet devices</returns>
        public async Task GetDevices()
        {
            var cl = await GetReqAsync(Token, "https://api.pushbullet.com/v2/devices", new ClientDevices())
                .ConfigureAwait(false);
            Devices = cl.Devices;
        }

        /// <summary>
        /// Finds your past pushes
        /// </summary>
        /// <returns>Your PushBullet pushes</returns>
        public async Task GetPushes()
        {
            var cl = await GetReqAsync(Token, "https://api.pushbullet.com/v2/pushes", new ClientPushes())
                .ConfigureAwait(false);
            Pushes = cl.Pushes;
        }

        /// <summary>
        /// Sends a push notification request
        /// </summary>
        /// <returns>Sends a push notification request</returns>
        public async Task PushNotificationReq(PushReq req)
        {
            var cl = await GetReqAsync(Token, "https://api.pushbullet.com/v2/upload-request", new ClientRequests())
                .ConfigureAwait(false);
            UploadUrl = cl.UploadUrl;
            var request =$"{{\"title\":\"{req.Title}\",\"body\":\"{req.Content}\",\"target_device_iden\":\"{req.TargetDeviceIdentity}\",\"type\":\"note\"}}";
            await SendPostReqAsync(Token, "https://api.pushbullet.com/v2/pushes", request).ConfigureAwait(false);
        }
    }
}