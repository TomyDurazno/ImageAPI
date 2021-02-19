using ImageAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageAPI.Interfaces
{
    public interface IImageCachingService
    {
        bool Loaded { get; }
        Task Load();

        Task<List<ImageSearchDetailResult>> Search(string searchTerm);
    }
}
