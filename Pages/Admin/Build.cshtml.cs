using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace SmartBotX_V_2.Pages.Admin
{
    public class BuildModel : PageModel
    {
        public List<BRL> listClient = new List<BRL>();
        public void OnGet()
        {
            try
            {
                String connectionstring = "Data Source=.\\SQLEXPRESS;Initial Catalog=smartbotx;Integrated Security=True;Pooling=False";

                using (SqlConnection connection = new(connectionstring))
                {
                    connection.Open();
                    String sql = "SELECT * FROM request ";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                BRL b = new BRL();
                                b.Id = "" + reader.GetInt32(0);
                                b.email = reader.GetString(1);
                                b.description = reader.GetString(2);

                                listClient.Add(b);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
        public class BRL
        {
            public String Id;
            public String email;
            public String description;
        }
    }
}
