using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DataCommunication;
using Microsoft.AspNetCore.Mvc;

namespace HomeworkApi.Controllers
{
    /// <summary>
    /// Controller used for interaction with the model records
    /// </summary>
    [Route("api/[controller]")]
    public class RecordsController : Controller
    {
        private string _directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private string[] _files;
        private IDataFileReader _dataFileReader;
        private IDataFileWriter _dataFileWriter;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dataFileReader">DataFileReader used to read models from the files</param>
        /// <param name="dataFileWriter">DataFileWriter used to write models to the files</param>
        public RecordsController(IDataFileReader dataFileReader, IDataFileWriter dataFileWriter)
        {
            _dataFileReader = dataFileReader;
            _dataFileWriter = dataFileWriter;
            _files = Directory.GetFiles(_directory, "*.*", SearchOption.AllDirectories);
        }

        /// <summary>
        /// Gets all of the records unsorted.
        /// </summary>
        /// <returns>Returns an unsorted list of models</returns>
        [HttpGet]
        public async Task<IList<Model>> GetUnsortedRecords()
        {
            IList<Model> models = await _dataFileReader.ReadFiles(_files);
            return models;
        }

        /// <summary>
        /// Retrieves a list of models sorted by Gender
        /// </summary>
        /// <returns>Returns a sorted list of models</returns>
        [HttpGet("/gender")]
        public async Task<IList<Model>> GetRecordsSortByGender()
        {
            IList<Model> models = await _dataFileReader.ReadFiles(_files);
            return models.OrderBy(_ => _.Gender).ToList();
        }

        /// <summary>
        /// Retrieves a list of models sorted by Date of birth
        /// </summary>
        /// <returns>Returns a sorted list of models</returns>
        [HttpGet("/birthdate")]
        public async Task<IList<Model>> GetRecordsSortByDateOfBirth()
        {
            IList<Model> models = await _dataFileReader.ReadFiles(_files);
            return models.OrderBy(_ => _.DateOfBirth).ToList();
        }

        /// <summary>
        /// Retrieves a list of models sorted by First and Last names
        /// </summary>
        /// <returns>Returns a sorted list of models</returns>
        [HttpGet("/name")]
        public async Task<IList<Model>> GetRecordsSortByName()
        {
            IList<Model> models = await _dataFileReader.ReadFiles(_files);
            return models.OrderBy(_ => _.FirstName).OrderBy(_ => _.LastName).ToList();
        }

        /// <summary>
        /// Adds a new record to the files
        /// </summary>
        /// <param name="model">The model to write to the files</param>
        /// <returns>OK if successful and BadRequest if not</returns>
        [HttpPost]
        public async Task<IActionResult> AddRecord([FromBody]Model model)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(Path.Combine(_directory, "Comma.csv"));
                if (await _dataFileWriter.WriteModelToFile(model, fileInfo))
                    return Ok("Data successfully saved.");
            }
            catch
            {
                return BadRequest("Unable to save the data.");
            }
            return BadRequest("Unable to save the data.");
        }

    }
}
