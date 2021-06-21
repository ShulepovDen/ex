using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace WindowsFormsApp1
{
    public class globalClass
    {
        public static MySqlConnection connection = new MySqlConnection("server=127.0.0.1;user=root;password=root;database=exam;sslmode=none");
        //convert zero datetime=True
        public class agent
        {
            public  int id { get; set; }
            public  string name { get; set; }
            public string file { get; set; }
            public string type { get; set; }
        }
        public static List<agent> listAgent = new List<agent>();
        public static List<int> listSelected = new List<int>();
    }
}
