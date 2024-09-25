using CRUD.entity;
using CRUD.Repository;


namespace CRUD.services;

public class PersonService : PersonInterfaceService

{
    private readonly PersonRepository _personRepository;
    
    public PersonService(PersonRepository personRepository)
    {
        _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
    }


    public PersonService()
    {
        _personRepository = new PersonRepository();
    }

    public List<Person> GetAllPersons()
    {
        return _personRepository.getAllPersons();
    }

    public Person? GetPersonById(long id)
    {
        return _personRepository.getPersonById(id);
    }

    public Person AddPerson(Person person)
    {
        return _personRepository.addNewPerson(person);
    }

    public Person? UpdatePerson(Person person)
    {
        return _personRepository.UpdatePerson(person);
    }

    public bool DeletePerson(long id)
    {
        return _personRepository.deletePerson(id);
    }
}