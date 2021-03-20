using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace TesteFagronTech.Repositories
{
    public class Database
    {
        protected SqlConnection _conn = new SqlConnection("Server=DESKTOP-JR0T6JP;Database=TesteFagronTech;Integrated Security=SSPI;MultipleActiveResultSets=true");
    }
}
