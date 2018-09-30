using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PushBulletNet
{
    internal class Base
    {
        private HttpClient Client { get; }
        private JsonSerializer Serializer { get; }

        public Base()
        {
            Client = new HttpClient();
            Serializer = new JsonSerializer();
        }

        public async Task<T> GetRequestAsync<T>(string token, string url)
        {
            Client.DefaultRequestHeaders.Add("Access-Token", token);
            using (var get = await Client.GetAsync($"https://api.pushbullet.com/v2/{url}").ConfigureAwait(false))
            {
                if (!get.IsSuccessStatusCode)
                    throw new Exception("GetReq failed!");
                var content = await get.Content.ReadAsStreamAsync().ConfigureAwait(false);
                Client.DefaultRequestHeaders.Clear();
                return ProcessStream<T>(content);
            }
        }

        private T ProcessStream<T>(Stream stream)
        {
            using (var streamReader = new StreamReader(stream))
            using (var jsonReader = new JsonTextReader(streamReader))
                return Serializer.Deserialize<T>(jsonReader);
        }

        public async Task PostRequestAsync(string token, string url, string request)
        {
            try
            {
                Client.DefaultRequestHeaders.Add("Access-Token", token);
                using (var get = await Client
                    .PostAsync(url, new StringContent(request, Encoding.UTF8, "application/json")).ConfigureAwait(false))
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