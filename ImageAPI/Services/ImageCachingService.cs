using ImageAPI.Interfaces;
using ImageAPI.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageAPI.Services
{
    public class ImageCachingService : IImageCachingService
    {
        IMemoryCache _cache;
        IImageService _imageService;

        bool loaded;

        string pagesKey = "PAGES";
        string allImagesKey = "ALLIMAGES";

        public ImageCachingService(IMemoryCache cache, IImageService imageService)
        {
            _cache = cache;
            _imageService = imageService;
        }

        public bool Loaded => loaded;

        public async Task Load()
        {
            var pages = new List<ImageSearchPaginatedResult>();

            bool exit = false;

            int n = 1;

            while(!exit)
            {
                var page = await _imageService.GetPage(n);

                pages.Add(page);

                n++;

                if (!page.hasMore)
                    exit = true;
            }

            var allImages = await Task.WhenAll(pages.SelectMany(r => r.pictures).Select(p => _imageService.GetImage(p.id)));

            _cache.Set(pagesKey, pages);
            _cache.Set(allImagesKey, allImages.ToList());
            loaded = true;
        }

        public async Task<List<ImageSearchDetailResult>> Search(string searchTerm)
        {
            if(!loaded) //first time, force load                            
                await Load();           

            if (!_cache.TryGetValue(allImagesKey, out List<ImageSearchDetailResult> cached))
                throw new Exception("Cache initialization not working properly");

            return cached.Where(c => c.FieldMatches(searchTerm)).ToList();
        }
    }
}
