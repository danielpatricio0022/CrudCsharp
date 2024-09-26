using CRUD.DataBase;
using CRUD.entity;
using Microsoft.Data.Sqlite;

namespace CRUD.Repository
{
    public class PersonRepository
    {
        string dbFileName = "CRUD.db";

        public string GetDbPath()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string dbPath = Path.Combine(currentDirectory, "DataBase", dbFileName);
            return dbPath;
        }

        public SqliteConnection Connect()
        {
            string path = GetDbPath(); 
            var connection = new SqliteConnection($"DataSource={path}");
            try
            {
                connection.Open();
                Console.WriteLine("Connection established");
            }
            catch (SqliteException e)
            {
                Console.WriteLine("Connection failed: " + e.Message);
                throw;
            }

            return connection;
        }

        public List<Person> getAllPersons()
        {
            List<Person> persons = new List<Person>();

            using (var connection = Connect())
            {
                SqliteCommand command = connection.CreateCommand();
                command.CommandText = @"SELECT id, firstName, lastName, address, gender FROM Person";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var person = new Person(
                            reader.GetInt64(0),
                            reader.GetString(1),
                            reader.GetString(2),
                            reader.GetString(3),
                            reader.GetString(4)
                        );
                        persons.Add(person);
                    }
                }
            }

            return persons;
        }


        public Person getPersonById(long id)
        {
            Person entity = null; // inicializando

            using (var connection = Connect())
            {
                try
                {
                    SqliteCommand command = connection.CreateCommand();
                    command.CommandText = @"SELECT * FROM Person WHERE id = @id";
                    command.Parameters.AddWithValue("@id", id); // parametro

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            entity = new Person(
                                reader.GetInt64(0),
                                reader.GetString(1),
                                reader.GetString(2),
                                reader.GetString(3),
                                reader.GetString(4)
                            );
                        }
                    }

                    if (entity == null)
                    {
                        throw new Exception("no data found.");
                    }
                }
                catch (SqliteException e)
                {
                    Console.WriteLine("database error: " + e);
                    throw;
                }
                catch (Exception e)
                {
                    Console.WriteLine("error: " + e.Message);
                    throw;
                }
            }

            return entity;
        }

        public Person addNewPerson(Person person)
        {
            Person entity = null;

            using (var connection = Connect())
            {
                try
                {
                    connection.Open();
                    SqliteCommand command = connection.CreateCommand();
                    command.CommandText = @"INSERT INTO Person (firstName, lastName, address, gender) 
                                            VALUES
                                            (@firstName, @lastName, @address, @gender); ";

                    command.Parameters.AddWithValue("@firstName", person.firstName);
                    command.Parameters.AddWithValue("@lastName", person.lastName);
                    command.Parameters.AddWithValue("@address", person.address);
                    command.Parameters.AddWithValue("@gender", person.gender);
                    command.ExecuteNonQuery();
                  

                    command.CommandText = "SELECT last_insert_rowid();";
                    long id = (long)command.ExecuteScalar();

                    entity = new Person(id, person.firstName, person.lastName, person.address, person.gender);
                }
                catch (Exception e)
                {

                    Console.WriteLine("error to insert a new person" + e.Message);
                }

                return entity;
            }
        }


        public Person UpdatePerson(Person person)
            {
                Person entity = null;

                using (var connection = Connect())
                {
                    try
                    {
                        connection.Open(); 
                        SqliteCommand command = connection.CreateCommand();
                        command.CommandText = @"
                                                UPDATE Person
                                                SET firstName = @firstName,
                                                    lastName = @lastName,
                                                    address = @address,
                                                    gender = @gender
                                                WHERE id = @id;";
                        command.Parameters.AddWithValue("@firstName", person.firstName ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@lastName", person.lastName ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@address", person.address ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@gender", person.gender ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@id", person.id);


                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            entity = person;
                            Console.WriteLine("Person updated ");
                        }
                        else
                        {
                            Console.WriteLine("not found person this id.");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error updating the person: " + e.Message);
                    }
                }

                return entity;
            }


        public bool deletePerson(long id)
        {
            using (var connection = Connect())
            {
                try    
                {
                    connection.Open(); 
                    SqliteCommand command = connection.CreateCommand();
                    command.CommandText = @" DELETE FROM Person WHERE id = @id; ";
                    command.Parameters.AddWithValue("@id", id);
                

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Person deleted ");
                        return true;
                    } 
                    {
                        Console.WriteLine("not found this person this id.");
                        return false;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error deleting person: " + e.Message);
                    return false;
                }
            }

           
        }







        }

    }
