using System;

namespace DataCommunication
{
    public class Model
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Model()
        {
        }

        /// <summary>
        /// Constructor
        /// Creates the model based on a particular array of strings in a particular order
        /// </summary>
        /// <param name="properties">The list of values for the properties of the model</param>
        public Model(string[] properties)
        {
            try
            {
                LastName = properties[0];
                FirstName = properties[1];
                Gender = properties[2];
                FavoriteColor = properties[3];
                DateOfBirth = Convert.ToDateTime(properties[4]);
            }
            catch
            {
                //do nothing
                //allow model to be created with whatever data is available
            }
        }

        /// <summary>
        /// Last Name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// First Name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gender
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// Favorite color
        /// </summary>
        public string FavoriteColor { get; set; }

        /// <summary>
        /// Date of birth
        /// </summary>
        public DateTime DateOfBirth { get; set; }
    }
}
