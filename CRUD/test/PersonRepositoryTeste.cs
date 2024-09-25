using System;
using CRUD.Repository;
using System.Collections.Generic;
using CRUD.entity;

namespace CRUD.Tests
{
    public class PersonRepositoryConsoleTests
    {
        //Unit Tests
        public static void GetAllPersons()
        {
            PersonRepository personRepository = new PersonRepository();
            List<Person> persons = personRepository.getAllPersons();
            
            if (persons != null && persons.Count > 0)
            {
                foreach (var person in persons)
                {
                    Console.WriteLine($"ID: {person.id}, Name: {person.firstName} {person.lastName}, Address: {person.address}, Gender: {person.gender}");
                }
            }
            else
            {
                Console.WriteLine("Not found");
            }
        }

        public static void GetPersonById()
        {
            PersonRepository personRepository = new PersonRepository();
            Person person = personRepository.getPersonById(1);
            
            if (person != null)
            {
                Console.WriteLine($"ID: {person.id}, Name: {person.firstName} {person.lastName}, Address: {person.address}, Gender: {person.gender}");
            }
            else
            {
                Console.WriteLine("Person not found.");
            }
        }

        public static void AddNewPerson()
        {
            PersonRepository personRepository = new PersonRepository();
            var newPerson = new Person
            {
                firstName = "John",
                lastName = "Doe",
                address = "Unknown",
                gender = "Male"
            };

            var result = personRepository.addNewPerson(newPerson);

            Console.WriteLine("Done created a new person.");
            if (result != null)
            {
                Console.WriteLine(
                    $"Person Added: ID: {result.id}, Name: {result.firstName} {result.lastName}, Address: {result.address}, Gender: {result.gender}");
            }
            else
            {
                Console.WriteLine("Failed to add person.");
            }
        }
        
        public static void UpdatePerson()
        {
            PersonRepository personRepository = new PersonRepository();
            var newPerson = new Person
            {   id = 14,
                firstName = "John",
                lastName = "Doe",
                address = "Unknown",
                gender = "Male"
            };

            var result = personRepository.UpdatePerson(newPerson);

            Console.WriteLine("Done updated");
            if (result != null)
            {
                Console.WriteLine(
                    $"Person Update ID: {result.id}, Name: {result.firstName} {result.lastName}, Address: {result.address}, Gender: {result.gender}");
            }
            else
            {
                Console.WriteLine("Failed to update person.");
            }
        }


        public static void deletePerson()
            {
                PersonRepository personRepository = new PersonRepository();
                personRepository.deletePerson(6);
            }
        }
    }

