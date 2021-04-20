using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Products.Client.Interfaces.IRepository;
using Products.Client.Models;
using Products.Client.Models.ViewModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Products.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPieRepository _pieRepository;
        private readonly IAccountRepository _accountRepository;

        public HomeController(ILogger<HomeController> logger, IPieRepository pieRepository,
             IAccountRepository accountRepository)
        {
            _pieRepository = pieRepository;
            _logger = logger;
            _accountRepository = accountRepository;
        }

        public async Task<IActionResult> Index()
        {
            IndexViewModel listOfPies = new IndexViewModel()
            {
                PieList = (IEnumerable<Pie>)await _pieRepository.GetAllAsync(ApiDomain.PieAPIPath, HttpContext.Session.GetString("JWToken")),

            };
            return View(listOfPies);
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
        [HttpGet]
        public IActionResult Login()
        {
            User obj = new User();
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User obj)
        {
            User objUser = await _accountRepository.LoginAsync(ApiDomain.AccountAPIPath + "authenticate/", obj);
            if (objUser.Token == null)
            {
                return View();
            }

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Name, objUser.Username));
            identity.AddClaim(new Claim(ClaimTypes.Role, objUser.Role));
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);


            HttpContext.Session.SetString("JWToken", objUser.Token);
            TempData["alert"] = "Welcome " + objUser.Username;
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User obj)
        {
            bool result = await _accountRepository.RegisterAsync(ApiDomain.AccountAPIPath + "register/", obj);
            if (result == false)
            {
                return View();
            }
            TempData["alert"] = "Registeration Successful";
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.SetString("JWToken", "");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}