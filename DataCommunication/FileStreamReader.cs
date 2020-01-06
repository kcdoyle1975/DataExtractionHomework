using System.IO;

namespace DataCommunication
{
    /// <summary>
    /// Retrieves a StreamReader for a file
    /// </summary>
    public class FileStreamReader : IFileStreamReader
    {
        /// <summary>
        /// Retrieves a StreamReader for the file passed in
        /// </summary>
        /// <param name="fileInfo">Represents the file to get a StreamReader for</param>
        /// <returns>StreamReader</returns>
        public StreamReader GetReader(FileInfo fileInfo)
        {
            FileStream fileStream = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read);
            return new StreamReader(fileStream);
        }
    }
}
