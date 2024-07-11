using BookServiceUsingRepo.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MvcClient.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(LoginModel loginModel)
        {
            var token = string.Empty;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5041/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                // Make call to service
                HttpResponseMessage response = await client.PostAsJsonAsync("api/login", loginModel);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();
                    var stringJWT = jsonString.Result;
                    JWT jwt = JsonConvert.DeserializeObject
      <JWT>(stringJWT);
                    HttpContext.Session.SetString("token", jwt.Token);

                    TempData["Message"] = "User logged in successfully!";

                    return RedirectToAction("Index", "Book");
                }
                else
                {
                    return View();
                }

               
            }
        }
    }

    public class JWT
    {
        public string Token { get; set; }
    }
}
