using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.Data;
using App.Data.Entities;
using System.Text;
using System.Text.Json;

namespace App.View.Controllers
{
    public class VeMayBaysController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "https://localhost:7039/api/vemaybay";

        public VeMayBaysController()
        {
            _httpClient = new();
        }

        // GET: VeMayBays
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync(_apiBaseUrl);
            if (response.IsSuccessStatusCode)
            {
                var veMayBays = await response.Content.ReadAsStringAsync();
                var veMayBayList = JsonSerializer.Deserialize<List<VeMayBay>>(veMayBays);
                return View(veMayBayList);
            }
            return View(new List<VeMayBay>());
        }

        // GET: VeMayBays/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                var veMayBay = await response.Content.ReadAsStringAsync();
                var veMayBayDetail = JsonSerializer.Deserialize<VeMayBay>(veMayBay);
                return View(veMayBayDetail);
            }

            return NotFound();
        }

        // GET: VeMayBays/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VeMayBays/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,SoHieuMayBay,NgayBay,DiemKhoiHanh,DiemDen,GiaVe")] VeMayBay veMayBay)
        {
            if (ModelState.IsValid)
            {
                veMayBay.Id = Guid.NewGuid();
                var jsonContent = JsonSerializer.Serialize(veMayBay);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(_apiBaseUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(veMayBay);
        }

        // GET: VeMayBays/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                var veMayBay = await response.Content.ReadAsStringAsync();
                var veMayBayDetail = JsonSerializer.Deserialize<VeMayBay>(veMayBay);
                return View(veMayBayDetail);
            }

            return NotFound();
        }

        // POST: VeMayBays/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,SoHieuMayBay,NgayBay,DiemKhoiHanh,DiemDen,GiaVe")] VeMayBay veMayBay)
        {
            if (id != veMayBay.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var jsonContent = JsonSerializer.Serialize(veMayBay);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"{_apiBaseUrl}/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(veMayBay);
        }

        // GET: VeMayBays/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                var veMayBay = await response.Content.ReadAsStringAsync();
                var veMayBayDetail = JsonSerializer.Deserialize<VeMayBay>(veMayBay);
                return View(veMayBayDetail);
            }

            return NotFound();
        }

        // POST: VeMayBays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View();
        }
    }
}
