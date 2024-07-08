using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Extensibility;
using Microsoft.IdentityModel.Tokens;
using ParkingLotManagment.DataBase;
using ParkingLotManagment.Extensions;
using ParkingLotManagment.Models;

namespace ParkingLotManagment.Controllers
{
    public class ClientesController : Controller
    {
        private readonly ParkingLotManagementContext _context;

        public ClientesController(ParkingLotManagementContext context)
        {
            _context = context;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Clientes.ToListAsync());
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        /*public async Task<IActionResult> Create([Bind("FechaDeNacimiento,Dni,Id,Nombre,Apellido,Username")] Cliente cliente)*/
        public async Task<IActionResult> Create(Cliente cliente, string pass)
        {
            try
            {
                pass.ValidatePassword();
                cliente.Password = pass.Encript();
                if (!ModelState.IsValid) ModelState.Remove(nameof(Usuario.Password));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(nameof(Cliente.Password), ex.Message);
            }

            cliente.Vehiculos = [];

            if (!ModelState.IsValid) ModelState.Remove(nameof(Cliente.Vehiculos));
            
            if (ModelState.IsValid)
            {
                cliente.Id = Guid.NewGuid();
                //cliente.Password = pass.Encript();

                cliente.FechaAlta = DateTime.Now;
                //
                //ModelState.SetModelValue(nameof(Usuario.Password), new ValueProviderResult(cliente.Password));
                //ModelState.Add(cliente.Password);
                //ModelState.AddModelError(nameof(Cliente.Password), "Contraseña requerida");

                _context.Add(cliente);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Details), new { id = cliente.Id });
            }
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("FechaDeNacimiento,Dni,Id,Nombre,Apellido,Username")] Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.Id))
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
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(Guid id)
        {
            return _context.Clientes.Any(e => e.Id == id);
        }
    }
}
