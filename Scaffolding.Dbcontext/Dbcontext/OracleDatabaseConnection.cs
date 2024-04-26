using Scaffolding.Dbcontext.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace Scaffolding.Dbcontext.Dbcontext
{
    public class OracleDatabaseConnection : IDatabaseConnection
    {
        private readonly string _connectionString;
        private OracleConnection _connection;

        public OracleDatabaseConnection(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Open()
        {
            try
            {
                _connection = new OracleConnection(_connectionString);
                _connection.Open();
                Console.WriteLine("Conexión a Oracle abierta.");
            }
            catch (OracleException ex)
            {
                Console.WriteLine($"Error al abrir la conexión a Oracle: {ex.Message}");
                throw;
            }
        }

        public void Close()
        {
            try
            {
                _connection.Close();
                Console.WriteLine("Conexión a Oracle cerrada.");
            }
            catch (OracleException ex)
            {
                Console.WriteLine($"Error al cerrar la conexión a Oracle: {ex.Message}");
                throw;
            }
        }

        public List<dynamic> ExecuteQuery(string query, CommandType commandType = CommandType.Text)
        {
            List<dynamic> results = new List<dynamic>();
            try
            {
                var command = new OracleCommand(query, _connection);
                command.CommandType = commandType;

                if (_connection.State != ConnectionState.Open)
                    _connection.Open();

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
                Console.WriteLine("Consulta ejecutada en Oracle.");
            }
            catch (OracleException ex)
            {
                Console.WriteLine($"Error al ejecutar la consulta en Oracle: {ex.Message}");
                throw;
            }
            return results;
        }
    }
}
