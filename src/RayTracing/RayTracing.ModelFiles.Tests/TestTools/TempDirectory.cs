using RayTracing.Base;
using System.Diagnostics;

namespace RayTracing.ModelFiles.Tests.TestTools
{
    /// <summary>
    /// Creates a random temporary directory which can be used as disposable working directory.
    /// </summary>
    public class TempDirectory : IDisposable
    {
        /// <summary>
        /// Initializes and created the random temporary directory.
        /// </summary>
        public TempDirectory()
            : this(pathPrefix: Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                Process.GetCurrentProcess().ProcessName))
        {
        }

        /// <summary>
        /// Initializes and created the random temporary directory, using the specified path prefix.
        /// </summary>
        /// <param name="pathPrefix">
        /// The path prefix where the random temporary directory is created
        /// </param>
        public TempDirectory(string pathPrefix)
        {
            Argument.AssertNotNull(pathPrefix, nameof(pathPrefix));

            var folderName = Guid.NewGuid().ToString();
            var path = Path.Combine(pathPrefix, folderName);

            // Create the directory.
            var dir = Directory.CreateDirectory(path);
            FullName = dir.FullName;
        }

        /// <summary>
        /// Gets the full name of the temporary directory.
        /// </summary>
        public string FullName { get; }

        /// <summary>
        /// Disposes of the temporary directory.
        /// </summary>
        public void Dispose()
        {
            Directory.Delete(FullName, recursive: true);
        }
    }
}