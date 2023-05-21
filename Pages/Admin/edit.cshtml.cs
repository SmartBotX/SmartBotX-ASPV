using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static SmartBotX_V_2.Pages.Admin.NewsletterModel;
using System.Data.SqlClient;

namespace SmartBotX_V_2.Pages.Admin
{
    public class editModel : PageModel
    {
        public CL cl = new CL();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String Id = Request.Query["Id"];
            try
            {
                String connectionstring = "Data Source=.\\SQLEXPRESS;Initial Catalog=smartbotx;Integrated Security=True;Pooling=False";

                using (SqlConnection connection = new(connectionstring))
                {
                    connection.Open();
                    String sql = "SELECT * FROM newsletter WHERE Id=@Id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", Id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                cl.Id = "" + reader.GetInt32(0);
                                cl.email = reader.GetString(1);
                            }
                        }
                    }
                }
                }catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
        }
        public void OnPost()
        {
            try
            {
            cl.Id = Request.Form["id"];
            cl.email = Request.Form["email"];

            if (cl.email.Length == 0 || cl.Id.Length == 0 )
            {
                errorMessage = "All fields are required";
                return;
                }
                else if(cl.Id.Length == 0){
                    errorMessage = "Some Error has been detected, Please contact your IT personel to fix it.";
                    return;
                }
                String connectionstring = "Data Source=HTT-DELL-N4030\\SQLEXPRESS;Initial Catalog=smartbotx;Integrated Security=True;Pooling=False";
                using (SqlConnection connection = new(connectionstring))
                {
                    connection.Open();
                    String sql = "Update newsletter " + "Set email=@email " + "WHERE Id=@Id";  
                    using (SqlCommand command = new SqlCommand(sql, connection)){
                        command.Parameters.AddWithValue("@Id", cl.Id);
                        command.Parameters.AddWithValue("@email", cl.email);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Admin/Newsletter");
        }
    }
}
