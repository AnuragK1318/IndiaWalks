using IndiaWalks.UI.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace IndiaWalks.UI.Controllers
{
    public class RegionController : Controller
    {
        //can create http client instances
        private readonly IHttpClientFactory _httpClientFactory;
        public RegionController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public async Task<IActionResult> Region(int pageNumber = 1, int pageSize = 10)
        {
            List<RegionDto> responseBody = new List<RegionDto>();
            try
            {
                var client = _httpClientFactory.CreateClient();
                var httpResponseMessage = await client.GetAsync($"https://localhost:7260/api/region?pageNumber={pageNumber}&pageSize={pageSize}");
                httpResponseMessage.EnsureSuccessStatusCode();
                responseBody.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionDto>>());
                ViewBag.PageNumber = pageNumber;
                ViewBag.PageSize = pageSize;
            }
            catch (Exception ex)
            {

                throw;
            }
            return View("Region", responseBody);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRegionRequestDto addregion)
        {
            var client = _httpClientFactory.CreateClient();
            var HttpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7260/api/region"),
                Content = new StringContent(JsonSerializer.Serialize(addregion), Encoding.UTF8, "application/json")
            };
            var httpResponseMessage = await client.SendAsync(HttpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDto>();
            if (response != null)
            {
                return RedirectToAction("Region", "Region");
            }
            return View();

        }

        // 1. GET: Fetch the data to show in the Edit Form
        [HttpGet]   
        public async Task<IActionResult> Edit(int id)
        {   
            var client = _httpClientFactory.CreateClient();
            var httpResponse = await client.GetFromJsonAsync<RegionDto>($"https://localhost:7260/api/region/{id}");
            if (httpResponse != null)
            {
                return View(httpResponse);
            }
            return View(null);
        }

        // 2. POST: Receive the data FROM the form and send it to the API
        [HttpPost]
        public async Task<IActionResult> Edit(RegionDto regionDto)
        {
            var client = _httpClientFactory.CreateClient();

            // We serialize the DTO to send to the API
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put, 
                RequestUri = new Uri($"https://localhost:7260/api/region/{regionDto.Id}"),
                Content = new StringContent(JsonSerializer.Serialize(regionDto), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                // After successful update, go back to the list
                return RedirectToAction("Region", "Region");
            }

            // If API update failed, stay on the edit page and show the data again
            return View(regionDto);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient();
            // Fetch the region so the user knows what they are deleting
            var response = await client.GetFromJsonAsync<RegionDto>($"https://localhost:7260/api/region/{id}");

            return View(response); // This looks for Delete.cshtml
        }

        [HttpPost]
        public async Task<IActionResult> Delete(RegionDto regionDto)
        {
            try
            {
                //create a client to talk to
                var client = _httpClientFactory.CreateClient();

                //delete the region
                var httpResponse = await client.DeleteAsync($"https://localhost:7260/api/region/{regionDto.Id}");

                httpResponse.EnsureSuccessStatusCode();

                return RedirectToAction("Region","Region");
            }
            catch (Exception ex)
            {

                throw;
            }
            return View("Edit");
        }
    }
}
