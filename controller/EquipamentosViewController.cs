using Microsoft.AspNetCore.Mvc;

namespace EquipamentosApi.Controllers
{
    public class EquipamentosViewController : Controller
    {
        public IActionResult Index() => View();
        public IActionResult Cadastro() => View();
        public IActionResult Relatorio() => View();
    }
}
