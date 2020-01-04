using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCMOVIES.Models;
using MvcMovie.Data;

namespace MVCMOVIES.Controllers
{
    public class InfoesController : Controller
    {
        private readonly MvcMovieContext _context;

        public InfoesController(MvcMovieContext context)
        {
            _context = context;
        }

        // GET: Infoes
        public async Task<IActionResult> Index()
        {
            var mvcMovieContext = _context.Info.Include(i => i.User);
            return View(await mvcMovieContext.ToListAsync());
        }

        // GET: Infoes/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var info = await _context.Info
                .Include(i => i.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (info == null)
            {
                return NotFound();
            }

            return View(info);
        }

        // GET: Infoes/Create
        public IActionResult Create()
        {
            ViewData["Id"] = new SelectList(_context.User, "Id", "Id");
            return View();
        }

        // POST: Infoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,InitialAmount,TotalAmount")] Info info)
        {
            if (ModelState.IsValid)
            {
                _context.Add(info);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id"] = new SelectList(_context.User, "Id", "Id", info.Id);
            return View(info);
        }

        // GET: Infoes/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var info = await _context.Info.FindAsync(id);
            if (info == null)
            {
                return NotFound();
            }
            ViewData["Id"] = new SelectList(_context.User, "Id", "Id", info.Id);
            return View(info);
        }

        // POST: Infoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,InitialAmount,TotalAmount")] Info info)
        {
            if (id != info.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(info);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InfoExists(info.Id))
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
            ViewData["Id"] = new SelectList(_context.User, "Id", "Id", info.Id);
            return View(info);
        }

        // GET: Infoes/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var info = await _context.Info
                .Include(i => i.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (info == null)
            {
                return NotFound();
            }

            return View(info);
        }

        // POST: Infoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var info = await _context.Info.FindAsync(id);
            _context.Info.Remove(info);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InfoExists(long id)
        {
            return _context.Info.Any(e => e.Id == id);
        }
    }
}
