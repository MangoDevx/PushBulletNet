using Newtonsoft.Json;
using PushBulletNet.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PushBulletNet
{
    public class Base
    {
        internal readonly HttpClient Client;

        public Base()
        {
            Client = new HttpClient();
        }

        public async Task<ClientData> GetClient(string token)
        {
            Client.DefaultRequestHeaders.Add("Access-Token", token);
            using (var get = await Client.GetAsync("https://api.pushbullet.com/v2/users/me").ConfigureAwait(false))
            {
                if (!get.IsSuccessStatusCode)
                    throw new Exception("");
                var content = await get.Content.ReadAsStringAsync().ConfigureAwait(false);
                Client.DefaultRequestHeaders.Clear();
                return JsonConvert.DeserializeObject<ClientData>(content);
            }
        }

        public async Task<ClientDevices> GetDevices(string token)
        {
            Client.DefaultRequestHeaders.Add("Access-Token", token);
            using (var get = await Client.GetAsync("https://api.pushbullet.com/v2/devices").ConfigureAwait(false))
            {
                if (!get.IsSuccessStatusCode)
                    throw new Exception("Device loading failed!");
                var content = await get.Content.ReadAsStringAsync().ConfigureAwait(false);
                Client.DefaultRequestHeaders.Clear();
                return JsonConvert.DeserializeObject<ClientDevices>(content);
            }
        }

        public async Task<ClientPushes> GetPushes(string token)
        {
            Client.DefaultRequestHeaders.Add("Access-Token", token);
            using (var get = await Client.GetAsync("https://api.pushbullet.com/v2/pushes").ConfigureAwait(false))
            {
                if (!get.IsSuccessStatusCode)
                    throw new Exception("Pushes loading failed!");
                var content = await get.Content.ReadAsStringAsync().ConfigureAwait(false);
                Client.DefaultRequestHeaders.Clear();
                return JsonConvert.DeserializeObject<ClientPushes>(content);
            }
        }
    }
}