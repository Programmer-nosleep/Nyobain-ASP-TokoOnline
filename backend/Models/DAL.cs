using Microsoft.AspNetCore.Authentication.OAuth;
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

        public Response OrderList(Users users, SqlConnection conn)
        {
            Response response = new Response();
            List<Orders> listOrder = new List<Orders>();
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
                    orders.id = Convert.ToInt32(dataTable.Rows[i]["id"]);
                    orders.orderNo = Convert.ToString(dataTable.Rows[i]["order_no"]);
                    orders.orderTotal = Convert.ToDecimal(dataTable.Rows[i]["order_status"]);
                    listOrder.Add(orders);
                }

                if (listOrder.Count > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "Order details fetched";
                    response.listOrders = listOrder;
                }

                response.StatusCode = 100;
                response.StatusMessage = "Order details are not available";
                response.listOrders = null;
            }

            return response;
        }

        public Response UserLists(Users users, SqlConnection conn)
        {
            Response response = new Response();
            List<Users> listUsers = new List<Users>();
            SqlDataAdapter sqlDA = new SqlDataAdapter("sp_UserList", conn);
            sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;


            DataTable dataTable = new DataTable();
            sqlDA.Fill(dataTable);

            if (dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    Users user = new Users();
                    user.ID = Convert.ToInt32(dataTable.Rows[i]["id"]);
                    user.firstname = Convert.ToString(dataTable.Rows[i]["firstname"]);
                    user.lastname = Convert.ToString(dataTable.Rows[i]["lastname"]);
                    user.password = Convert.ToString(dataTable.Rows[i]["password"]);
                    user.email = Convert.ToString(dataTable.Rows[i]["email"]);
                    user.fund = Convert.ToDecimal(dataTable.Rows[i]["fund"]);
                    user.status = Convert.ToInt32(dataTable.Rows[i]["status"]);
                    user.createdAt = Convert.ToDateTime(dataTable.Rows[i]["created_at"]);

                    listUsers.Add(user);
                }

                if (listUsers.Count > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "User details fetched";
                    response.listUsers = listUsers;
                }

                response.StatusCode = 100;
                response.StatusMessage = "User details are not available";
                response.listOrders = null;
            }

            return response;
        }

        public Response AddUpdateMedicine(Medicines medicines, SqlConnection conn)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_AddUpdateMedicine");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@name", medicines.name);
            cmd.Parameters.AddWithValue("@manufacturer", medicines.manufaturer);
            cmd.Parameters.AddWithValue("@unit_price", medicines.unitPrice);
            cmd.Parameters.AddWithValue("@discount", medicines.discount);
            cmd.Parameters.AddWithValue("@quantity", medicines.quantity);
            cmd.Parameters.AddWithValue("@exp_date", medicines.expDate);
            cmd.Parameters.AddWithValue("@img_url", medicines.imgURL);
            cmd.Parameters.AddWithValue("@status", medicines.status);
            cmd.Parameters.AddWithValue("@type", medicines.type);

            conn.Open();
            int i = cmd.ExecuteNonQuery();
            conn.Close();

            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Medicine inserted successfully";
            }

            response.StatusCode = 100;
            response.StatusMessage = "Medicine did not save item. try again";

            return response;
        }
    }
}

