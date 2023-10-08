using System.Diagnostics.CodeAnalysis;

namespace RayTracing.Base
{
    /// <summary>
    /// Performs runtime checks on arguments, and throws appropriate exceptions.
    /// </summary>
    public class Argument
    {
        /// <summary>
        /// Asserts that the specified argument not be null; throws an exception if it is.
        /// </summary>
        public static void AssertNotNull([NotNull] object? arg, string argName)
        {
            if (arg == null)
            {
                throw new ArgumentNullException($"The specified argument {argName} must not be null.");
            }
        }
    }
}