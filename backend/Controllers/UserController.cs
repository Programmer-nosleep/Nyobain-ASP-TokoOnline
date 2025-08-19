using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using backend.Models;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("registration")]
        public Response Register(Users users)
        {
            Response response = new Response();
            DAL dal = new DAL();
            SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());

            response = dal.Register(users, conn);

            // TODO: implement registration logic (DB connection, insert, etc.)
            return response;
        }

        [HttpPost]
        [Route("login")]
        public Response Login(Users users)
        {
            DAL dal = new DAL();
            SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());

            Response response = new Response();
            response = dal.Login(users, conn);

            return response;
        }

        [HttpPost]
        [Route("ViewUser")]
        public Response ViewUser(Users users)
        {
            DAL dal = new DAL();
            SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());

            Response response = dal.ViewUser(users, conn);

            return response;
        }

        [HttpPost]
        [Route("UpdateProfile")]
        public Response UpdateProfile(Users users)
        {
            DAL dal = new DAL();
            SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());

            Response response = dal.UpdateProfile(users, conn);
            return response;
        }
    }
}
