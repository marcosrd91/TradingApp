using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCMOVIES.Models;
using MvcMovie.Data;

namespace MVCMOVIES.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfoesApiController : ControllerBase
    {
        private readonly MvcMovieContext _context;

        public InfoesApiController(MvcMovieContext context)
        {
            _context = context;
        }

        // GET: api/InfoesApi
        [HttpGet]
        public IEnumerable<Info> GetInfo()
        {
            return _context.Info;
        }

        // GET: api/InfoesApi/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInfo([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var info = await _context.Info.FindAsync(id);

            if (info == null)
            {
                return NotFound();
            }

            return Ok(info);
        }

        // PUT: api/InfoesApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInfo([FromRoute] long id, [FromBody] Info info)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != info.Id)
            {
                return BadRequest();
            }

            _context.Entry(info).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InfoExists(id))
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

        // POST: api/InfoesApi
        [HttpPost]
        public async Task<IActionResult> PostInfo([FromBody] Info info)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Info.Add(info);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (InfoExists(info.Id))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetInfo", new { id = info.Id }, info);
        }

        // DELETE: api/InfoesApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInfo([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var info = await _context.Info.FindAsync(id);
            if (info == null)
            {
                return NotFound();
            }

            _context.Info.Remove(info);
            await _context.SaveChangesAsync();

            return Ok(info);
        }

        private bool InfoExists(long id)
        {
            return _context.Info.Any(e => e.Id == id);
        }
    }
}