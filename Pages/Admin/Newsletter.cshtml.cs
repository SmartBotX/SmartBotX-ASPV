using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static SmartBotX_V_2.Pages.IndexModel;

namespace SmartBotX_V_2.Pages.Admin
{
    public class NewsletterModel : PageModel
    {
        public List<CL> listClients = new List<CL>();
        public void OnGet()
        {
            try
            {
                String connectionstring = "Data Source=.\\SQLEXPRESS;Initial Catalog=smartbotx;Integrated Security=True;Pooling=False";

                using (SqlConnection connection = new(connectionstring))
                {
                    connection.Open();
                    String sql = "SELECT * FROM newsletter ";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CL c = new CL();
                                c.Id = "" + reader.GetInt32(0);
                                c.email = reader.GetString(1);

                                listClients.Add(c);
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
        public class CL
        {
            public String Id;
            public String email;
        }
    }
}

