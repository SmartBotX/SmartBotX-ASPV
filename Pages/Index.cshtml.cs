using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static SmartBotX_V_2.Pages.Admin.NewsletterModel;

namespace SmartBotX_V_2.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public CL cl = new CL();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {            
        }
        public void OnPost()
        {
            cl.email = Request.Form["email"];
            if (cl.email.Length == 0)
            {
                errorMessage = "Please enter a email.";
                return;
            }

            try
            {
                String connectionstring = "Data Source=.\\SQLEXPRESS;Initial Catalog=smartbotx;Integrated Security=True;Pooling=False";
                using (SqlConnection connection = new(connectionstring))
                {
                    connection.Open();
                    String sql = "INSERT INTO newsletter" + "(email) VALUES" + "(@email);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@email", cl.email);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            cl.email = "";
            successMessage = "Thank you for subscribing!";
        }
    }
}