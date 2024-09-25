namespace CRUD.entity;

public class PersonView
{
    
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string? address { get; set; }
        public string? gender { get; set; }
    
  
        public PersonView() { }

        public PersonView(string? firstName, string? lastName, string? address, string? gender)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.address = address;
            this.gender = gender;
        }
}

