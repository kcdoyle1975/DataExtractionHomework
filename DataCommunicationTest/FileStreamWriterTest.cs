using System;
using System.IO;
using System.Reflection;
using DataCommunication;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataCommunicationTest
{
    [TestClass]
    public class FileStreamWriterTest
    {
        [TestMethod]
        public void GetWriterSuccessTest()
        {
            //Arrange
            IFileStreamWriter fileStreamWriter = new FileStreamWriter();
            string filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "TestFiles\\DummyFile.txt");
            FileInfo fileInfo = new FileInfo(filePath);

            //Act
            using (var streamWriter = fileStreamWriter.GetWriter(fileInfo))
            {
                //Assert
                Assert.IsTrue(streamWriter is StreamWriter, $"File: {fileInfo.FullName} could not be written to");
            }
        }
    }
}
