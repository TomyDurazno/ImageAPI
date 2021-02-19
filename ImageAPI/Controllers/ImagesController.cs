using ImageAPI.Interfaces;
using ImageAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImageAPI.Controllers
{
    [ApiController]
    public class ImagesController : ControllerBase
    {
        IImageCachingService _imageCachingService;

        public ImagesController(IImageCachingService imageCachingService)
        {
            _imageCachingService = imageCachingService;
        }

        [Route("/search/{searchTerm}")]
        public async Task<List<ImageSearchDetailResult>> Search(string searchTerm)
        {
            return await _imageCachingService.Search(searchTerm);
        }
    }
}
