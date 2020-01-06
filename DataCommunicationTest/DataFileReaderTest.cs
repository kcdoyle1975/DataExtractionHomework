using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DataCommunication;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DataCommunicationTest
{
    [TestClass]
    public class DataFileReaderTest
    {
        [TestMethod]
        public async Task ReadFilesCsvSuccessTest()
        {
            //Arrange
            MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes("comma,firstname,gender,favoritecolor,1/1/2001"));
            Mock<IFileStreamReader> fileStreamReader = new Mock<IFileStreamReader>();
            fileStreamReader.Setup(m => m.GetReader(It.IsAny<FileInfo>())).Returns(new StreamReader(memoryStream));
            var dataFileReader = new DataFileReader(fileStreamReader.Object);

            //Act
            List<Model> models = await dataFileReader.ReadFiles(new string[] { "something.csv" });

            //Assert
            Assert.AreEqual(models.Count, 1);
            Assert.AreEqual(models[0].LastName, "comma");
            Assert.AreEqual(models[0].FirstName, "firstname");
            Assert.AreEqual(models[0].Gender, "gender");
            Assert.AreEqual(models[0].FavoriteColor, "favoritecolor");
            Assert.AreEqual(models[0].DateOfBirth.ToShortDateString(), "1/1/2001");
        }

        [TestMethod]
        public async Task ReadFilesPsvSuccessTest()
        {
            //Arrange
            MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes("lastname|pipe|gender|favoritecolor|1/1/2001"));
            Mock<IFileStreamReader> fileStreamReader = new Mock<IFileStreamReader>();
            fileStreamReader.Setup(m => m.GetReader(It.IsAny<FileInfo>())).Returns(new StreamReader(memoryStream));
            var dataFileReader = new DataFileReader(fileStreamReader.Object);

            //Act
            List<Model> models = await dataFileReader.ReadFiles(new string[] { "something.psv" });

            //Assert
            Assert.AreEqual(models.Count, 1);
            Assert.AreEqual(models[0].LastName, "lastname");
            Assert.AreEqual(models[0].FirstName, "pipe");
            Assert.AreEqual(models[0].Gender, "gender");
            Assert.AreEqual(models[0].FavoriteColor, "favoritecolor");
            Assert.AreEqual(models[0].DateOfBirth.ToShortDateString(), "1/1/2001");
        }

        [TestMethod]
        public async Task ReadFilesSsvSuccessTest()
        {
            //Arrange
            MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes("lastname firstname space favoritecolor 1/1/2001"));
            Mock<IFileStreamReader> fileStreamReader = new Mock<IFileStreamReader>();
            fileStreamReader.Setup(m => m.GetReader(It.IsAny<FileInfo>())).Returns(new StreamReader(memoryStream));
            var dataFileReader = new DataFileReader(fileStreamReader.Object);

            //Act
            List<Model> models = await dataFileReader.ReadFiles(new string[] { "something.ssv" });

            //Assert
            Assert.AreEqual(models.Count, 1);
            Assert.AreEqual(models[0].LastName, "lastname");
            Assert.AreEqual(models[0].FirstName, "firstname");
            Assert.AreEqual(models[0].Gender, "space");
            Assert.AreEqual(models[0].FavoriteColor, "favoritecolor");
            Assert.AreEqual(models[0].DateOfBirth.ToShortDateString(), "1/1/2001");
        }

        [TestMethod]
        public async Task ReadFilesInvalidTest()
        {
            //Arrange
            MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes("lastname,firstname,gender,invalid,dateofbirth"));
            Mock<IFileStreamReader> fileStreamReader = new Mock<IFileStreamReader>();
            fileStreamReader.Setup(m => m.GetReader(It.IsAny<FileInfo>())).Returns(new StreamReader(memoryStream));
            var dataFileReader = new DataFileReader(fileStreamReader.Object);

            //Act
            List<Model> models = await dataFileReader.ReadFiles(new string[] { "something.invalid" });

            //Assert
            Assert.AreEqual(models.Count, 0);
        }
    }
}
