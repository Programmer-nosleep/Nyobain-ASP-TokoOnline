using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using backend.Models;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : Controller
    {
        private IConfiguration _configuration;
        public AdminController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("AddUpdateMedicine")]
        public Response AddUpdateMedicine(Medicines medicines)
        {
            DAL dal = new DAL();
            SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            Response response = dal.AddUpdateMedicine(medicines, conn);
            return response;
        }

        [HttpGet]
        [Route("UsersList")]
        public Response UserLists(Users users)
        {
            DAL dal = new DAL();
            SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            Response response = dal.UserLists(users, conn);
            return response;
        }
    }
}