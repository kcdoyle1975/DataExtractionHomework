using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DataCommunication;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DataCommunicationTest
{
    [TestClass]
    public class DataFileWriterTest
    {
        [TestMethod]
        public async Task GetWriterSuccessTest()
        {
            //Arrange
            var model = new Model()
            {
                LastName = "lastname",
                FirstName = "firstname",
                Gender = "gender",
                FavoriteColor = "favoritecolor",
                DateOfBirth = DateTime.Today
            };
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{model.LastName},{model.FirstName},{model.Gender},{model.FavoriteColor},{model.DateOfBirth.ToShortDateString()}");
            MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(sb.ToString()));
            Mock<IFileStreamWriter> fileStreamWriter = new Mock<IFileStreamWriter>();
            fileStreamWriter.Setup(m => m.GetWriter(It.IsAny<FileInfo>())).Returns(new StreamWriter(memoryStream));
            var dataFileWriter = new DataFileWriter(fileStreamWriter.Object);

            //Act
            bool didItWrite = await dataFileWriter.WriteModelToFile(model, null);

            //Assert
            Assert.IsTrue(didItWrite);
        }

        [TestMethod]
        public async Task GetWriterFailsTest()
        {
            //Arrange
            Mock<IFileStreamWriter> fileStreamWriter = new Mock<IFileStreamWriter>();
            fileStreamWriter.Setup(m => m.GetWriter(It.IsAny<FileInfo>())).Returns<StreamWriter>(null);
            var dataFileWriter = new DataFileWriter(fileStreamWriter.Object);

            //Act
            bool didItWrite = await dataFileWriter.WriteModelToFile(null, null);

            //Assert
            Assert.IsFalse(didItWrite);
        }
    }
}
