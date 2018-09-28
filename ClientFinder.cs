using System.Threading.Tasks;

namespace PushBulletNet
{
    public class ClientFinder : Base
    {
        public async Task FindClient<ClientObject>(string token)
            => await GetClient<ClientObject>(token).ConfigureAwait(false);
    }
}
