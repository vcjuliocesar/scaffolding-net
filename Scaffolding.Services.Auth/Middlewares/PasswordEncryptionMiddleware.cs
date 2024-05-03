using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.IO;
using Microsoft.Extensions.Primitives;

namespace Scaffolding.Services.Auth.Middlewares
{
    public class PasswordEncryptionMiddleware
    {
        private readonly RequestDelegate _next;

        public PasswordEncryptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Intercepta las solicitudes entrantes
            if (context.Request.Method == "POST" && context.Request.Path == "/api/Auth/register" 
                && context.Request.ContentType?.ToLower().Contains("application/json") == true)
            {
                // Encripta el password antes de pasar la solicitud al siguiente middleware
                EncryptPassword(context);
            }

            // Intercepta las respuestas salientes
            await _next(context);

            if (context.Response.StatusCode == StatusCodes.Status200OK && context.Request.Path == "/api/Auth/register" 
                && context.Request.ContentType?.ToLower().Contains("application/json") == true)
            {
                // Desencripta el password en la respuesta antes de enviarla al cliente
                DecryptPassword(context);
            }
        }

        private void EncryptPassword(HttpContext context)
        {
            var request = context.Request;
            
            // Leer los datos del formulario
            var form = request.ReadFormAsync().GetAwaiter().GetResult();

            // Verificar si hay un campo de contraseña en el formulario y encriptarlo si es necesario
            if (form.ContainsKey("Password"))
            {
                var password = form["Password"];
                // Encriptar la contraseña
                var encryptedPassword = EncryptString(password);

                var updatedFormValues = new Dictionary<string, StringValues>(form)
                {
                    ["Password"] = new StringValues(encryptedPassword)
                };
                
                var updatedForm = new FormCollection(updatedFormValues);

                request.Form = updatedForm;
                //form = new FormCollection(form.ToDictionary(pair => pair.Key, pair => pair.Value));
                //form["Password"] = encryptedPassword;
            }

            // Actualizar los datos del formulario en la solicitud
            request.Form = form;
        }

        private void DecryptPassword(HttpContext context)
        {
            var response = context.Response;
            using (var reader = new StreamReader(response.Body))
            {
                response.Body.Seek(0, SeekOrigin.Begin);
                var responseBody = reader.ReadToEnd();
                var decryptedResponse = DecryptString(responseBody);
                response.Body.Seek(0, SeekOrigin.Begin);
                response.ContentLength = null;
                response.ContentType = "application/json";
                using (var writer = new StreamWriter(response.Body))
                {
                    writer.Write(decryptedResponse);
                    writer.Flush();
                }
            }
        }

        private string EncryptString(string plainText)
        {
            // Implementa la lógica para encriptar el password
            // Aquí te muestro un ejemplo básico con cifrado AES
            using (Aes aes = Aes.Create())
            {
                // Configura la clave secreta
                aes.Key = Encoding.UTF8.GetBytes("0123456789ABCDEF");
                aes.IV = Encoding.UTF8.GetBytes("ABCDEFGH01234567");

                // Cifra el texto plano
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(plainText);
                        }
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        private string DecryptString(string cipherText)
        {
            // Implementa la lógica para desencriptar el password
            // Aquí te muestro un ejemplo básico con cifrado AES
            using (Aes aes = Aes.Create())
            {
                // Configura la clave secreta
                aes.Key = Encoding.UTF8.GetBytes("0123456789ABCDEF");
                aes.IV = Encoding.UTF8.GetBytes("ABCDEFGH01234567");

                // Descifra el texto cifrado
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader reader = new StreamReader(cs))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
