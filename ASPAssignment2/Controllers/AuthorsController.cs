using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASPAssignment2.Data;
using ASPAssignment2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Principal;
using System.Security.Claims;

namespace ASPAssignment2.Controllers
{
[Authorize]
    public class AuthorsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> userManager;

        public AuthorsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        [AllowAnonymous]
        // GET: Authors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Author.ToListAsync());
        }

        [AllowAnonymous]
        // GET: Authors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Author
                .FirstOrDefaultAsync(m => m.AuthorId == id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // GET: Authors/Create
        public async Task<IActionResult> Create()
        {
            Author author = new Author {AuthorName = HttpContext.Request.Query["AuthorName"], AccountID = HttpContext.Request.Query["AccountId"]};
            if (ModelState.IsValid)
            {
                foreach(Author a in _context.Set<Author>())
                {
                    //if any existing author has the same account ID, redirect to Index to avoid duplication of the Author
                    if (a.AccountID == User.FindFirst(ClaimTypes.NameIdentifier).Value)
                    {
                        return RedirectToAction("Index");
                    }
                }

                _context.Add(author);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        // GET: Authors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Author
                .FirstOrDefaultAsync(m => m.AuthorId == id);
            if (author == null)
            {
                return NotFound();
            }

            if (author.AccountID == User.FindFirst(ClaimTypes.NameIdentifier).Value) { 
            return View(author);
            }
            return RedirectToAction("Index");
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var author = await _context.Author.FindAsync(id);
            //TODO: Delete all of the author's articles
            _context.Author.Remove(author);
            await userManager.DeleteAsync(userManager.FindByIdAsync(author.AccountID).Result);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorExists(int id)
        {
            return _context.Author.Any(e => e.AuthorId == id);
        }
    }
}
