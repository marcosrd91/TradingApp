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
    public class OperationsController : Controller
    {
        private readonly MvcMovieContext _context;

        public OperationsController(MvcMovieContext context)
        {
            _context = context;
        }

        // GET: Operations
        public async Task<IActionResult> Index()
        {
            var mvcMovieContext = _context.Operations.Include(o => o.User);
            return View(await mvcMovieContext.ToListAsync());
     
        }

        // GET: Operations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operations = await _context.Operations
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (operations == null)
            {
                return NotFound();
            }

            return View(operations);
        }

        // GET: Operations/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id");
            return View();
        }

        // POST: Operations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Amount,IsLost,Percentage,OperactionType,Description,UserId")] Operations operations)
        {

            operations.Date = DateTime.Now.ToString("dd/MM/yyyy");

            if (ModelState.IsValid)
            {
                _context.Add(operations);
                await _context.SaveChangesAsync();

                return Redirect("Create");

                //return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", operations.UserId);

            return View();
           

        }




        // GET: Operations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operations = await _context.Operations.FindAsync(id);
            if (operations == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", operations.UserId);
            return View(operations);
        }

        // POST: Operations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,Amount,OperactionType,Description,UserId")] Operations operations)
        {
            if (id != operations.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(operations);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OperationsExists(operations.Id))
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
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", operations.UserId);
            return View(operations);
        }

        // GET: Operations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operations = await _context.Operations
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (operations == null)
            {
                return NotFound();
            }

            return View(operations);
        }

        // POST: Operations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var operations = await _context.Operations.FindAsync(id);
            _context.Operations.Remove(operations);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OperationsExists(int id)
        {
            return _context.Operations.Any(e => e.Id == id);
        }
    }
}
