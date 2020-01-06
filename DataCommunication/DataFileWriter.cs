using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DataCommunication
{
    /// <summary>
    /// Used to write a model to a file.
    /// Currently, it only writes to a CSV file
    /// </summary>
    public class DataFileWriter : IDataFileWriter
    {
        private IFileStreamWriter _fileStreamWriter;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fileStreamWriter">Retrieves the stream needed to write to the file</param>
        public DataFileWriter(IFileStreamWriter fileStreamWriter)
        {
            _fileStreamWriter = fileStreamWriter;
        }

        /// <summary>
        /// Writes a model to a file in a particular format.
        /// </summary>
        /// <param name="model">The model to be written</param>
        /// <param name="fileInfo">Represents the file to be written to</param>
        /// <returns>true if successfully written, otherwise false</returns>
        public async Task<bool> WriteModelToFile(Model model, FileInfo fileInfo)
        {
            try
            {
                using (var writer = _fileStreamWriter.GetWriter(fileInfo))
                {
                    StringBuilder csvFormattedString = new StringBuilder();
                    csvFormattedString.AppendLine($"{model.LastName},{model.FirstName},{model.Gender},{model.FavoriteColor},{model.DateOfBirth.ToShortDateString()}");
                    await writer.WriteAsync(csvFormattedString.ToString());
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
