using System.IO;
using System.Threading.Tasks;

namespace DataCommunication
{
    /// <summary>
    /// Used to write a model to a file.
    /// </summary>
    public interface IDataFileWriter
    {
        /// <summary>
        /// Writes a model to a file in a particular format.
        /// </summary>
        /// <param name="model">The model to be written</param>
        /// <param name="fileInfo">Represents the file to be written to</param>
        /// <returns>true if successfully written, otherwise false</returns>
        Task<bool> WriteModelToFile(Model model, FileInfo fileInfo);
    }
}