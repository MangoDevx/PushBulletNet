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
        private readonly Base _newBase;

        public static async Task<PBClient> GetInstance(string token)
        {
            var newBase = new Base();
            var cl = new PBClient(token)
            {
                UserData = await newBase.GetRequestAsync<ClientData>(token, "/users/me").ConfigureAwait(false),
                UserDevices = await newBase.GetRequestAsync<ClientDevices>(token, "/devices").ConfigureAwait(false)
            };

            if (double.TryParse(cl.UserData.Created.ToString(CultureInfo.InvariantCulture), out var created))
                cl.Created = DateTimeOffset.FromUnixTimeSeconds((int)created);

            return cl;
        }

        private PBClient(string token)
        {
            _token = token;
            UserData = new ClientData();
            UserDevices = new ClientDevices();
            _newBase = new Base();
        }

        public Push[] Pushes { get; private set; }
        public DateTimeOffset Created { get; private set; }

        /// <summary>
        /// Finds your past pushes
        /// </summary>
        /// <returns>Your PushBullet pushes</returns>
        public async Task GetPushesAsync()
        {
            var cl = await _newBase.GetRequestAsync<ClientPushes>(_token, "/pushes").ConfigureAwait(false);
            Pushes = cl.Pushes;
        }

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