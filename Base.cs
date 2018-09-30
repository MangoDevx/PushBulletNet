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
        private readonly HttpClient _client;
        private readonly JsonSerializer _serializer;

        public Base()
        {
            _client = new HttpClient();
            _serializer = new JsonSerializer();
        }

        public async Task<T> GetRequestAsync<T>(string token, string url)
        {
            _client.DefaultRequestHeaders.Add("Access-Token", token);
            using (var get = await _client.GetAsync($"https://api.pushbullet.com/v2/{url}").ConfigureAwait(false))
            {
                if (!get.IsSuccessStatusCode)
                    throw new Exception("GetReq failed!");
                var content = await get.Content.ReadAsStreamAsync().ConfigureAwait(false);
                _client.DefaultRequestHeaders.Clear();
                return ProcessStream<T>(content);
            }
        }

        private T ProcessStream<T>(Stream stream)
        {
            using (var streamReader = new StreamReader(stream))
            using (var jsonReader = new JsonTextReader(streamReader))
                return _serializer.Deserialize<T>(jsonReader);
        }

        public async Task PostRequestAsync(string token, string url, string request)
        {
            try
            {
                _client.DefaultRequestHeaders.Add("Access-Token", token);
                using (var get = await _client
                    .PostAsync(url, new StringContent(request, Encoding.UTF8, "application/json")).ConfigureAwait(false))
                {
                    if (!get.IsSuccessStatusCode)
                        throw new Exception("SendPostReq failed!");
                    _client.DefaultRequestHeaders.Clear();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}