using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarDealerView.Models;
using CarDealerView.ViewModels;
using System.IO;

namespace CarDealerView.Controllers
{
    public class CarsController : Controller
    {
        private readonly CarDealerDbContext _context;
        private readonly Microsoft.AspNetCore.Hosting.IWebHostEnvironment _environment;
        public CarsController(CarDealerDbContext context, 
            Microsoft.AspNetCore.Hosting.IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Cars
        public async Task<IActionResult> Index()
        {
            var carDealerDbContext = _context.Cars.Include(c => c.CarModels);
            return View(await carDealerDbContext.ToListAsync());
        }

        // GET: Cars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .Include(c => c.CarModels)
                .FirstOrDefaultAsync(m => m.CarId == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // GET: Cars/Create
        public IActionResult Create()
        {
            ViewData["CarModelId"] = new SelectList(_context.CarModels, "CarModelId", "FullModelName");
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CarId,ProductionYear,Mileage,AccidentFree,FuelType,Price,CarPicture,CarModelId")] CarViewModel carViewModel)
        {
            if (ModelState.IsValid)
            {
                string fileName = UploadedFile(carViewModel);
                carViewModel.PicturePath = fileName;
                Car car = new Car
                {
                    AccidentFree = carViewModel.AccidentFree,
                    CarModelId = carViewModel.CarModelId,
                    FuelType = carViewModel.FuelType,
                    Mileage = carViewModel.Mileage,
                    Price = carViewModel.Price,
                    ProductionYear = carViewModel.ProductionYear,
                    PicturePath = fileName
                };
                _context.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarModelId"] = new SelectList(_context.CarModels, "CarModelId", "FullModelName", carViewModel.CarModelId);
            return View(carViewModel);
        }

        // GET: Cars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            ViewData["CarModelId"] = new SelectList(_context.CarModels, "CarModelId", "FullModelName", car.CarModelId);
            return View(car);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CarId,ProductionYear,Mileage,AccidentFree,FuelType,Price,PicturePath,CarModelId")] Car car)
        {
            if (id != car.CarId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.CarId))
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
            ViewData["CarModelId"] = new SelectList(_context.CarModels, "CarModelId", "FullModelName", car.CarModelId);
            return View(car);
        }

        // GET: Cars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .Include(c => c.CarModels)
                .FirstOrDefaultAsync(m => m.CarId == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.CarId == id);
        }

        private string UploadedFile(CarViewModel car)
        {
            string fileName = null;
            if (car != null)
            {
                string uploadsFolder = System.IO.Path.Combine(_environment.WebRootPath, "images");
                fileName = Guid.NewGuid().ToString() + "_" + car.CarPicture.FileName;
                
                string filePath = Path.Combine(uploadsFolder, fileName);
                using (var fileStream = new System.IO.FileStream(
                    filePath, System.IO.FileMode.Create))
                {
                    car.CarPicture.CopyTo(fileStream);
                }
            }
            return fileName;
        }
    }
}
