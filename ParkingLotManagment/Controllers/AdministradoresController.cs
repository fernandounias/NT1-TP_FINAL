using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ParkingLotManagment.DataBase;
using ParkingLotManagment.Models;

namespace ParkingLotManagment.Controllers
{
    public class AdministradoresController : Controller
    {
        private readonly ParkingLotManagementContext _context;

        public AdministradoresController(ParkingLotManagementContext context)
        {
            _context = context;
        }

        // GET: Administadors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Administradores.ToListAsync());
        }

        // GET: Administadors/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var administador = await _context.Administradores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (administador == null)
            {
                return NotFound();
            }

            return View(administador);
        }

        // GET: Administadors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Administadors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Legajo,Id,Nombre,Apellido,Username")] Administrador administador)
        {
            if (ModelState.IsValid)
            {
                administador.Id = Guid.NewGuid();
                _context.Add(administador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(administador);
        }

        // GET: Administadors/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var administador = await _context.Administradores.FindAsync(id);
            if (administador == null)
            {
                return NotFound();
            }
            return View(administador);
        }

        // POST: Administadors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Legajo,Id,Nombre,Apellido,Username")] Administrador administador)
        {
            if (id != administador.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(administador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdministadorExists(administador.Id))
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
            return View(administador);
        }

        // GET: Administadors/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var administador = await _context.Administradores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (administador == null)
            {
                return NotFound();
            }

            return View(administador);
        }

        // POST: Administadors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var administador = await _context.Administradores.FindAsync(id);
            if (administador != null)
            {
                _context.Administradores.Remove(administador);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdministadorExists(Guid id)
        {
            return _context.Administradores.Any(e => e.Id == id);
        }
    }
}
