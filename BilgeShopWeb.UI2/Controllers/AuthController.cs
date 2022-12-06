using BilgeShop.Business2.Dto;
using BilgeShop.Business2.Services;
using BilgeShopWeb.UI2.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BilgeShop.WebUI.Controllers
{
    // Authentication ve Authorization işlemleri
    // (Kimlik Doğrulama ve Yetkilendirme)
    public class AuthController : Controller
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("kayit-ol")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("kayit-ol")]
        public IActionResult Register(RegisterViewModel formData)
        {
            if (!ModelState.IsValid)
            {
                return View(formData); // formData'yı geri göndermezsen , açılan view boş gelecek , yani kullanıcının girdiği bütün veriler silinecek.
            }

            var userDto = new UserDto()
            {
                FirstName = formData.FirstName.Trim(),
                LastName = formData.LastName.Trim(),
                Email = formData.Email.Trim(),
                Password = formData.Password.Trim()
            };


            var response = _userService.AddUser(userDto);


            if (response.IsSucceed)
            {
                return RedirectToAction("index", "home");
            }
            else
            {
                ViewBag.ErrorMessage = response.Message;
                return View(formData);
            }


        }

        public async Task<IActionResult> Login(LoginViewModel formData)
        {
            if(!ModelState.IsValid) {
                TempData["LoginMessage"] = "Kullanıcı Adı veya Şifre alanını doldurunuz.";
                return RedirectToAction("index", "home");


            }

            var loginDto = new LoginDto()
            {
                Email = formData.Email.Trim(),
                Password = formData.Password.Trim()
            };

            var user = _userService.Login(loginDto);

            if (user is null)
            {
                // Todo : Kullanıcı adı veya şifre hatalı mesajı ver
                return RedirectToAction("index", "home");
            }

            var claims = new List<Claim>();

            // Cookie(çerez) -> tarayıcıda saklanan dosyalar.
            // Claim -> Cookie'deki her bir bilgi.

            claims.Add(new Claim("id", user.Id.ToString()));
            claims.Add(new Claim("email", user.Email));
            claims.Add(new Claim("firstName", user.FirstName));
            claims.Add(new Claim("lastName", user.LastName));
            claims.Add(new Claim("userType", user.UserType.ToString()));

            claims.Add(new Claim(ClaimTypes.Role, user.UserType.ToString()));
            var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var autProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = new DateTimeOffset(DateTime.Now.AddHours(48))
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity), autProperties);

            return RedirectToAction("index", "home");


            // claims.Add(new Claim("password", user.Password)); BU ÇOK BÜYÜK BİR GÜVENLİK AÇIĞIDIR. PASSWORD KESİNLİKLE VE KESİNLİKLE CLAIM VEYA BAŞKA BİR YERDE ( VIEW UZERI ) TUTULMAZ.

        }
        // await keywordü ile çalışacaksanız ( yapılar birbirlerini beklesin. ) - Metodunuzu async Task<..> olarak tanımlamanız gerekir.
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(); // Oturumu kapat.
            return RedirectToAction("index", "home"); // Anasayfaya gönder.
            
        }
    }
}

