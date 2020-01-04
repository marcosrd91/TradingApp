using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCMOVIES.Models;
using MvcMovie.Data;
using System.Data.SqlClient;

namespace MVCMOVIES.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationsApiController : ControllerBase
    {
        private readonly MvcMovieContext _context;

        public OperationsApiController(MvcMovieContext context)
        {
            _context = context;
        }

        // GET: api/OperationsApi
        [HttpGet]
        public IEnumerable<Operations> GetOperations()
        {
            return _context.Operations;
        }


        

        // GET: api/OperationsApi/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOperations([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var operations = await _context.Operations.FindAsync(id);

            var seacrh =  _context.Operations.FromSql("Select * from Operations where Userid=@id", new SqlParameter("@id",id));

            if (seacrh == null)
            {
                return NotFound();
            }

            return Ok(seacrh);
        }

     




        // PUT: api/OperationsApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOperations([FromRoute] int id, [FromBody] Operations operations)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != operations.Id)
            {
                return BadRequest();
            }

            _context.Entry(operations).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OperationsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/OperationsApi
        [HttpPost]
        public async Task<IActionResult> PostOperations([FromBody] Operations operations)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Operations.Add(operations);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOperations", new { id = operations.Id }, operations);
        }

        // DELETE: api/OperationsApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOperations([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var operations = await _context.Operations.FindAsync(id);
            if (operations == null)
            {
                return NotFound();
            }

            _context.Operations.Remove(operations);
            await _context.SaveChangesAsync();

            return Ok(operations);
        }

        private bool OperationsExists(int id)
        {
            return _context.Operations.Any(e => e.Id == id);
        }
    }
}