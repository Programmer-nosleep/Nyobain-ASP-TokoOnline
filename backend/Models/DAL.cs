using Microsoft.Data.SqlClient;
using System.Data;

namespace backend.Models
{
    public class DAL
    {
        public Response register(Users users, SqlConnection conn)
        {
            Response response = new Response();
            SqlCommand command = new SqlCommand("sp_register", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@firstname", users.firstname);
            command.Parameters.AddWithValue("@lastname", users.lastname);
            command.Parameters.AddWithValue("@password", users.password);
            command.Parameters.AddWithValue("@email", users.email);
            command.Parameters.AddWithValue("@fund", users.fund);
            // support optional 'type' property on Users without requiring model change
            object typeValue = DBNull.Value;
            var tprop = users.GetType().GetProperty("type")
                        ?? users.GetType().GetProperty("Type")
                        ?? users.GetType().GetProperty("user_type")
                        ?? users.GetType().GetProperty("UserType");
            if (tprop != null)
            {
                var v = tprop.GetValue(users);
                typeValue = v ?? DBNull.Value;
            }
            command.Parameters.AddWithValue("@type", typeValue);
            command.Parameters.AddWithValue("@status", users.status);
            command.Parameters.AddWithValue("@created_at", users.createdAt ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@updated_at", users.updatedAt ?? (object)DBNull.Value);

            return response;
        }
    }
}
