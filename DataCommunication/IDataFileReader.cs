using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataCommunication
{
    /// <summary>
    /// Reads model records from data files based on their file type.
    /// </summary>
    public interface IDataFileReader
    {
        /// <summary>
        /// Reads a list of files and returns a list of Models
        /// </summary>
        /// <param name="filePaths">The files to be read</param>
        /// <returns>A Model list extracted from the files</returns>
        Task<List<Model>> ReadFiles(string[] files);
    }
}