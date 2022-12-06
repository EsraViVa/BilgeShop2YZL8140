using BilgeShop.Business2.Dto;
using BilgeShop.Business2.Services;
using BilgeShopWeb.UI2.Extensions;
using BilgeShopWeb.UI2.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BilgeShopWeb.UI2.Controllers
{
    public class AccountController : Controller
    {

        // UserManager bir class , bunun içerisinde metotlar var. Benim bu metotları kullanmam için, UserManager classını newlemem gerekiyor.
        // Newlemek yerine kullanabileceğim bir diğer yöntem ise Dependency Injection ile servis burada tanımlamak.
        // Interface üzerinden bir servis tanımlayıp bunu constuctor injection'da yaratıyorum.
        // Artık _userService. diyerek , bana sunulan hizmetleri/metotları kullanabilirim.

        // Dependency Injection ile oluşturuğum servis, istek gönderildiğinde newlenip , istek bitiminde silinecek.
        // eskiden using kullanılırdı. artık gerek yok.

        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("Hesabim")]
        public IActionResult Index()
        {
            var viewModel = new AccountViewModel()
            {
                FirstName = User.GetUserFirstName(),
                LastName = User.GetUserLastName(),
                Email = User.GetUserEmail(),
                EmailConfirm = User.GetUserEmail()
            };
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Update(AccountViewModel formData)
        {
            if (!ModelState.IsValid)
            {
                return View("index" , formData);
            }

            var userProfileEditDto = new UserProfileEditDto()
            {
                FirstName = formData.FirstName,
                LastName = formData.LastName,
                Email = formData.Email,
                Id = User.GetUserId(),
            };

            _userService.UpdateUser(userProfileEditDto);

            return RedirectToAction("index" , "home");



        }
    }
}
