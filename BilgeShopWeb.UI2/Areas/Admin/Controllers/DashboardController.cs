﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BilgeShopWeb.UI2.Areas.Admin.Controllers
{


    [Area("Admin")]
    [Authorize(Roles = "admin")] // Claim'lardaki claims.Add(new Claim(ClaimTypes.Role, user.UserType.ToString())); ile bağlantılı. (auth controller) - Rolü admin olmayan kullanıcıların buradaki metotlara istek atmasını engeller.
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }

}
