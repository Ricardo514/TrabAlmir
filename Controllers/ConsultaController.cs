using Dez.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dez.Controllers
{
    public class ConsultaController : Controller
    {
        private readonly Contexto contexto;

        public ConsultaController(Contexto context)
        {
            contexto = context;
        }

        public ActionResult VndC ()
        {
            var vend = contexto.Vendas.Include(v => v.cliente)
                                       .Include(v => v.farmaceutico)
                                       .Include(v => v.produto)
                                        .Where(o=>o.produto.nome=="Paracetamol");

            return View(vend);
        }
          

    }
}
