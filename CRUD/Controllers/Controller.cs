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
        public IActionResult AddPerson()
        {
            return View("post", new Person()); 
        }
        
        [HttpPost]
        public IActionResult AddPerson(Person person) 
        {
            if (ModelState.IsValid)
            {
                var createdPerson = _personService.AddPerson(person);
                if (createdPerson != null)
                {
                    TempData["SuccessMessage"] = "Person created successfully!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Failed to create person.";
                 }
            }
            else
            {
                TempData["ErrorMessage"] = "Invalid input data.";
            }

            return View("post", person); 
        
        }
        public string Welcome(string name)
        {
            return $"Welcome, {name}!";
        }
    }
}