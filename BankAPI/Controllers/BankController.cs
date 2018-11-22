using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BankAPI.Models;

namespace BankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly BankAPIContext _context;

        public BankController(BankAPIContext context)
        {
            _context = context;
        }

        // GET: api/Bank
        [HttpGet]
        public IEnumerable<BankItem> GetBankItem()
        {
            return _context.BankItem;
        }

        // GET: api/Bank/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBankItem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bankItem = await _context.BankItem.FindAsync(id);

            if (bankItem == null)
            {
                return NotFound();
            }

            return Ok(bankItem);
        }

        // PUT: api/Bank/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBankItem([FromRoute] int id, [FromBody] BankItem bankItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bankItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(bankItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BankItemExists(id))
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

        // POST: api/Bank
        [HttpPost]
        public async Task<IActionResult> PostBankItem([FromBody] BankItem bankItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.BankItem.Add(bankItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBankItem", new { id = bankItem.Id }, bankItem);
        }

        // DELETE: api/Bank/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBankItem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bankItem = await _context.BankItem.FindAsync(id);
            if (bankItem == null)
            {
                return NotFound();
            }

            _context.BankItem.Remove(bankItem);
            await _context.SaveChangesAsync();

            return Ok(bankItem);
        }

        private bool BankItemExists(int id)
        {
            return _context.BankItem.Any(e => e.Id == id);
        }

        // GET: api/Bank/Asset
        [HttpGet]
        [Route("asset")]
        public async Task<List<BankItem>> GetAssetItem([FromQuery] string asset)
        {
            var assets = from m in _context.BankItem
                        select m; //get all the assets


            if (!String.IsNullOrEmpty(asset)) //make sure user gave a tag to search
            {
                assets = assets.Where(s => s.Asset.ToLower().Equals(asset.ToLower())); // find the entries with the search tag and reassign
            }

            var returned = await assets.ToListAsync(); //return the assets

            return returned;
        }

    }
}