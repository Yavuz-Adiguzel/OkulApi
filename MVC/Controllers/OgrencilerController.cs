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
            var response = await client.GetFromJsonAsync<List<Ogrenci>>("ogrenciler");
            return View(response);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Ogrenci ogrenci)
        {
            var client = _httpClientFactory.CreateClient("OkulApi");
            var response = await client.PostAsJsonAsync("ogrenciler", ogrenci);
            return View("Index");
        }
    }
}
