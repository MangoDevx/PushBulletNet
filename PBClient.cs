using System;
using System.Globalization;
using System.Threading.Tasks;
using PushBulletNet.GET.ClientModels;
using PushBulletNet.POST;

namespace PushBulletNet
{
    public class PBClient
    {
        private readonly string _token;
        public ClientData UserData { get; private set; }
        public ClientDevices UserDevices { get; private set; }
        public ClientPushes UserPushes { get; private set; }
        private readonly Base _newBase;

        public static async Task<PBClient> GetClientAsync(string token)
        {
            var newBase = new Base();
            var cl = new PBClient(token)
            {
                UserData = await newBase.GetRequestAsync<ClientData>(Token, "/users/me").ConfigureAwait(false),
                UserDevices = await newBase.GetRequestAsync<ClientDevices>(Token, "/devices").ConfigureAwait(false),
                UserPushes = await newBase.GetRequestAsync<ClientPushes>(Token, "/pushes").ConfigureAwait(false)
            };
            return cl;
        }

        private PBClient(string token)
        {
            _token = token;
            UserData = new ClientData();
            UserDevices = new ClientDevices();
            _newBase = new Base();
        }

        public Push[] Pushes { get; internal set; }

        /// <summary>
        /// Finds your past pushes
        /// </summary>
        /// <returns>Your PushBullet pushes</returns>
        public async Task UpdatePushesAsync()
        {
            UserPushes = await _newBase.GetRequestAsync<ClientPushes>(Token, "/pushes").ConfigureAwait(false);
        }

        /// <summary>
        /// Sends a push notification request
        /// </summary>
        /// <returns>Sends a push notification request</returns>
        public async Task PushRequestAsync(PushRequest req)
        {
            var request = $"{{\"title\":\"{req.Title}\",\"body\":\"{req.Content}\",\"target_device_iden\":\"{req.TargetDeviceIdentity}\",\"type\":\"note\"}}";
            await _newBase.PostRequestAsync(Token, "https://api.pushbullet.com/v2/pushes", request).ConfigureAwait(false);
        }
    }
}