using Reddis_NetCore6.Models;

namespace Reddis_NetCore6.Services
{
    public class CategoryService : ICategoryService
    {
        static List<CategoryModel> categories => new()
        {
            new CategoryModel {Id=1,Name="Athena"},
            new CategoryModel {Id=2,Name="Burak"}
        };
        public ICacheService CacheService { get; }
        public CategoryService(ICacheService cacheService)
        {
            CacheService = cacheService;
        }

        private List<CategoryModel> GetCategoriesFromCache()
        {
            return CacheService.GetOrAdd("allcategories", () => { return categories; });
        }

        public List<CategoryModel> GetAllCategory()
        {
            return GetCategoriesFromCache();
        }
    }
}
