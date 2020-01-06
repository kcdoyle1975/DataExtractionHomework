using DataCommunication;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace HomeworkConsole
{
    [ExcludeFromCodeCoverage]
    class Program
    {
        static async Task Main(string[] args)
        {
            string directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var files = Directory.GetFiles(directory, "*.*", SearchOption.AllDirectories);

            var dataReader = new DataFileReader(new FileStreamReader());

            IList<Model> models = await dataReader.ReadFiles(files);

            Console.WriteLine("Output1:");
            foreach (var model in models.OrderBy(_ => _.LastName).OrderBy(_ => _.Gender).ToList())
            {
                Console.WriteLine($"LastName: {model.LastName}\t FirstName: {model.FirstName}\t Gender: {model.Gender}\t FavoriteColor: {model.FavoriteColor}\t DateOfBirth: {model.DateOfBirth.ToShortDateString()}");
            }

            Console.WriteLine("Output2:");
            foreach (var model in models.OrderBy(_ => _.DateOfBirth).ToList())
            {
                Console.WriteLine($"LastName: {model.LastName}\t FirstName: {model.FirstName}\t Gender: {model.Gender}\t FavoriteColor: {model.FavoriteColor}\t DateOfBirth: {model.DateOfBirth.ToShortDateString()}");
            }

            Console.WriteLine("Output3:");
            foreach (var model in models.OrderByDescending(_ => _.LastName).ToList())
            {
                Console.WriteLine($"LastName: {model.LastName}\t FirstName: {model.FirstName}\t Gender: {model.Gender}\t FavoriteColor: {model.FavoriteColor}\t DateOfBirth: {model.DateOfBirth.ToShortDateString()}");
            }
            Console.ReadKey();
        }
    }
}
