using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static SmartBotX_V_2.Pages.Admin.BuildModel;
using static SmartBotX_V_2.Pages.Admin.NewsletterModel;

namespace SmartBotX_V_2.Pages
{
    public class BotModel : PageModel
    {
        public BRL b = new BRL();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnPost()
        {
            b.email = Request.Form["email"];
            b.description = Request.Form["description"];
            if (b.email.Length == 0 || b.description.Length == 0 )
            {
                errorMessage = "All Fields Required.";
                return;
            }

            try
            {
                String connectionstring = "Data Source=.\\SQLEXPRESS;Initial Catalog=smartbotx;Integrated Security=True;Pooling=False";
                using (SqlConnection connection = new(connectionstring))
                {
                    connection.Open();
                    String sql = "INSERT INTO request " + "(email, description) VALUES" + "(@email, @description);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@email", b.email);
                        command.Parameters.AddWithValue("@description", b.description);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            b.email = "";
            b.description = "";
            successMessage = "Thank you for subscribing!";
            return;
        }
    }
}
