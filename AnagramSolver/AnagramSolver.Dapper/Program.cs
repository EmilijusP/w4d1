using Microsoft.Data.SqlClient;
using Dapper;
using AnagramSolver.Dapper;

string connectionString = @"Server=.\SQLEXPRESS;Database=AnagramSolver_CF;Trusted_Connection=True;TrustServerCertificate=True";

using (var connection = new SqlConnection(connectionString))
{
    var query1 = "SELECT * FROM Words";
    var words = connection.Query<Word>(query1).ToList();

    foreach (var word in words)
    {
        Console.WriteLine($"{word.Id} {word.Value}");
    }
    
    var query2 = "INSERT INTO Words (Value) VALUES (@Value)";
    int rowsInserted = connection.Execute(query2, new { Value = "praktikantas" });
    Console.WriteLine($"Added {rowsInserted} rows.");

    var query3 = "UPDATE Words SET Value = @Value WHERE Id = @Id";
    int rowsUpdated = connection.Execute(query3, new { Value = "praktikavimas", Id = 9});
    Console.WriteLine($"Updated {rowsUpdated} rows.");
    
    var query4 = "DELETE FROM Words WHERE Id = @Id";
    int rowsDeleted = connection.Execute(query4, new { Id = 9 });
    Console.WriteLine($"Deleted {rowsDeleted} rows.");
}