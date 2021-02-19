using ImageAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageAPI.Interfaces
{
    public interface IImageService
    {
        Task<ImageSearchPaginatedResult> GetPage(int n);

        Task<ImageSearchDetailResult> GetImage(string id);
    }
}
