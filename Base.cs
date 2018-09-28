using Newtonsoft.Json;
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

        public async Task<T> GetClient<T>(string token)
        {
            Client.DefaultRequestHeaders.Add("Access-Token", token);
            var get = await Client.GetAsync("https://api.pushbullet.com/v2/users/me").ConfigureAwait(false);
            if (!get.IsSuccessStatusCode)
                throw new Exception("");
            var content = await get.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}