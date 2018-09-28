using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PushBulletNet
{
    public class PBClient
    {
        internal static string token;
        
        public PBClient(string Token)
        {
            token = Token;
        }
    }
}
