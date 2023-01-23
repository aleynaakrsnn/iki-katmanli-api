using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using webmvcclientuyg.Models;

namespace webmvcclientuyg.Controllers
{
    public class MusterilerController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<Musteriler> musteriList = new List<Musteriler>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:26935/api/Musteriler"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    musteriList = JsonConvert.DeserializeObject<List<Musteriler>>(apiResponse);
                }
            }
            return View(musteriList);
        }
        public ViewResult GetMusteriler() => View();


        [HttpPost]
        public async Task<IActionResult> GetMusteriler(int id)
        {
            Musteriler musteriler = new Musteriler();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:26935/api/Musteriler/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    musteriler = JsonConvert.DeserializeObject<Musteriler>(apiResponse);
                }
            }
            return View(musteriler);
        }

        [HttpGet]
        public ViewResult AddMusteriler() => View();

        [HttpPost]

        public async Task<IActionResult> AddMusteriler(Musteriler musteriler)
        {
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(musteriler), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync("http://localhost:26935/api/Musteriler", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        musteriler = JsonConvert.DeserializeObject<Musteriler>(apiResponse);
                    }
                }
                return View(musteriler);
            }
            return View();
            
        }

        [HttpGet]
        public async Task<IActionResult> UpdateMusteriler(int id)
        {
            Musteriler musteriler = new Musteriler();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:26935/api/Musteriler/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    musteriler = JsonConvert.DeserializeObject<Musteriler>(apiResponse);
                }
            }
            return View(musteriler);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateMusteriler(Musteriler musteriler)
        {
            Musteriler mus1 = new Musteriler();
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    var content = new MultipartFormDataContent();
                    content.Add(new StringContent(musteriler.Id.ToString()), "Id");
                    content.Add(new StringContent(musteriler.AdSoyad), "AdSoyad");
                    content.Add(new StringContent(musteriler.Adres), "Adres");
                    content.Add(new StringContent(musteriler.Telefon), "Telefon");
                    content.Add(new StringContent(musteriler.Email), "Email");
                    using (var response = await httpClient.PutAsync("http://localhost:26935/api/Musteriler", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        ViewBag.Result = "Success";
                        mus1 = JsonConvert.DeserializeObject<Musteriler>(apiResponse);
                    }
                }
            }
            return View(mus1);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteMusteriler(int MusteriId)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("http://localhost:26935/api/Musteriler/" + MusteriId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction("Index");
        }

    }
}

