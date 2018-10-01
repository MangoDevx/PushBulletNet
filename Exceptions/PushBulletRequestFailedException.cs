using System;

namespace PushBulletNet.Exceptions
{
    public class PushBulletRequestFailedException : Exception
    {
        internal PushBulletRequestFailedException()
        {
        }

        internal PushBulletRequestFailedException(string message)
            : base(message)
        {
        }

        internal PushBulletRequestFailedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
