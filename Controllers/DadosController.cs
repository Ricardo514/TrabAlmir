using Dez.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dez.Controllers
{
    public class DadosController : Controller
    {
        private readonly Contexto contexto;
        
        public DadosController (Contexto context)
        {
            contexto = context;

        }
        public IActionResult Cliente()
        {

            Random randNum = new Random();

            string[] Nome1 = { "Ricardo", "Alberto", "Lucas", "Eduardo", "Matheus" };
            string[] Nome2 = { "Nathalia", "Isadora", "Maria Eduarda", "Marie", "Paula" };
            string[] Cpf1 = { "038819470", "408717590", "368023130", "030260939", "36867169" };
            string[] Cpf2 = { "592043300", "160259360", "227270670", "659356540", "23048180" };
            string[] Cid1 = { "Assis", "Maracai", "Candido-Mota", "Florinea", "Tarumã" };
            string[] Cid2 = { "Londrina", "Marilia", "Garça", "Pedrinhas", "Cruzalia" };

            for (int i = 0; i < 10; i++)
            {
                Cliente cliente = new Cliente();
                cliente.nome = (i % 2 == 0) ? Nome1[i / 2] : Nome2[i / 2];
                cliente.cpf = (i % 2 == 0) ? Cpf1[i / 2] : Cpf2[i / 2];
                cliente.endereco = (i % 2 == 0) ? Cid1[i / 2] : Cid2[i / 2];
                contexto.Clientes.Add(cliente);

            }
            contexto.SaveChanges();

            return View(contexto.Clientes.OrderBy(o => o.nome).ToList());
        }
    }
}

