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

        public IActionResult Index()
        {
            var persons = _personService.GetAllPersons();
            return View(persons);
        }

        [HttpGet]
        public IActionResult Person()
        {
            return View("post", new Person());
        }

        [HttpPost]
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

        [HttpPost]
        public IActionResult UpdatePerson(Person person)
        {
            if (ModelState.IsValid)
            {
                var updatedPerson = _personService.UpdatePerson(person);

                if (updatedPerson != null)
                {
                    TempData["SuccessMessage"] = "Person created successfully!";
                }

                {
                    TempData["ErrorMessage"] = "Invalid input data.";
                }
            }
            TempData["SuccessMessage"] = "Person created successfully!";
            return RedirectToAction("Index");
        }



        [HttpGet]
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


        [HttpDelete]
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
