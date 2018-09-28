using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PushBulletNet
{
    public class PBClient : Base
    {
        private static string Token { get; set; }
        private static double CreatedAt { get; set; }
        private static string Email { get; set; }
        private static string NormalEmail { get; set; }
        
        public PBClient(string token)
        {
            Token = token;
        }
        
        ///<summary>FindClient will find your PB client with your access token.</summary>
        public async Task FindClient<ClientObject>(string token)
            => await GetClient<ClientObject>(token).ConfigureAwait(false);
    }
}
