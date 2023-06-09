using System.Data;
using System.Data.SqlClient;

namespace WebAPI
{
    public static class utilities
    {

        
        public static  class sql
        {
            private static string GetConnectingString()
            {

                string connectionString = "Server=localhost;Database=cohort11;Trusted_Connection=True; encrypt=false;";
                return connectionString;
            }
            private static SqlConnection con;
            private static SqlCommand cmd;

            private static  void establishConnection()
            {
                try
                {
                    con = new SqlConnection(GetConnectingString());
                    
                }
                catch (SqlException ex)
                {
                  Console.WriteLine(ex.Message);

                }
            }

            //Threading for handling database operations
            public static async
            //Threading for handling database operations
            Task<bool>
            Set(string query)
            {
                bool state;
                try
                {
                    establishConnection();
                    if (con.State != ConnectionState.Open)
                    {
                        await con.OpenAsync();
                    }
                    using (cmd = new SqlCommand(query, con))
                    {
                        await cmd.ExecuteNonQueryAsync();
                    }
                    Console.WriteLine("Executed Successfully");
                    con.Close();
                    state= true;
                }
                catch (SqlException ex)
                {
                    state = false;
                    Console.WriteLine(ex.Message);
                }
                return state;
            }

            public static DataTable Get(string query)
            {
                DataTable dt = new DataTable();
                try
                {
                    establishConnection();
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                    adapter.Fill(dt);
                    con.Close();

                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return dt;
            }
        }
    }

}
