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
using System.Security.Claims;

namespace ASPAssignment2.Controllers
{
    [Authorize]
    public class ArticlesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArticlesController(ApplicationDbContext context)
        {
            _context = context;
        }
        [AllowAnonymous]
        // GET: Articles
        public async Task<IActionResult> Index()
        {
            var ApplicationDbContext = _context.Article.Include(a => a.Author);
            return View("Index", await ApplicationDbContext.ToListAsync());
        }

        [AllowAnonymous]
        // GET: Articles/Details/5
        public async Task<IActionResult> Details(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var article = await _context.Article
                .Include(a => a.Author)
                .FirstOrDefaultAsync(m => m.Id == Id);
            if (article == null)
            {
                return NotFound();
            }

            return View("Details", article);
        }

        // GET: Articles/Create
        public IActionResult Create()
        {
            //find the current author in the table
            foreach (Author a in _context.Set<Author>())
            {
                if(a.AccountID == User.FindFirst(ClaimTypes.NameIdentifier).Value)
                {
                    ViewData["Author"] = a;
                }
            }
            return View("Create");
        }

        // POST: Articles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Content,PeerReviewed,AuthorId")] Article article)
        {
            if (ModelState.IsValid)
            {
                _context.Add(article);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            foreach (Author a in _context.Set<Author>())
            {
                if (a.AccountID == User.FindFirst(ClaimTypes.NameIdentifier).Value)
                {
                    ViewData["Author"] = a;
                }
            }
            return View(article);
        }

        // GET: Articles/Edit/5
        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var article = await _context.Article.FindAsync(Id);
            if (article == null)
            {
                return NotFound();
            }
            foreach (Author a in _context.Set<Author>())
            {

                if (a.AccountID == User.FindFirst(ClaimTypes.NameIdentifier).Value)
                {
                if(article.AuthorId != a.AuthorId)
                    {
                        //redirect to index if user accesses unauthorized page
                        return RedirectToAction("Index");
                    }
                    ViewData["Author"] = a;
                }
            }
            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content,PeerReviewed,AuthorId")] Article article)
        {
            if (id != article.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(article);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticleExists(article.Id))
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
            foreach (Author a in _context.Set<Author>())
            {
                if (a.AccountID == User.FindFirst(ClaimTypes.NameIdentifier).Value)
                {
                    ViewData["Author"] = a;
                }
            }
            return View(article);
        }

        // GET: Articles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Article = await _context.Article
                .Include(a => a.Author)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Article == null)
            {
                return NotFound();
            }

            return View("Delete", Article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Article = await _context.Article.FindAsync(id);
            foreach (Author a in _context.Set<Author>())
            {
                if (a.AccountID == User.FindFirst(ClaimTypes.NameIdentifier).Value)
                {
                    if(Article.AuthorId != a.AuthorId)
                    {
                        //redirect to index if user accesses unauthorized page
                        return RedirectToAction("Index");
                    }
                }
            }
            _context.Article.Remove(Article);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public bool ArticleExists(int id)
        {
            return _context.Article.Any(e => e.Id == id);
        }
    }
}
