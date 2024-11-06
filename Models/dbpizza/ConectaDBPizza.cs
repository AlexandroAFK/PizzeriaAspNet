using MySql.Data.MySqlClient;

namespace Proyecto4A.Models.dbpizza
{
    public class ConectaBDPizza
    {
        public static MySqlConnection con;
        private static string server = "mysql";
        private static string bd = "dbpizza";
        private static string usuario = "root";
        private static string clave = "rootpass";
        public static string url = "";

        public static MySqlConnection Abrir()
        {
            try
            {
                // url = "SERVER=" + server + ";" + "DATABASE=" + bd + ";" +
                //     "UID=" + usuario + ";" + "PASSWORD=" + clave + ";" +
                //     "SSLMODE=none";

                url = "SERVER=" + server + ";" + "DATABASE=" + bd + ";" +
                    "UID=" + usuario + ";" + "PASSWORD=" + clave + ";" +
                    "SSLMODE=none;" + "AllowPublicKeyRetrieval=True;";

                con = new MySqlConnection(url);
                con.Open();

                return con;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void cerrar()
        {
            try
            {
                if (con != null)
                    con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}