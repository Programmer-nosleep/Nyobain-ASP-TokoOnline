using Microsoft.Data.SqlClient;
using System.Data;

namespace backend.Models
{
    public class DAL
    {
        public Response Register(Users users, SqlConnection conn)
        {
            Response response = new Response();
            SqlCommand command = new SqlCommand("sp_register", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@firstname", users.firstname);
            command.Parameters.AddWithValue("@lastname", users.lastname);
            command.Parameters.AddWithValue("@password", users.password);
            command.Parameters.AddWithValue("@email", users.email);
            command.Parameters.AddWithValue("@fund", 0);
            command.Parameters.AddWithValue("@type", "Users");
            command.Parameters.AddWithValue("@status", "Pending");
            command.Parameters.AddWithValue("@created_at", users.createdAt);
            command.Parameters.AddWithValue("@updated_at", users.updatedAt);

            conn.Open();
            int i = command.ExecuteNonQuery();
            conn.Close();

            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "User registered successfully.";
            }

            response.StatusCode = 100;
            response.StatusMessage = "User registration failed";

            return response;
        }

        public Response Login(Users users, SqlConnection conn)
        {
            SqlDataAdapter sqlDA = new SqlDataAdapter("sp_login", conn);
            sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlDA.SelectCommand.Parameters.AddWithValue("@Email", users.email);
            sqlDA.SelectCommand.Parameters.AddWithValue("@Password", users.password);

            DataTable dataTable = new DataTable();
            sqlDA.Fill(dataTable);

            Response response = new Response();
            Users user = new Users();

            if (dataTable.Rows.Count > 0)
            {
                user.ID = Convert.ToInt32(dataTable.Rows[0]["id"]);
                user.firstname = Convert.ToString(dataTable.Rows[0]["firstname"]);
                user.lastname = Convert.ToString(dataTable.Rows[0]["lastname"]);
                user.email = Convert.ToString(dataTable.Rows[0]["email"]);
                user.fund = Convert.ToDecimal(dataTable.Rows[0]["fund"]);
                user.type = Convert.ToString(dataTable.Rows[0]["type"]);

                response.StatusCode = 200;
                response.StatusMessage = "User is valid";
                response.user = user;
            }

            response.StatusCode = 100;
            response.StatusMessage = "User is invalid";
            response.user = user;

            return response;
        }

        public Response ViewUser(Users users, SqlConnection conn)
        {
            SqlDataAdapter sqlDA = new SqlDataAdapter("p_viewUser", conn);
            sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlDA.SelectCommand.Parameters.AddWithValue("@id", users.ID);

            DataTable dataTable = new DataTable();
            sqlDA.Fill(dataTable);
            Response response = new Response();
            Users user = new Users();

            if (dataTable.Rows.Count > 0)
            {
                user.ID = Convert.ToInt32(dataTable.Rows[0]["id"]);
                user.firstname = Convert.ToString(dataTable.Rows[0]["firstname"]);
                user.lastname = Convert.ToString(dataTable.Rows[0]["lastname"]);
                user.password = Convert.ToString(dataTable.Rows[0]["password"]);
                user.email = Convert.ToString(dataTable.Rows[0]["email"]);
                user.fund = Convert.ToDecimal(dataTable.Rows[0]["fund"]);
                user.type = Convert.ToString(dataTable.Rows[0]["type"]);
                user.createdAt = Convert.ToDateTime(dataTable.Rows[0]["create_at"]);
                user.updatedAt = Convert.ToDateTime(dataTable.Rows[0]["updated_at"]);

                response.StatusCode = 200;
                response.StatusMessage = "User is exist.";
                response.user = user;
            }

            response.StatusCode = 100;
            response.StatusMessage = "User does not exist.";
            response.user = user;
            return response;
        }
    }
}
