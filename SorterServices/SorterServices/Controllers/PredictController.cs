using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SorterServices.Models;
using SorterServices.Services.Interfaces;

namespace SorterServices.Controllers
{
    [RequireHttps]
    [Route("[controller]/[action]")]
    public class PredictController : Controller
    {
        private readonly IPredictService _predictService;

        #region Constructor
        public PredictController(IPredictService predictService)
        {
            _predictService = predictService;
        }
        #endregion

        #region Post 
        [HttpPost]
        public async Task<IActionResult> PredictClassImage(ImageViewModel imageViewModel)
        {
            try
            {
                var result = await _predictService.PredictClassImageAsync(imageViewModel);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest("Ha ocurrido un error inesperado.");
            }
        }
        #endregion
    }
}