using System.Data;

namespace Scaffolding.Services.Auth.Contracts
{
    public interface IDatabaseConnection
    {
        void Open();
        void Close();
        List<dynamic> ExecuteQuery(string query, CommandType commandType = CommandType.Text);
    }
}
