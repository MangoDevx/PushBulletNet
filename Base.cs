using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PushBulletNet
{
    public class Base
    {
        internal readonly HttpClient Client;

        public Base()
        {
            Client = new HttpClient();
        }

        public async Task<T> GetReqAsync<T>(string token, string url, T data) where T : class
        {
            Client.DefaultRequestHeaders.Add("Access-Token", token);
            using (var get = await Client.GetAsync(url).ConfigureAwait(false))
            {
                if (!get.IsSuccessStatusCode)
                    throw new Exception("GetReq failed!");
                var content = await get.Content.ReadAsStringAsync().ConfigureAwait(false);
                Client.DefaultRequestHeaders.Clear();
                return JsonConvert.DeserializeObject<T>(content);
            }
        }

        public async Task SendPostReqAsync(string token, string url, string request)
        {
            try
            {
                Client.DefaultRequestHeaders.Add("Access-Token", token);
                using (var get = await Client
                    .PostAsync(url, new StringContent(request, Encoding.UTF8, "application/json"))
                    .ConfigureAwait(false))
                {
                    if (!get.IsSuccessStatusCode)
                        throw new Exception("SendPostReq failed!");
                    Client.DefaultRequestHeaders.Clear();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}