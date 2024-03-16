﻿using Cokkie_Cerez_.Models;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Cokkie_Cerez_.Controllers
{
    public class HomeController : Controller
    {
        public IHttpContextAccessor _context;
       

        public HomeController(IHttpContextAccessor context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            // Atılan cookie'i okuma


            UserViewModel model = new UserViewModel();
            if (_context.HttpContext.Request.Cookies["UserName"] != null)
            {
                string username = _context.HttpContext.Request.Cookies["UserName"].ToString();
                string password = _context.HttpContext.Request.Cookies["Password"].ToString();


                model.Username = username;
                model.Password = password;
            }
            

            return View(model);
        }
        [HttpPost]
        public IActionResult Index(UserViewModel model)
        {
            // Parametre olarak gelen verileri cookie atıyorum

            if (model.Remember)
            {
                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddMinutes(10);
                option.Domain = "wissenhost.com";
                option.Secure = true;
                

               _context.HttpContext.Response.Cookies.Append("UserName", model.Username, option);
                _context.HttpContext.Response.Cookies.Append("Password", model.Password, option);
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}