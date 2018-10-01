using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PushBulletNet.Exceptions;
using PushBulletNet.PushBullet.Model;

namespace PushBulletNet.PushBullet
{
    internal interface IPushBulletService
    {
        Task<PushBulletUser> GetClientData(string token);
        Task<IEnumerable<PushBulletDevice>> GetDevices(string token);
        Task<IEnumerable<PushBulletPush>> GetPushes(string token);
        Task Post(string token, string title, string content, string targetdeviceid, string url);
    }

    internal class PushBulletService : IPushBulletService
    {
        private readonly HttpClient _client;
        private readonly JsonSerializer _serializer;

        private const string ACCESS_TOKEN_HEADER = "Access-Token";
        private const string BASE_URI = "https://api.pushbullet.com/v2";

        public PushBulletService()
        {
            _client = new HttpClient();
            _serializer = new JsonSerializer();
        }

        public async Task<PushBulletUser> GetClientData(string token)
        {
            var clientData = await Get<PushBulletUser>(token, "users/me").ConfigureAwait(false);

            var createdSecondsAgo = clientData.Created;

            clientData.CreationDate = DateTimeOffset.FromUnixTimeSeconds((long)createdSecondsAgo);

            return clientData;
        }

        public async Task<IEnumerable<PushBulletDevice>> GetDevices(string token)
        {
            var devices = await Get<PushBulletDeviceData>(token, "devices").ConfigureAwait(false);

            return devices.Devices;
        }

        public async Task<IEnumerable<PushBulletPush>> GetPushes(string token)
        {
            var pushes = await Get<PushBulletPushData>(token, "pushes").ConfigureAwait(false);

            return pushes.Pushes;
        }
        
        public async Task Post(string token, string title, string content, string targetdevideid, string url)
        {
            _client.DefaultRequestHeaders.Add(ACCESS_TOKEN_HEADER, token);

            var pushRequest = $"{{\"title\":\"{title}\",\"body\":\"{content}\",\"target_device_iden\":\"{targetdevideid}\",\"type\":\"note\"}}";

            using (var response = await _client.PostAsync(BuildUri("pushes"), new StringContent(pushRequest, Encoding.UTF8, "application/json")).ConfigureAwait(false))
            {
                if (!response.IsSuccessStatusCode)
                    throw new PushBulletRequestFailedException("The request did not succeed"); // TODO More verbose message for the consumer - Coming right up sir

                _client.DefaultRequestHeaders.Clear();
            }
        }

        private async Task<T> Get<T>(string token, string url)
        {
            _client.DefaultRequestHeaders.Add(ACCESS_TOKEN_HEADER, token);

            using (var response = await _client.GetAsync(BuildUri(url)).ConfigureAwait(false))
            {
                if (!response.IsSuccessStatusCode)
                    throw new PushBulletRequestFailedException("The request did not succeed"); // TODO More verbose message for the consumer - Coming right up sir

                var content = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

                _client.DefaultRequestHeaders.Clear();

                return ProcessStream<T>(content);
            }
        }

        private T ProcessStream<T>(Stream stream)
        {
            using (var streamReader = new StreamReader(stream))
            {
                using (var jsonReader = new JsonTextReader(streamReader))
                {
                    return _serializer.Deserialize<T>(jsonReader);
                }
            }
        }

        private static string BuildUri(params string[] paths)
        {
            return BASE_URI + "/" + string.Join("/", paths);
        }
    }
}