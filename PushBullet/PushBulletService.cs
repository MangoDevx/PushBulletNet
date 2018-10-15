using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PushBulletNet.Exceptions;
using PushBulletNet.PushBullet.Models;
using PushBulletNet.PushBullet.Models.GetModels;
using PushBulletNet.PushBullet.Models.PostModels;

namespace PushBulletNet.PushBullet
{
    internal interface IPushBulletService
    {
        Task<PushBulletUser> GetClientData();
        Task<IEnumerable<PushBulletDevice>> GetDevices();
        Task<IEnumerable<PushBulletPush>> GetPushes();
        Task<IEnumerable<PushBulletChat>> GetChats();
        Task PushNotification(NotificationPostModel request);
        Task CreateDevice(NewDeviceModel model);
        Task CreateChat(string email);
        Task CreateSubscription(string channeltag);
    }

    internal class PushBulletService : IPushBulletService
    {
        private readonly string _token;
        private readonly HttpClient _client;
        private readonly JsonSerializer _serializer;

        private const string ACCESS_TOKEN_HEADER = "Access-Token";
        private const string BASE_URI = "https://api.pushbullet.com/v2";

        public PushBulletService(string token)
        {
            _token = token;
            _client = new HttpClient();
            _serializer = new JsonSerializer();
        }

        public async Task<PushBulletUser> GetClientData()
        {
            var clientData = await Get<PushBulletUser>(_token, "users/me").ConfigureAwait(false);

            var createdSecondsAgo = clientData.Created;

            clientData.CreationDate = DateTimeOffset.FromUnixTimeSeconds((long)createdSecondsAgo);

            return clientData;
        }

        public async Task<IEnumerable<PushBulletDevice>> GetDevices()
        {
            var devices = await Get<PushBulletDeviceData>(_token, "devices").ConfigureAwait(false);

            return devices.Devices;
        }

        public async Task<IEnumerable<PushBulletPush>> GetPushes()
        {
            var pushes = await Get<PushBulletPushData>(_token, "pushes").ConfigureAwait(false);

            return pushes.Pushes;
        }

        public async Task<IEnumerable<PushBulletChat>> GetChats()
        {
            var chats = await Get<PushBulletChatData>(_token, "chats").ConfigureAwait(false);

            return chats.Chats;
        }

        public async Task PushNotification(NotificationPostModel request)
        {
            var pushRequest = JsonConvert.SerializeObject(request);
            await Post(_token, "pushes", pushRequest).ConfigureAwait(false);
        }

        public async Task CreateDevice(NewDeviceModel model)
        {
            var pushRequest = JsonConvert.SerializeObject(model);
            await Post(_token, "devices", pushRequest).ConfigureAwait(false);
        }

        public async Task CreateChat(string email)
        {
            await Post(_token, "chats", $"{{\"email\": \"{email}\"}}").ConfigureAwait(false);
        }

        public async Task CreateSubscription(string channeltag)
        {
            await Post(_token, "subscriptions", $"{{\"channel_tag\": \"{channeltag}\"}}").ConfigureAwait(false);
        }

        private async Task<T> Get<T>(string token, string url)
        {
            _client.DefaultRequestHeaders.Add(ACCESS_TOKEN_HEADER, token);

            using (var response = await _client.GetAsync(BuildUri(url)).ConfigureAwait(false))
            {
                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    var reason = ProcessStream<ErrorModel>(error);
                    throw new PushBulletRequestFailedException($"GET request failed! {response.ReasonPhrase} || API Response: {reason.Cat ?? "N/A"} {reason.Message ?? "N/A"} {reason.Happy}");
                }

                var content = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

                _client.DefaultRequestHeaders.Clear();

                return ProcessStream<T>(content);
            }
        }

        private async Task Post(string token, string url, string request)
        {
            _client.DefaultRequestHeaders.Add(ACCESS_TOKEN_HEADER, token);

            using (var response = await _client.PostAsync(BuildUri(url), new StringContent(request, Encoding.UTF8, "application/json")).ConfigureAwait(false))
            {
                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    var reason = ProcessStream<ErrorModel>(error);
                    throw new PushBulletRequestFailedException($"POST request failed! {response.ReasonPhrase} || API Response: {reason.Cat ?? "N/A"} {reason.Message ?? "N/A"} {reason.Happy}");
                }

                _client.DefaultRequestHeaders.Clear();
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
