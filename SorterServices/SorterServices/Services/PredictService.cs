using SorterServices.Models;
using SorterServices.Services.Interfaces;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SorterServices.Services
{
    public class PredictService : IPredictService
    {
        private const string HttpUrl = "MODEL_ENDPOINT";
        private const string HttpsUrl = "BASE_URL";
        private readonly HttpClient _client = new HttpClient { BaseAddress = new Uri(HttpUrl) };

        #region Post
        public async Task<PredictResponseViewModel> PredictClassImageAsync(ImageViewModel imageViewModel)
        {
            var url = Path.Combine("wwwroot", "files", "images");
            Directory.CreateDirectory(url);

            var path = Path.Combine(url, imageViewModel.Image.FileName);
            if (File.Exists(url)) File.Delete(url);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await imageViewModel.Image.CopyToAsync(stream);
            }

            var imagePath = HttpsUrl + "/files/images/" + imageViewModel.Image.FileName;
            var result = await Predict(imagePath);

            var predictResponseViewModel = new PredictResponseViewModel { Response = FormatResult(result) };
            return predictResponseViewModel;
        }

        public async Task<string> Predict(string url)
        {
            var urlViewModel = new UrlViewModel
            {
                url = url
            };

            try
            {
                var json = JsonConvert.SerializeObject(urlViewModel);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _client.PostAsync(_client.BaseAddress + "predict", content);
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    return result;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "Error";
        }
        #endregion

        #region Utils
        private static string FormatResult(string result)
        {
            switch (result)
            {
                case "shoe":
                    return "Shoes";
                case "dress":
                    return "Dresses";
                case "pants":
                    return "Pants";
                case "outerwear":
                    return "Outerwear";
                default:
                    return "No Classified";
            }
        }

        private static string GetClass()
        {
            var random = new Random();
            var value = random.Next(1, 5);

            switch (value)
            {
                case 1:
                    return "Outerwear";
                case 2:
                    return "Dresses";
                case 3:
                    return "Pants";
                case 4:
                    return "Shoes";
                default:
                    return "";
            }
        }
        #endregion
    }
}
