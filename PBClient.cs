using System;
using System.Threading.Tasks;

namespace PushBulletNet.Client
{
    public class PBClient : Base
    {

        public string Name { get; internal set; }
        public double Created { get; internal set; }
        private string Email { get; set; }
        public string EmailNormalized { get; internal set; }
        public string Identity { get; internal set; }
        public Uri ImageUrl { get; internal set; }
        public long MaxUploadSize { get; internal set; }
        public double Modified { get; internal set; }
        public Device[] Devices { get; internal set; }
        public Push[] Pushes { get; internal set; }

        internal string Token;

        public PBClient(string token)
        {
            Token = token;
        }

        ///<summary>FindClient will find your PB client with your access token.</summary>
        public async Task FindClient()
        {
            var cl = await GetReq(Token, "https://api.pushbullet.com/v2/users/me", new ClientData()).ConfigureAwait(false);
            Name = cl.Name;
            Created = cl.Created;
            Email = cl.Email;
            EmailNormalized = cl.EmailNormalized;
            Identity = cl.Iden;
            ImageUrl = cl.ImageUrl;
            MaxUploadSize = cl.MaxUploadSize;
            Modified = cl.Modified;
        }

        public async Task GetDevices()
        {
            var cl = await GetReq(Token, "https://api.pushbullet.com/v2/devices", new ClientDevices()).ConfigureAwait(false);
            Devices = cl.Devices;
        }

        public async Task GetPushes()
        {
            var cl = await GetReq(Token, "https://api.pushbullet.com/v2/pushes", new ClientPushes()).ConfigureAwait(false);
            Pushes = cl.Pushes;
        }
    }
}