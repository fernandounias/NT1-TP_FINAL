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
    public class VehiculosController : Controller
    {
        private readonly ParkingLotManagementContext _context;
        private const string _Client_Id = "ClientId";

        public VehiculosController(ParkingLotManagementContext context)
        {
            _context = context;
        }

        // GET: Vehiculos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Vehiculos.ToListAsync());
        }

        // GET: Vehiculos/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehiculo = await _context.Vehiculos
                .FirstOrDefaultAsync(m => m.Patente == id);
            if (vehiculo == null)
            {
                return NotFound();
            }

            return View(vehiculo);
        }

        // GET: Vehiculoes/Create
        public async Task<IActionResult> Create(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            TempData[_Client_Id] = id;

            var id2 = id;
            var i = _context.Clientes;
            var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.Id.Equals(id));
            var cliente = await _context.Clientes.Find(id);
            
            if (cliente == null)
            {
                return NotFound();
            }
            return View();
        }

        // POST: Vehiculos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Vehiculo vehiculo)
        {
            string clientId = TempData[_Client_Id] as string;

            if (ModelState.IsValid)
            {
                var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.Id.Equals(clientId));
                if (cliente != null)
                {
                    cliente.Vehiculos.Add(vehiculo);
                }
                else
                {
                    return RedirectToAction(nameof(Index), "Home");
                }
                /*var vehiculosList= cliente.Vehiculos.ToList();
                vehiculosList.Add(vehiculo);
                cliente.Vehiculos = vehiculosList.ToArray();*/
                _context.Add(vehiculo);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            TempData[_Client_Id] = clientId;
            return View(vehiculo);
        }

        // GET: Vehiculoes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehiculo = await _context.Vehiculos.FindAsync(id);
            if (vehiculo == null)
            {
                return NotFound();
            }
            return View(vehiculo);
        }

        // POST: Vehiculos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Patente,Marca,Modelo,TipoDeVehiculo")] Vehiculo vehiculo)
        {
            if (id != vehiculo.Patente)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehiculo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehiculoExists(vehiculo.Patente))
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
            return View(vehiculo);
        }

        // GET: Vehiculoes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehiculo = await _context.Vehiculos
                .FirstOrDefaultAsync(m => m.Patente == id);
            if (vehiculo == null)
            {
                return NotFound();
            }

            return View(vehiculo);
        }

        // POST: Vehiculoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var vehiculo = await _context.Vehiculos.FindAsync(id);
            if (vehiculo != null)
            {
                _context.Vehiculos.Remove(vehiculo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehiculoExists(string id)
        {
            return _context.Vehiculos.Any(e => e.Patente == id);
        }
    }
}
