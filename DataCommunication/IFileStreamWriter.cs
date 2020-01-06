using System.IO;

namespace DataCommunication
{
    /// <summary>
    /// Retrieves a StreamWriter
    /// </summary>
    public interface IFileStreamWriter
    {
        /// <summary>
        /// Retrieves a StreamWriter for the file
        /// </summary>
        /// <param name="fileInfo">Represents the file for the desired StreamWriter</param>
        /// <returns>StreamWriter</returns>
        StreamWriter GetWriter(FileInfo fileInfo);
    }
}