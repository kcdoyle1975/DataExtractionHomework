using System.IO;

namespace DataCommunication
{
    /// <summary>
    /// Retrieves a StreamReader for a file
    /// </summary>
    public interface IFileStreamReader
    {
        /// <summary>
        /// Retrieves a StreamReader for the file passed in
        /// </summary>
        /// <param name="fileInfo">Represents the file to get a StreamReader for</param>
        /// <returns>StreamReader</returns>
        StreamReader GetReader(FileInfo fileInfo);
    }
}