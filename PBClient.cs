using System;
using System.Threading.Tasks;
using PushBulletNet.GET.ClientModels;
using PushBulletNet.POST;

namespace PushBulletNet
{
    public class PBClient
    {
        internal static string Token;
        public ClientData UserData { get; private set; }
        public ClientDevices UserDevices { get; private set; }
        private Base NewBase { get; set; }

        public static async Task<PBClient> GetInstance(string token)
        {
            var newBase = new Base();
            var cl = new PBClient(token);
            cl.UserData = await newBase.GetRequestAsync<ClientData>(Token, "/users/me").ConfigureAwait(false);
            cl.UserDevices = await newBase.GetRequestAsync<ClientDevices>(Token, "/devices").ConfigureAwait(false);
            var cr = double.TryParse(cl.UserData.Created.ToString(), out var created);
            cl.Created = DateTimeOffset.FromUnixTimeSeconds((int)created);
            return cl;
        }

        private PBClient(string token)
        {
            Token = token;
            UserData = new ClientData();
            UserDevices = new ClientDevices();
            NewBase = new Base();
        }

        public Push[] Pushes { get; internal set; }
        public DateTimeOffset Created { get; internal set; }

        /// <summary>
        /// Finds your past pushes
        /// </summary>
        /// <returns>Your PushBullet pushes</returns>
        public async Task GetPushesAsync()
        {
            var cl = await NewBase.GetRequestAsync<ClientPushes>(Token, "/pushes").ConfigureAwait(false);
            Pushes = cl.Pushes;
        }

        /// <summary>
        /// Sends a push notification request
        /// </summary>
        /// <returns>Sends a push notification request</returns>
        public async Task PushRequestAsync(PushRequest req)
        {
            var request = $"{{\"title\":\"{req.Title}\",\"body\":\"{req.Content}\",\"target_device_iden\":\"{req.TargetDeviceIdentity}\",\"type\":\"note\"}}";
            await NewBase.PostRequestAsync(Token, "https://api.pushbullet.com/v2/pushes", request).ConfigureAwait(false);
        }
    }
}