using cellShopSloution.ViewModel.Dtos.Users;
using cellShopSolution.ApiIntegration.Services.IService;
using cellShopSolution.Utilities.Constants;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace cellShopSolution.WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserClient _userClient;
        private readonly IConfiguration _configuration;
        public AccountController(IUserClient userClient, IConfiguration configuration)
        {
            _userClient = userClient;
            _configuration = configuration;
        }
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            if (!ModelState.IsValid)
                return View(registerRequest);
            var result = await _userClient.RegisterUserAsync(registerRequest);
            if (!result.IsSuccessed)
            {
                ModelState.AddModelError("", result.Message);
            }
            var loginResult = _userClient.AuthenticateAsync( new LoginRequest()
            {
                Password = registerRequest.Password,
                UserName =registerRequest.UserName,
                RememberMe= true
            });
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            if(!ModelState.IsValid)
            return View();
            var result = await _userClient.AuthenticateAsync(loginRequest);
            var principal = this.ValidateToken(result.ResultObj);
            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                IsPersistent = false,
            };
            HttpContext.Session.SetString(SystemConstant.AppSettings.Token, result.ResultObj);
            await HttpContext.SignInAsync
                (
                     CookieAuthenticationDefaults.AuthenticationScheme,
                     principal,
                     authProperties
                );
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("Token");
            return RedirectToAction("Login", "Account");
        }
        private ClaimsPrincipal ValidateToken(string jwtToken)
        {
            IdentityModelEventSource.ShowPII = true;
            SecurityToken validatedToken;
            TokenValidationParameters validationParameters = new TokenValidationParameters();
            validationParameters.ValidateLifetime = true;
            validationParameters.ValidAudience = _configuration["Tokens:Issuer"];
            validationParameters.ValidIssuer = _configuration["Tokens:Issuer"];
            validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out validatedToken);
            return principal;
        }
    }
}
