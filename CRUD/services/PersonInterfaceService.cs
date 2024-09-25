using CRUD.entity;


namespace CRUD.services;

public interface PersonInterfaceService
{
    List<Person> GetAllPersons();
    Person? GetPersonById(long id);
    Person AddPerson(Person person);
    Person? UpdatePerson(Person person);
    bool DeletePerson(long id); 
}