using System.Linq;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParkingLotManagment.DataBase;
using ParkingLotManagment.Extensions;
using ParkingLotManagment.Models;
using System.Security.Claims;
using System.Threading.RateLimiting;

namespace ParkingLotManagment.Controllers
{
    //accceso a cualquiera
    [AllowAnonymous]
    public class AccesosController : Controller
    {
        private readonly ParkingLotManagementContext _context;
        private const string _Return_Url = "ReturnUrl";

        public AccesosController(ParkingLotManagementContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            //se guarda el returnUrl para redirigir a la pagina que estaba previamente en este caso de uso no tiene mucho sentido
            TempData[_Return_Url] = returnUrl;
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password, Rol rol)
        {
            string returnUrl = TempData[_Return_Url] as string;

            if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
            {
                Usuario usuario = null;

                if (rol == Rol.Cliente)
                {
                    usuario =_context.Clientes.FirstOrDefault(u => u.Username == username);
                }
                if (rol == Rol.Administrador)
                {
                    usuario =_context.Administradores.FirstOrDefault(u => u.Username == username);
                    
                }

                if(usuario!=null)
                {
                    var encriptedPassword = password.Encript();

                    if (encriptedPassword != null && usuario.Password.SequenceEqual(encriptedPassword))
                    {
                        //creando credenciales de usuario
                        ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

                        //claims para data necesaria en toda la aplicación
                        identity.AddClaim(new Claim(ClaimTypes.Name, username));
                        identity.AddClaim(new Claim(ClaimTypes.Role, rol.ToString()));
                        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()));
                        var nombrecompleto = usuario.Nombre + " " + usuario.Apellido;
                        identity.AddClaim(new Claim(ClaimTypes.GivenName, nombrecompleto));

                        ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                        //se realiza el login
                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal).Wait();

                        usuario.FechaUltimoAcceso = DateTime.Now;
                        _context.SaveChanges();

                        if(!string.IsNullOrWhiteSpace(returnUrl)) return Redirect(returnUrl);

                        return RedirectToAction(nameof(HomeController.Index), "Home");
                    }
                }
            }

            ViewBag.Error = "Usuario o contraseña incorrectos";
            ViewBag.UserName = username;
            ViewBag.ReturnUrl = returnUrl;
            TempData[_Return_Url] = returnUrl;

            return View();
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult NoAutorizado()
        {
            return View();
        }
    }
}
