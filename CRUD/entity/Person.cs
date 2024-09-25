namespace CRUD.entity;

public class Person
{
    public long id { get; set; } 
    public string? firstName { get; set; }
    public string? lastName { get;  set; }
    public string? address { get;  set; }
    public string? gender { get;  set; }

    public Person()
    {
        
    }
    
    public Person(long id)
    {
        this.id = id;

    }

    public Person(long id, string firstName, string lastName, string address, string gender)
    {
        this.id = id;
        this.firstName = firstName;
        this.lastName = lastName;
        this.address = address;
        this.gender = gender;
    }
    
    public Person(string firstName, string lastName, string address, string gender)
    {
        this.firstName = firstName;
        this.lastName = lastName;
        this.address = address;
        this.gender = gender;
    }
}