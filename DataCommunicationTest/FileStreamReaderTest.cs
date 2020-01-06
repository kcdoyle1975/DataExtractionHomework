using System;
using System.IO;
using System.Reflection;
using DataCommunication;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataCommunicationTest
{
    [TestClass]
    public class FileStreamReaderTest
    {
        [TestMethod]
        public void GetReaderSuccessTest()
        {
            //Arrange
            IFileStreamReader fileStreamReader = new FileStreamReader();
            string filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "TestFiles\\DummyFile.txt");
            FileInfo fileInfo = new FileInfo(filePath);

            //Act
            var streamReader = fileStreamReader.GetReader(fileInfo);

            //Assert
            Assert.IsTrue(streamReader is StreamReader);
        }
    }
}
