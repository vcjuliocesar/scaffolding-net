using Scaffolding.Dbcontext.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scaffolding.Dbcontext.Dbcontext
{
    public class SqliteDatabaseConnection : IDatabaseConnection
    {
        private readonly string _connectionString;
        private SQLiteConnection _connection;

        public SqliteDatabaseConnection(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Open()
        {
            try
            {
                _connection = new SQLiteConnection(_connectionString);
                _connection.Open();
                Console.WriteLine("Conexión a SQLite abierta.");
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"Error al abrir la conexión a SQLite: {ex.Message}");
                throw;
            }
        }

        public void Close()
        {
            try
            {
                _connection.Close();
                Console.WriteLine("Conexión a SQLite cerrada.");
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"Error al cerrar la conexión a SQLite: {ex.Message}");
                throw;
            }
        }

        public List<dynamic> ExecuteQuery(string query, CommandType commandType = CommandType.Text)
        {
            List<dynamic> results = new List<dynamic>();
            try
            {
                using (_connection = new SQLiteConnection(_connectionString))
                {
                    _connection.Open();
                    var command = new SQLiteCommand(query, _connection);
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
                Console.WriteLine("Consulta ejecutada en SQLite.");
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"Error al ejecutar la consulta en SQLite: {ex.Message}");
                throw;
            }
            return results;
        }

        public void InitializeDatabase()
        {
            //string directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Scaffolding.Dbcontext.Dbcontext");

            // Ruta del archivo de inicialización de la base de datos
           // string sqlFile = Directory.GetFiles(directoryPath, "*.sql").FirstOrDefault();

            // Verificar si el archivo existe
            //if (File.Exists(sqlFile))
            //{
            // Leer el script SQL desde el archivo
            //string script = File.ReadAllText(sqlFile);
            string script = "CREATE TABLE IF NOT EXISTS users (\r\n    Id INTEGER PRIMARY KEY,\r\n    Name TEXT NOT NULL,\r\n    Email TEXT NOT NULL,\r\n    Password TEXT NOT NULL,\r\n    CreatedAt DATETIME NOT NULL\r\n);\r\n";
                try
                {
                    // Ejecutar el script SQL para inicializar la base de datos
                    using (_connection = new SQLiteConnection(_connectionString))
                    {
                        _connection.Open();

                        using (var command = new SQLiteCommand(script, _connection))
                        {
                            command.ExecuteNonQuery();
                        }

                        Console.WriteLine("Base de datos inicializada correctamente.");
                    }
                }
                catch (SQLiteException ex)
                {
                    Console.WriteLine($"Error al inicializar la base de datos SQLite: {ex.Message}");
                    throw;
                }
            //}
            /*else
            {
                Console.WriteLine($"El archivo: {directoryPath} de inicialización de la base de datos no existe.");
            }*/
        }
    }

}
