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

        public Response UpdateProfile(Users users, SqlConnection conn)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_updateProfile", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@firstname", users.firstname);
            cmd.Parameters.AddWithValue("@lastname", users.lastname);
            cmd.Parameters.AddWithValue("@email", users.email);
            cmd.Parameters.AddWithValue("@password", users.password);
            // cmd.Parameters.AddWithValue()
            conn.Open();

            int i = cmd.ExecuteNonQuery();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Record updated successfully";
            }

            response.StatusCode = 100;
            response.StatusMessage = "Some error occured. Try again after sometime";
            return response;
        }

        public Response AddToCart(Cart cart, SqlConnection conn)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_AddToCart", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@user_id", cart.userId);
            cmd.Parameters.AddWithValue("@unit_price", cart.unitPrice);
            cmd.Parameters.AddWithValue("@medicine_id", cart.medicineId);
            cmd.Parameters.AddWithValue("@discount", cart.discount);
            cmd.Parameters.AddWithValue("@quantity", cart.quantity);
            cmd.Parameters.AddWithValue("@total_price", cart.totalPrice);

            conn.Open();
            int i = cmd.ExecuteNonQuery();
            conn.Close();

            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Item added successfully";
            }

            response.StatusCode = 100;
            response.StatusMessage = "Item could not be added";

            return response;
        }

        public Response PlaceOrder(Users users, SqlConnection conn)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_PlaceOrder", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", users.ID);
            // cmd.Parameters.AddWithValue("@");
            conn.Open();
            int i = cmd.ExecuteNonQuery();
            conn.Close();

            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Order has been placed successfully";
            }

            response.StatusCode = 100;
            response.StatusMessage = "Order could not be placed";

            return response;
        }

        public Response UserOrderList(Users users, SqlConnection conn)
        {
            Response response = new Response();
            SqlDataAdapter sqlDA = new SqlDataAdapter("sp_OrderList", conn);
            sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlDA.SelectCommand.Parameters.AddWithValue("@type", users.type);
            sqlDA.SelectCommand.Parameters.AddWithValue("@id", users.ID);
            // sqlDA.SelectCommand.Parameters.AddWithValue("@", users.);
            // sqlDA.SelectCommand.Parameters.AddWithValue("", users.);
            DataTable dataTable = new DataTable();

            sqlDA.Fill(dataTable);

            if (dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    Orders orders = new Orders();
                    orders.id = Convert.ToInt32(dataTable.Rows[0]["id"]);
                    orders.orderNo = Convert.ToString(dataTable.Rows[0]["order_no"]);
                }
            }

            return response;
        }
    }
}

