using cellShopSloution.ViewModel.Dtos.Users;
using cellShopSolution.ApiIntegration.Services.IService;
using cellShopSolution.ManagerApp.Services.IService;
using cellShopSolution.Utilities.Constants;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace cellShopSolution.ManagerApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserClient _userClient;
        private readonly IConfiguration _configuration;
        public LoginController(IUserClient userClient, IConfiguration configuration)
        {
            _userClient = userClient;
            _configuration = configuration;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
                return View();
            var result = await _userClient.AuthenticateAsync(loginRequest);
            var principal = this.ValidateToken(result.ResultObj);
            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                IsPersistent = true
            };
            HttpContext.Session.SetString(SystemConstant.AppSettings.DefaultLanguageId, _configuration["DefaultLanguageId"]);
            HttpContext.Session.SetString(SystemConstant.AppSettings.Token, result.ResultObj);
            await HttpContext.SignInAsync
                (
                     CookieAuthenticationDefaults.AuthenticationScheme,
                     principal,
                     authProperties
                );
            return RedirectToAction("Index", "Home");
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
