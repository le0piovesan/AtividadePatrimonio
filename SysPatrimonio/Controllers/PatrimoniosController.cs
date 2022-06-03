using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SysPatrimonio.Models;

namespace SysPatrimonio.Controllers
{
    public class PatrimoniosController : Controller
    {
        private readonly Context _context;

        public PatrimoniosController(Context context)
        {
            _context = context;
        }

        // GET: Patrimonios
        public async Task<IActionResult> Index()
        {

            List<DtoPatrimonio> lista = (from p in _context.Patrimonios
                                           join l in _context.Locais on p.idlocal equals l.id
                                           join c in _context.Categorias on p.idcategoria equals c.id
                                           join d in _context.Departamentos on p.iddepartamento equals d.id
                                           select new DtoPatrimonio
                                           {
                                               id = p.id,
                                               numetiqueta = p.numetiqueta,
                                               nomepatrimonio = p.nomepatrimonio,
                                               descricaopatrimonio = p.descricaopatrimonio,
                                               valorpatrimonio = p.valorpatrimonio,
                                               marcamodelo = p.marcamodelo,
                                               dataaquisicao = p.dataaquisicao,
                                               databaixa = p.databaixa,
                                               numnf = p.numnf,
                                               numserie = p.numserie,
                                               situacao = p.situacao
                                           }).ToList();

            return View(lista);

            //return _context.Patrimonios != null ? 
            //              View(await _context.Patrimonios.ToListAsync()) :
            //              Problem("Entity set 'Context.Patrimonios'  is null.");
        }

        // GET: Patrimonios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Patrimonios == null)
            {
                return NotFound();
            }

            var dbPatrimonio = await _context.Patrimonios
                .FirstOrDefaultAsync(m => m.id == id);
            if (dbPatrimonio == null)
            {
                return NotFound();
            }

            return View(dbPatrimonio);
        }

        // GET: Patrimonios/Create
        public IActionResult Create()
        {
            ViewBag.Categoria = new SelectList(_context.Categorias, "id", "nomecategoria");
            ViewBag.Local = new SelectList(_context.Locais, "id", "nomelocal");
            ViewBag.Departamento = new SelectList(_context.Departamentos, "id", "nomedepartamento");

            return View();
        }

        // POST: Patrimonios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,numetiqueta,nomepatrimonio,descricaopatrimonio,valorpatrimonio,marcamodelo,dataaquisicao,databaixa,numnf,numserie,situacao")] DbPatrimonio dbPatrimonio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dbPatrimonio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dbPatrimonio);
        }

        // GET: Patrimonios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Patrimonios == null)
            {
                return NotFound();
            }

            var dbPatrimonio = await _context.Patrimonios.FindAsync(id);
            if (dbPatrimonio == null)
            {
                return NotFound();
            }
            return View(dbPatrimonio);
        }

        // POST: Patrimonios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,numetiqueta,nomepatrimonio,descricaopatrimonio,valorpatrimonio,marcamodelo,dataaquisicao,databaixa,numnf,numserie,situacao")] DbPatrimonio dbPatrimonio)
        {
            if (id != dbPatrimonio.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dbPatrimonio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DbPatrimonioExists(dbPatrimonio.id))
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
            return View(dbPatrimonio);
        }

        // GET: Patrimonios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Patrimonios == null)
            {
                return NotFound();
            }

            var dbPatrimonio = await _context.Patrimonios
                .FirstOrDefaultAsync(m => m.id == id);
            if (dbPatrimonio == null)
            {
                return NotFound();
            }

            return View(dbPatrimonio);
        }

        // POST: Patrimonios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Patrimonios == null)
            {
                return Problem("Entity set 'Context.Patrimonios'  is null.");
            }
            var dbPatrimonio = await _context.Patrimonios.FindAsync(id);
            if (dbPatrimonio != null)
            {
                _context.Patrimonios.Remove(dbPatrimonio);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DbPatrimonioExists(int id)
        {
          return (_context.Patrimonios?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
