using Newtonsoft.Json;
using SorterApp.ViewModels;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace SorterApp.Services
{
    public class PredictService
    {
        private const string _httpsUrl = "YOUR_ENDPOINT_URL";
        private readonly HttpClient _client = new HttpClient { BaseAddress = new Uri(_httpsUrl) };

        public async Task<string> PredictClassImage (string imagePath)
        {
            var content = new MultipartFormDataContent();

            var image = File.ReadAllBytes(imagePath);
            content.Add(new ByteArrayContent(new MemoryStream(image).ToArray()), "Image", Path.GetFileName(imagePath));

            try
            {
                var response = await _client.PostAsync(_client.BaseAddress + "Predict/PredictClassImage", content);
                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<PredictResponseViewModel>(response.Content.ReadAsStringAsync().Result);
                    return result.Response;
                }
            } catch (Exception ex)
            {
                return ex.Message;
            }
            return "Error";
        }
    }
}
