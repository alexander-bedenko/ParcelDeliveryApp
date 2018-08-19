using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using ParcelDelivery.BLL.DTO;
using ParcelDelivery.BLL.Services;
using ParcelDelivery.Models;

namespace ParcelDelivery.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                var regUser = _userService.FindUser(Mapper.Map<UserDTO>(user).Login);
                if (regUser != null)
                {
                    ModelState.AddModelError("", "Такой пользователь уже существует.");
                }
                else
                {
                    _userService.RegisterUser(Mapper.Map<UserDTO>(user));
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(user);
        }

        [Authorize]
        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(UserViewModel user)
        {
            user.Login = User.Identity.Name;
            ModelState.Remove("Login");
            if (ModelState.IsValid && user.Login != null)
            {
                _userService.EditUser(Mapper.Map<UserDTO>(user));
                return RedirectToAction("Index", "Home");
            }
            return View(user);
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserViewModel user)
        {
            var userAuth = _userService.AutheticateUser(user.Login, user.Password);
            if (userAuth != null)
            {
                FormsAuthentication.SetAuthCookie(user.Login, true);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Имя пользователя или пароль не верны.");
            }

            return View(user);
        }

        [Authorize(Users = "Admin")]
        public ActionResult Admin()
        {
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}