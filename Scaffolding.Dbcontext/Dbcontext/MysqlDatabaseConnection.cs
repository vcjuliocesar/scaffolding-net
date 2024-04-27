using MySql.Data.MySqlClient;
using Scaffolding.Dbcontext.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scaffolding.Dbcontext.Dbcontext
{
    public class MySqlDatabaseConnection : IDatabaseConnection
    {
        private readonly string _connectionString;
        private MySqlConnection _connection;

        public MySqlDatabaseConnection(string connectionString)
        {
            _connectionString = connectionString;
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
    }

}
