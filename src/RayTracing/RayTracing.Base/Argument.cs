using System.Security.Cryptography.X509Certificates;

namespace RayTracing.Base
{
    public class Argument
    {
        public static void AssertNotNull(object arg, string argName)
        {
            if (arg == null)
            {
                throw new ArgumentNullException($"The specified argument {argName} must not be null.");
            }
        }
    }
}