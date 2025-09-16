using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Controllers
{
    public class OgrencilerController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public OgrencilerController (IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("OkulApi");
            var response = await client.GetFromJsonAsync<List<OgrenciCreateDto>>("Ogrenciler");
            return View(response);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(OgrenciCreateDto ogrenci)
        {
            if (!ModelState.IsValid)
            {
                return View(ogrenci);
            }

            var client = _httpClientFactory.CreateClient("OkulApi");
            var response = await client.PostAsJsonAsync("Ogrenciler/ogrenci", ogrenci);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Öğrenci eklenemedi");
            return View(ogrenci);
        }
    }
}
