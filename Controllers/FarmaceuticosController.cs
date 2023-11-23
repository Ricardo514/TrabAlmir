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
    public class FarmaceuticosController : Controller
    {
        private readonly Contexto _context;

        public FarmaceuticosController(Contexto context)
        {
            _context = context;
        }

        // GET: Farmaceuticos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Farmaceuticos.ToListAsync());
        }

        // GET: Farmaceuticos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var farmaceutico = await _context.Farmaceuticos
                .FirstOrDefaultAsync(m => m.id == id);
            if (farmaceutico == null)
            {
                return NotFound();
            }

            return View(farmaceutico);
        }

        // GET: Farmaceuticos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Farmaceuticos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,nome,cpf")] Farmaceutico farmaceutico)
        {
            if (ModelState.IsValid)
            {
                _context.Add(farmaceutico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(farmaceutico);
        }

        // GET: Farmaceuticos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var farmaceutico = await _context.Farmaceuticos.FindAsync(id);
            if (farmaceutico == null)
            {
                return NotFound();
            }
            return View(farmaceutico);
        }

        // POST: Farmaceuticos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,nome,cpf")] Farmaceutico farmaceutico)
        {
            if (id != farmaceutico.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(farmaceutico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FarmaceuticoExists(farmaceutico.id))
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
            return View(farmaceutico);
        }

        // GET: Farmaceuticos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var farmaceutico = await _context.Farmaceuticos
                .FirstOrDefaultAsync(m => m.id == id);
            if (farmaceutico == null)
            {
                return NotFound();
            }

            return View(farmaceutico);
        }

        // POST: Farmaceuticos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var farmaceutico = await _context.Farmaceuticos.FindAsync(id);
            if (farmaceutico != null)
            {
                _context.Farmaceuticos.Remove(farmaceutico);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FarmaceuticoExists(int id)
        {
            return _context.Farmaceuticos.Any(e => e.id == id);
        }
    }
}
