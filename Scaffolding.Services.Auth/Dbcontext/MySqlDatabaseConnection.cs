using MySql.Data.MySqlClient;
using Scaffolding.Services.Auth.Contracts;
using System.Data;

namespace Scaffolding.Services.Auth.Dbcontext
{
    public class MySqlDatabaseConnection : IDatabaseConnection
    {
        private readonly string _connectionString;
        private MySqlConnection _connection;

        public MySqlDatabaseConnection(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void Close()
        {
            try
            {
                _connection.Close();
                Console.WriteLine("Conexión a MySQL cerrada.");
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Error al cerrar la conexión a MySQL: {ex.Message}");
                throw;
            }
        }

        public List<dynamic> ExecuteQuery(string query, CommandType commandType = CommandType.Text)
        {
            List<dynamic> results = new List<dynamic>();
            try
            {
                using (_connection = new MySqlConnection(_connectionString))
                {
                    _connection.Open();
                    var command = new MySqlCommand(query, _connection);
                    command.CommandType = commandType;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var result = new System.Dynamic.ExpandoObject() as IDictionary<string, object>;
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                result.Add(reader.GetName(i), reader[i]);
                            }
                            results.Add(result);
                        }
                    }
                }
                Console.WriteLine("Consulta ejecutada en MySQL.");
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Error al ejecutar la consulta en MySQL: {ex.Message}");
                throw;
            }
            return results;
        }

        public void Open()
        {
            try
            {
                _connection = new MySqlConnection(_connectionString);
                _connection.Open();
                Console.WriteLine("Conexión a MySQL abierta.");
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Error al abrir la conexión a MySQL: {ex.Message}");
                throw;
            }
        }
    }
}
