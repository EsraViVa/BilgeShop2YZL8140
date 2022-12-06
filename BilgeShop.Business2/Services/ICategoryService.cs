using BilgeShop.Business2.Dto;
using BilgeShop.Business2.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeShop.Business2.Services
{
    public interface ICategoryService
    {
        ServiceMessage AddCategory(CategoryDto categoryDto);
        List<CategoryDto> GetCategories();
        CategoryDto GetCategoryById(int id);
        void UpdateCategory (CategoryDto categoryDto);
    }
}
