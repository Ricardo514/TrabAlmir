using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dez.Models;
using Microsoft.AspNetCore.Authorization;

namespace Dez.Controllers
{
    [Authorize]
    public class VendasController : Controller
    {
        private readonly Contexto _context;

        public VendasController(Contexto context)
        {
            _context = context;
        }

        // GET: Vendas
        public async Task<IActionResult> Index()
        {
            var contexto = _context.Vendas.Include(v => v.cliente).Include(v => v.farmaceutico).Include(v => v.produto);
            return View(await contexto.ToListAsync());
        }

        // GET: Vendas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venda = await _context.Vendas
                .Include(v => v.cliente)
                .Include(v => v.farmaceutico)
                .Include(v => v.produto)
                .FirstOrDefaultAsync(m => m.id == id);
            if (venda == null)
            {
                return NotFound();
            }

            return View(venda);
        }

        // GET: Vendas/Create
        public IActionResult Create()
        {
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "id", "nome");
            ViewData["IdFarmaceutico"] = new SelectList(_context.Farmaceuticos, "id", "nome");
            ViewData["IdProduto"] = new SelectList(_context.Produtos, "id", "nome");
            return View();
        }

        // POST: Vendas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,IdFarmaceutico,IdProduto,IdCliente,qntd,total")] Venda venda)
        {
            if (ModelState.IsValid)
            {
                Produto produto = _context.Produtos.Find(venda.IdProduto);
                produto.estoque = produto.estoque - venda.qntd;
                venda.total = venda.qntd * produto.valor;
                _context.Update(produto);

               

                _context.Add(venda);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "id", "nome", venda.IdCliente);
            ViewData["IdFarmaceutico"] = new SelectList(_context.Farmaceuticos, "id", "nome", venda.IdFarmaceutico);
            ViewData["IdProduto"] = new SelectList(_context.Produtos, "id", "nome", venda.IdProduto);
            return View(venda);
        }

        // GET: Vendas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venda = await _context.Vendas.FindAsync(id);
            if (venda == null)
            {
                return NotFound();
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "id", "nome", venda.IdCliente);
            ViewData["IdFarmaceutico"] = new SelectList(_context.Farmaceuticos, "id", "nome", venda.IdFarmaceutico);
            ViewData["IdProduto"] = new SelectList(_context.Produtos, "id", "nome", venda.IdProduto);
            return View(venda);
        }

        // POST: Vendas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,IdFarmaceutico,IdProduto,IdCliente,qntd,total")] Venda venda)
        {
            if (id != venda.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(venda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendaExists(venda.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "id", "nome", venda.IdCliente);
            ViewData["IdFarmaceutico"] = new SelectList(_context.Farmaceuticos, "id", "nome", venda.IdFarmaceutico);
            ViewData["IdProduto"] = new SelectList(_context.Produtos, "id", "nome", venda.IdProduto);
            return View(venda);
        }

        // GET: Vendas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venda = await _context.Vendas
                .Include(v => v.cliente)
                .Include(v => v.farmaceutico)
                .Include(v => v.produto)
                .FirstOrDefaultAsync(m => m.id == id);
            if (venda == null)
            {
                return NotFound();
            }

            return View(venda);
        }

        // POST: Vendas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venda = await _context.Vendas.FindAsync(id);
            if (venda != null)
            {
                _context.Vendas.Remove(venda);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendaExists(int id)
        {
            return _context.Vendas.Any(e => e.id == id);
        }
    }
}
