using decorArqui.Models;
using Microsoft.AspNetCore.Mvc;

namespace decorArqui.Controllers
{
    public class WhatsappController : Controller
    {
        public readonly Whatsapp model;

        public WhatsappController(Whatsapp model)
        {
            this.model = model;
        }

        public void IniciarConversa(string numeroCliente, string mensagem)
        {
            model.IniciarConversa(numeroCliente, mensagem);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
