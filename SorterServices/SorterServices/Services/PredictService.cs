using SorterServices.Models;
using SorterServices.Services.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SorterServices.Services
{
    public class PredictService : IPredictService
    {
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

            // TODO: Return real class
            var predictResponseViewModel = new PredictResponseViewModel { Response = GetClass() };

            return predictResponseViewModel;
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
