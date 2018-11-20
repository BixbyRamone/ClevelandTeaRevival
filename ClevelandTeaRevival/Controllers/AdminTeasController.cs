using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClevelandTeaRevival.Data;
using ClevelandTeaRevival.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace ClevelandTeaRevival.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminTeasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminTeasController(ApplicationDbContext context,
                                    UserManager<IdentityUser> userManager/*,
                                    UserManager<IdentityRole> userRole*/)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: AdminTeas
        public async Task<IActionResult> Index(string sortOrder,
                                                string searchString,
                                                string currentFilter)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            //bool currentRole = _userRole.IsInRoleAsync("Administrator");

            ViewData["CategorySort"] = null;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            ViewData["CurrentFilter"] = searchString;
            var teas = from t in _context.Teas
                       select t;

            if (!String.IsNullOrEmpty(searchString))
            {
                teas = teas.Where(t => t.Name.Contains(searchString)
                                    || t.Description.Contains(searchString)
                                    || t.Category.Contains(searchString));
            }
            return View(await teas.ToListAsync());
        }


        // GET: AdminTeas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tea = await _context.Teas
                .FirstOrDefaultAsync(m => m.ID == id);
            if (tea == null)
            {
                return NotFound();
            }

            return View(tea);
        }

        // GET: AdminTeas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminTeas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Category,Description,PricePerCup,PricePerOz,PricePerPot,PricePerLb,OtherPrice,Amount")] Tea tea)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tea);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tea);
        }

        // GET: AdminTeas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tea = await _context.Teas.FindAsync(id);
            if (tea == null)
            {
                return NotFound();
            }
            return View(tea);
        }

        // POST: AdminTeas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Category,Description,PricePerCup,PricePerOz,PricePerPot,PricePerLb,OtherPrice,Amount")] Tea tea)
        {
            if (id != tea.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tea);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeaExists(tea.ID))
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
            return View(tea);
        }

        // GET: AdminTeas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tea = await _context.Teas
                .FirstOrDefaultAsync(m => m.ID == id);
            if (tea == null)
            {
                return NotFound();
            }

            return View(tea);
        }

        // POST: AdminTeas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tea = await _context.Teas.FindAsync(id);
            _context.Teas.Remove(tea);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeaExists(int id)
        {
            return _context.Teas.Any(e => e.ID == id);
        }
    }
}
