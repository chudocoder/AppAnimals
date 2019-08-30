using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppAnimals.Models;
using AppAnimals.DAL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace AppAnimals.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        public DataController(IConfiguration configuration) 
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// Get a animals.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET api/data/
        ///
        /// </remarks>
        /// <returns>Return all animals</returns>
        /// <response code="200">Return all animals</response>

        [HttpGet]
        public IActionResult Get()
        {

            using (var context = new AnimalsStoreContext(Configuration))
            {
                var animals = context.Animals
                                .Include(a_Id => a_Id.Id)
                                .Include(a_type => a_type.AnimalType)
                                .Include(a_color => a_color.AnimalSkinColor)
                                .Include(a_region => a_region.AnimalRegion)
                                .Include(a_location => a_location.AnimalLocation)
                                .Select(a => new
                                {
                                    a.Id,
                                    a.Name,
                                    AnimalType          = a.AnimalType.Name,
                                    AnimalSkinColor     = a.AnimalSkinColor.Name,
                                    AnimalRegion        = a.AnimalRegion.Name,
                                    AnimalLocation      = a.AnimalLocation.Name
                                });

                var output = JsonConvert.SerializeObject(animals);
                
                return new OkObjectResult(output);
            }
        }

        /// <summary>
        /// Get a animal.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET api/data/{id}
        ///     
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>A exist animal</returns>
        /// <response code="200">Return animal</response>

        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {

            using (var context = new AnimalsStoreContext(Configuration))
            {
                var animal = context.Animals
                                .Where(i => i.Id == id)
                                .Select(a => new
                                {
                                    a.Id,
                                    a.Name,
                                    AnimalType = a.AnimalType.Name,
                                    AnimalSkinColor = a.AnimalSkinColor.Name,
                                    AnimalRegion = a.AnimalRegion.Name,
                                    AnimalLocation = a.AnimalLocation.Name
                                });

                var output = JsonConvert.SerializeObject(animal);

                return new OkObjectResult(output);
            }
        }

        /// <summary>
        /// Add new animal.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST api/data
        ///     
        /// </remarks>
        /// <returns>Add new animal</returns>
        /// <response code="200">Animal added</response>

        [HttpPost]
        public void Post([FromBody] JObject value)
        {
            Animals animal = value.ToObject<Animals>();

            using (var context = new AnimalsStoreContext(Configuration))
            {
                context.Add(animal);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Update exist animal.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT api/data/5
        ///     
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>Update animal</returns>
        /// <response code="200">Animal Updated</response>

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] JObject value)
        {

            using (var context = new AnimalsStoreContext(Configuration))
            {
                Animals animal = value.ToObject<Animals>();

                var animalFromBd = context.Animals
                                .Include(a_type => a_type.AnimalType)
                                .Include(a_color => a_color.AnimalSkinColor)
                                .Include(a_region => a_region.AnimalRegion)
                                .Include(a_location => a_location.AnimalLocation)
                                .Where(i => i.Id == id)
                                .SingleOrDefault();

                animalFromBd.Name                   = animal.Name;
                animalFromBd.AnimalLocation.Name    = animal.AnimalLocation.Name;
                animalFromBd.AnimalRegion.Name      = animal.AnimalRegion.Name;
                animalFromBd.AnimalSkinColor.Name   = animal.AnimalSkinColor.Name;
                animalFromBd.AnimalType.Name        = animal.AnimalType.Name;

                context.Update(animalFromBd);
           

                context.SaveChanges();

            }
        }

        /// <summary>
        /// Delete exist animal.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE api/data/5
        ///     
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>Delete animal</returns>
        /// <response code="200">Animal deleted</response>

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            using (var context = new AnimalsStoreContext(Configuration))
            {
                context.Remove(context.Animals.Single(a => a.Id == id));
                context.SaveChanges();
            }
        }
    }
}
