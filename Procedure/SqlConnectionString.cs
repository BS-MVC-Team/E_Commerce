using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procedure
{
    public static class SqlConnectionString
    {
        public static string ConnectionString()
        {
            if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("SQLAZURECONNSTR_Commerce")))
            {
                return Environment.GetEnvironmentVariable("SQLAZURECONNSTR_Commerce");
            }
            else
            {
                //return ConfigurationManager.ConnectionStrings["db"].ConnectionString;
                //return "Server=buildschool.database.windows.net;Database=bs-team4;User Id=bsteam4;Password=@bsTp44D#;";
                return "data source=.; database=Commerce; integrated security=true";
            }
        }
    }
}
