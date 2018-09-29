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

        public async Task<T> GetReq<T>(string token, string url, T data) where T: class
        {
            Client.DefaultRequestHeaders.Add("Access-Token", token);
            using (var get = await Client.GetAsync(url).ConfigureAwait(false))
            {
                if (!get.IsSuccessStatusCode)
                    throw new Exception("");
                var content = await get.Content.ReadAsStringAsync().ConfigureAwait(false);
                Client.DefaultRequestHeaders.Clear();
                return JsonConvert.DeserializeObject<T>(content);
            }
        }
    }
}