using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using backend.Models;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicinesController : Controller
    {
        private readonly IConfiguration _configuration;

        public MedicinesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("AddToCart")]
        public Response AddToCart(Cart cart)
        {
            DAL dal = new DAL();
            SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            Response response = dal.AddToCart(cart, conn);

            return response;
        }

        [HttpPost]
        [Route("PlaceOrder")]
        public Response PlaceOrder(Users users)
        {
            DAL dal = new DAL();
            SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            Response response = dal.PlaceOrder(users, conn);

            return response;
        }

        [HttpGet]
        [Route("OrderList")]
        public Response OrderList(Users users)
        {
            DAL dal = new DAL();
            SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            Response response = dal.OrderList(users, conn);

            return response;
        }
    }
}