using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using AppAnimals.Models;

namespace AppAnimals.DAL
{
    public class CreateDB
    {
        public  IConfiguration Configuration { get; }

        public CreateDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void CreateDBSQl()
        {

            using (var context = new AnimalsStoreContext(Configuration))
            {
                // Creates the database if not exists
                if (context.Database.EnsureCreated() == false)
                {
                    // Adds a animal
                    var animal = new Animals
                    {
                        Name = "Elephant",
                        AnimalType = new AnimalType { Name = "mammals" },
                        AnimalLocation = new AnimalLocation { Name = "34.66361,-113.41417" },
                        AnimalSkinColor = new AnimalSkinColor { Name = "grey" },
                        AnimalRegion = new AnimalRegion { Name = "Kerala" }
                    };
                    context.Animals.Add(animal);
                    context.SaveChanges();
                }
            }
        }

    }
}
