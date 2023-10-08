
using RayTracing.Model;

namespace RayTracing.ModelFiles
{
    /// <summary>
    /// Parses model files, and produces an in-memory 3D model.
    /// </summary>
    internal interface IModelFileParser
    {
        /// <summary>
        /// Parses the specified file, and loads the 3D model from the file.
        /// </summary>
        Scene LoadFromFile(string fileName);
    }
}
