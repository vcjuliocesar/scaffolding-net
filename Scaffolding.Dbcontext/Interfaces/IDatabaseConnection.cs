using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scaffolding.Dbcontext.Interfaces
{
    public interface IDatabaseConnection
    {
        void Open();
        void Close();
        List<dynamic> ExecuteQuery(string query, CommandType commandType = CommandType.Text);
    }
}
