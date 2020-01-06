using DataCommunication;
using HomeworkApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace GuaranteedRateHomeworkApiTest
{
    [TestClass]
    public class RecordsControllerTest
    {
        private Mock<IDataFileReader> _dataFileReader = new Mock<IDataFileReader>();
        private Mock<IDataFileWriter> _dataFileWriter = new Mock<IDataFileWriter>();
        private RecordsController _controller;

        [TestInitialize]
        public void TestInitialize()
        {
            List<Model> models = GetModels();
            _dataFileReader.Setup(_ => _.ReadFiles(It.IsAny<string[]>())).ReturnsAsync(models);
            _dataFileWriter.Setup(_ => _.WriteModelToFile(It.IsAny<Model>(), It.IsAny<FileInfo>())).ReturnsAsync(true);
            _controller = new RecordsController(_dataFileReader.Object, _dataFileWriter.Object);
        }

        [TestMethod]
        public async Task GetUnsortedRecordsSuccessTest()
        {
            var results = await _controller.GetUnsortedRecords();

            Assert.AreEqual(results.Count, 5);
            Assert.AreEqual(results[0].LastName, "one");
            Assert.AreEqual(results[4].LastName, "four");
        }

        [TestMethod]
        public async Task GetRecordsSortByGenderSuccessTest()
        {
            var results = await _controller.GetRecordsSortByGender();

            Assert.AreEqual(results.Count, 5);
            Assert.AreEqual(results[0].LastName, "two");
            Assert.AreEqual(results[1].LastName, "two");
            Assert.AreEqual(results[2].LastName, "three");
            Assert.AreEqual(results[3].LastName, "one");
            Assert.AreEqual(results[4].LastName, "four");
        }

        [TestMethod]
        public async Task GetRecordsSortByDateOfBirthSuccessTest()
        {
            var results = await _controller.GetRecordsSortByDateOfBirth();

            Assert.AreEqual(results.Count, 5);
            Assert.AreEqual(results[0].LastName, "four");
            Assert.AreEqual(results[1].LastName, "three");
            Assert.AreEqual(results[2].FirstName, "dos");
            Assert.AreEqual(results[3].FirstName, "deux");
            Assert.AreEqual(results[4].LastName, "one");
        }

        [TestMethod]
        public async Task GetRecordsSortByNameSuccessTest()
        {
            var results = await _controller.GetRecordsSortByName();

            Assert.AreEqual(results.Count, 5);
            Assert.AreEqual(results[0].LastName, "four");
            Assert.AreEqual(results[1].LastName, "one");
            Assert.AreEqual(results[2].LastName, "three");
            Assert.AreEqual(results[3].FirstName, "deux");
            Assert.AreEqual(results[4].FirstName, "dos");
        }

        [TestMethod]
        public async Task AddRecordSuccessTest()
        {
            var result = await _controller.AddRecord(new Model() { FirstName = "Test" });

            Assert.IsTrue(result is OkObjectResult);
        }

        [TestMethod]
        public async Task AddRecordFailsTest()
        {
            Mock<IDataFileWriter> dataFileWriter = new Mock<IDataFileWriter>();
            dataFileWriter.Setup(_ => _.WriteModelToFile(It.IsAny<Model>(), It.IsAny<FileInfo>())).ReturnsAsync(false);
            var controller = new RecordsController(null, dataFileWriter.Object);

            var result = await controller.AddRecord(new Model());

            Assert.IsFalse(result is BadRequestObjectResult);
        }

        [TestMethod]
        public async Task AddRecordFailsBecauseOfExceptionTest()
        {
            Mock<IDataFileWriter> dataFileWriter = new Mock<IDataFileWriter>();
            dataFileWriter.Setup(_ => _.WriteModelToFile(It.IsAny<Model>(), It.IsAny<FileInfo>())).Throws(new IOException());
            var controller = new RecordsController(null, dataFileWriter.Object);

            var result = await controller.AddRecord(new Model());

            Assert.IsFalse(result is BadRequestObjectResult);
        }

        /// <summary>
        /// Gets a list of test models.
        /// </summary>
        /// <returns>List of Models</returns>
        private List<Model> GetModels()
        {
            return new List<Model>()
            {
                new Model()
                {
                    LastName = "one",
                    FirstName = "uno",
                    Gender = "male",
                    FavoriteColor = "green",
                    DateOfBirth = new DateTime(2001, 1, 1)
                },
                new Model()
                {
                    LastName = "two",
                    FirstName = "dos",
                    Gender = "female",
                    FavoriteColor = "blue",
                    DateOfBirth = new DateTime(1992, 2, 2)
                },
                new Model()
                {
                    LastName = "two",
                    FirstName = "deux",
                    Gender = "female",
                    FavoriteColor = "purple",
                    DateOfBirth = new DateTime(1992, 2, 3)
                },
                new Model()
                {
                    LastName = "three",
                    FirstName = "tres",
                    Gender = "female",
                    FavoriteColor = "red",
                    DateOfBirth = new DateTime(1983, 3, 3)
                },
                new Model()
                {
                    LastName = "four",
                    FirstName = "quatro",
                    Gender = "male",
                    FavoriteColor = "orange",
                    DateOfBirth = new DateTime(1974, 4, 4)
                }
            };
        }
    }
}
