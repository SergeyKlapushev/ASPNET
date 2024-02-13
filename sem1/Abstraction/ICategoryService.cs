using sem1.Dto;

namespace sem1.Abstraction
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDto> GetCategories();
        int AddCategory(CategoryDto category);
    }
}
