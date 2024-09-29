using CRUD.entity;
using CRUD.services;
using Microsoft.AspNetCore.Mvc;

namespace CRUD.Controllers
{
    
  
    public class Crud : Controller
    {
        private readonly PersonService _personService;

        public Crud(PersonService personService)
        {
            _personService = personService;
        }
        
        public IActionResult Index() //Raiz ./localhost
        {
            var persons = _personService.GetAllPersons();
            return View(persons);
        }

        [HttpGet("person")] //person  Get(Add person)
        public IActionResult Person()
        {
            return View("post", new Person());
        }

        [HttpPost("person")] //person  Post(Add person) 
        public IActionResult Person(Person person)
        {
            if (ModelState.IsValid)
            {
                var createdPerson = _personService.AddPerson(person);
                if (createdPerson != null)
                {
                    TempData["SuccessMessage"] = "Person created successfully!";
                    return RedirectToAction("Index");
                }

                TempData["ErrorMessage"] = "Failed to create person.";
            }
            else
            {
                TempData["ErrorMessage"] = "Invalid input data.";
            }

            return View("post", person);
        }

        [HttpGet("getperson/{id}")] 
        public IActionResult GetPerson(long id)
        {
            return Ok(new { Id = id });
        }

        [HttpPut("updateperson/{id}")] //crud/updateperson Put(return json response)
        [Produces("application/json")]
        [Consumes("application/json")]
        public IActionResult UpdatePerson(long id, [FromBody] Person person) 
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Invalid input data.";
                return BadRequest(new { message = "Invalid input data." }); 
            }

            var existingPerson = _personService.GetPersonById(id);

            if (existingPerson == null)
            {
                TempData["ErrorMessage"] = "Person not found.";
                return NotFound(new { message = "Person not found." }); 
            }

            existingPerson.firstName = person.firstName;
            existingPerson.lastName = person.lastName;
            existingPerson.address = person.address;
            existingPerson.gender = person.gender;

            var updatedPerson = _personService.UpdatePerson(existingPerson);

            TempData["SuccessMessage"] = "Person updated successfully!";
            return Ok(updatedPerson); 
        }

        [HttpGet("getpersonbyid/{id}")]  // crud/getpersonbyid?id=3 QUERY PARAMETER
        public IActionResult GetPersonById(long id) 
        {
            try
            {
                var entityById = _personService.GetPersonById(id);

                if (entityById != null)
                {
                    TempData["SuccessMessage"] = "Person found!";
                    return View("byid", entityById);
                }

                TempData["ErrorMessage"] = "Person not found!";
                return RedirectToAction("Index");
            }
            catch (Exception ex) 
            {
                TempData["ErrorMessage"] = "An error occurred while retrieving the person: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpDelete] // Post(Teste Api funcionando backend/ postman) RETURN 500 view
        public IActionResult DeletePerson(long id)
        {
            var success = _personService.DeletePerson(id);
    
            if (success)
            {
                TempData["SuccessMessage"] = "Person deleted successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to delete person.";
            }

            return RedirectToAction("Index");
        }

        public string Welcome(string name)
        {
            return $"Welcome, {name}!";
        }
    }
}
