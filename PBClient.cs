using System;
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
                UserData = await newBase.GetRequestAsync<ClientData>(token, "/users/me").ConfigureAwait(false),
                UserDevices = await newBase.GetRequestAsync<ClientDevices>(token, "/devices").ConfigureAwait(false),
                UserPushes = await newBase.GetRequestAsync<ClientPushes>(token, "/pushes").ConfigureAwait(false)
            };
            double.TryParse(cl.UserData.Created.ToString(), out var result);
            cl.UserData.CreationDate = DateTimeOffset.FromUnixTimeSeconds((int)result);
            return cl;
        }

        private PBClient(string token)
        {
            _token = token;
            UserData = new ClientData();
            UserDevices = new ClientDevices();
            _newBase = new Base();
        }

        /// <summary>
        /// Updates your past pushes
        /// </summary>
        /// <returns>Your PushBullet devices</returns>
        public async Task UpdateDevicesAsync()
            => UserPushes = await _newBase.GetRequestAsync<ClientPushes>(_token, "/pushes").ConfigureAwait(false);

        /// <summary>
        /// Updates your past pushes
        /// </summary>
        /// <returns>Your PushBullet pushes</returns>
        public async Task UpdatePushesAsync()
           => UserDevices = await _newBase.GetRequestAsync<ClientDevices>(_token, "/pushes").ConfigureAwait(false);

        /// <summary>
        /// Sends a push notification request
        /// </summary>
        /// <returns>Sends a push notification request</returns>
        public async Task PushRequestAsync(PushRequest req)
        {
            var request = $"{{\"title\":\"{req.Title}\",\"body\":\"{req.Content}\",\"target_device_iden\":\"{req.TargetDeviceIdentity}\",\"type\":\"note\"}}";
            await _newBase.PostRequestAsync(_token, "https://api.pushbullet.com/v2/pushes", request).ConfigureAwait(false);
        }
    }
}