using Microsoft.Data.Sqlite;
namespace CRUD.DataBase
{
    public class Connection
    {
        public static void connection()
        {
            string dbPath = @"C:\Users\CMT\RiderProjects\App\CRUD\DataBase\CRUD.db"; //absolut path
            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Connection established"); //open connection
                    Console.WriteLine($"Database path: {dbPath}"); //print path
                    
                    var createCommand = connection.CreateCommand();
                    createCommand.CommandText = @"
                        CREATE TABLE IF NOT EXISTS Person (
                            id INTEGER PRIMARY KEY AUTOINCREMENT UNIQUE,
                            firstName TEXT NOT NULL,
                            lastName TEXT NOT NULL,
                            address TEXT NOT NULL,
                            gender TEXT NOT NULL
                        );
                    ";
                    createCommand.ExecuteNonQuery(); //query
                    Console.WriteLine("Table 'Person' created successfully.");

                    var insertCommand = connection.CreateCommand();
                    insertCommand.CommandText = @"
                        INSERT INTO Person (firstName, lastName, address, gender) 
                        VALUES ('John', 'Doe', '123 Main St', 'Male');
                    ";
                    insertCommand.ExecuteNonQuery();
                    Console.WriteLine("Data inserted successfully.");
                    
                    
                    var selectCommand = connection.CreateCommand();
                    selectCommand.CommandText = @"
                        SELECT id, firstName, lastName, address, gender 
                        FROM Person;
                    ";

                    using (var reader = selectCommand.ExecuteReader())//reader inputStream
                    {
                        while (reader.Read())//read colunms
                        {
                            var id = reader.GetInt32(0);
                            var firstName = reader.GetString(1);
                            var lastName = reader.GetString(2);
                            var address = reader.GetString(3);
                            var gender = reader.GetString(4);

                            Console.WriteLine($"ID: {id}, Name: {firstName} {lastName}, Address: {address}, Gender: {gender}");
                            
                        }
                    }
                }
                catch (SqliteException e)
                {
                    Console.WriteLine("Error executing SQL: " + e);
                    throw;
                }
            }
        }
    }
}
