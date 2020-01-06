using System.IO;

namespace DataCommunication
{
    /// <summary>
    /// Retrieves a StreamWriter
    /// </summary>
    public class FileStreamWriter : IFileStreamWriter
    {
        /// <summary>
        /// Retrieves a StreamWriter for the file
        /// </summary>
        /// <param name="fileInfo">Represents the file for the desired StreamWriter</param>
        /// <returns>StreamWriter</returns>
        public StreamWriter GetWriter(FileInfo fileInfo)
        {
            FileStream fileStream = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Write);
            return new StreamWriter(fileStream);
        }
    }
}
