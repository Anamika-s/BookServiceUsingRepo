using BookServiceUsingRepo.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace MvcClient.Controllers
{
    public class BookController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<Book> books = new List<Book>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5041/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization =
          new AuthenticationHeaderValue("Bearer",
          HttpContext.Session.GetString("token"));
                // Make call to service
                HttpResponseMessage response = await client.GetAsync("api/book");
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();
                    books = JsonConvert.DeserializeObject<List<Book>>(jsonString.Result);
                }
                else
                {
                    ViewBag.msg = "NO records";
                }
            }
            return View(books);
        }

        public async Task<IActionResult> Details(int id)
        {
            Book book = new Book();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5041/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                // Make call to service
                HttpResponseMessage response = await client.GetAsync("api/book/" + id);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();
                    book = JsonConvert.DeserializeObject<Book>(jsonString.Result);
                }
                else
                {
                    ViewBag.msg = "NO records";
                }
            }
            return View(book);
        }


        public async Task<ActionResult> Create()
        {
            return View();
        }
        [HttpPost]


        public async Task<ActionResult> Create(Book book)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5041/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization =
        new AuthenticationHeaderValue("Bearer",
        HttpContext.Session.GetString("token"));
                // Make call to service
                HttpResponseMessage response = await client.PostAsJsonAsync("api/book", book);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();
                    book = JsonConvert.DeserializeObject<Book>(jsonString.Result);
                }

                return RedirectToAction("Index");
            }









        }
    }
}