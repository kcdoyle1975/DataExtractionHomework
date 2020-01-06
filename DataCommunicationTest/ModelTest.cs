using DataCommunication;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataCommunicationTest
{
    [TestClass]
    public class ModelTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            //Arrange
            string[] properties = { "lastname", "firstname", "gender", "favoritecolor", "1/1/2001" };

            //Act
            var model = new Model(properties);

            //Assert
            Assert.AreEqual(model.LastName, "lastname");
            Assert.AreEqual(model.FirstName, "firstname");
            Assert.AreEqual(model.Gender, "gender");
            Assert.AreEqual(model.FavoriteColor, "favoritecolor");
            Assert.AreEqual(model.DateOfBirth.ToShortDateString(), "1/1/2001");
        }

        [TestMethod]
        public void ConstructorIncorrectNumberOfPropertiesTest()
        {
            //Arrange
            string[] properties = { "lastname"};

            //Act
            var model = new Model(properties);

            //Assert
            Assert.AreEqual(model.LastName, "lastname");
            Assert.AreEqual(model.FirstName, null);
            Assert.AreEqual(model.Gender, null);
            Assert.AreEqual(model.FavoriteColor, null);
            Assert.AreEqual(model.DateOfBirth.ToShortDateString(), "1/1/0001");
        }
    }
}
