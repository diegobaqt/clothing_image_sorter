using SorterServices.Models;
using System.Threading.Tasks;

namespace SorterServices.Services.Interfaces
{
    public interface IPredictService
    {
        // POST:Predict Image Class
        Task<PredictResponseViewModel> PredictClassImageAsync(ImageViewModel imageViewModel);
    }
}
