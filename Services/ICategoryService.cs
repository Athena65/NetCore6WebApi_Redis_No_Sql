using Reddis_NetCore6.Models;

namespace Reddis_NetCore6.Services
{
    public interface ICategoryService
    {
        public List<CategoryModel> GetAllCategory();
    }
}
