using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DataCommunication
{
    /// <summary>
    /// Reads model records from data files based on their file type.
    /// Currently, it can only read csv, psv, and ssv
    /// </summary>
    public class DataFileReader : IDataFileReader
    {
        private IFileStreamReader _fileStreamReader;

        /// <summary>
        /// Constructory
        /// </summary>
        /// <param name="fileStreamReader">The FileStreamReader used to retrieve a stream.</param>
        public DataFileReader(IFileStreamReader fileStreamReader)
        {
            _fileStreamReader = fileStreamReader;
        }

        /// <summary>
        /// Reads a list of files and returns a list of Models
        /// </summary>
        /// <param name="filePaths">The files to be read</param>
        /// <returns>A Model list extracted from the files</returns>
        public async Task<List<Model>> ReadFiles(string[] filePaths)
        {
            char delimeter;
            List<Model> models = new List<Model>();

            foreach (string file in filePaths)
            {
                FileInfo fileInfo = new FileInfo(file);
                switch (fileInfo.Extension.ToLower())
                {
                    case ".csv":
                        delimeter = ',';
                        break;
                    case ".psv":
                        delimeter = '|';
                        break;
                    case ".ssv":
                        delimeter = ' ';
                        break;
                    default:
                        continue;
                }
                models.AddRange(await ReadFile(fileInfo, delimeter));
            }

            return models;
        }

        /// <summary>
        /// Reads a single file and extracts a Model list
        /// </summary>
        /// <param name="fileInfo">The FileInfo representing the file to be read</param>
        /// <param name="delimeter">The delimeter to be used to parse the data</param>
        /// <returns>A Model List extracted from the file</returns>
        private async Task<IList<Model>> ReadFile(FileInfo fileInfo, char delimeter)
        {
            List<Model> data = new List<Model>();
            using (var reader = _fileStreamReader.GetReader(fileInfo))
            {
                string line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        data.Add(new Model(line.Split(delimeter)));
                    }
                }
            }

            return data;
        }
    }
}
