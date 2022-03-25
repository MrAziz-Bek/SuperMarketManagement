using System.Linq;
using CoreBusiness;
using UseCases.DataStorePluginInterfaces;

namespace Plugins.DataStore.InMemory;
public class CategoryInMemoryRepository : ICategoryRepository
{
    public static List<Category> _categories;

    public CategoryInMemoryRepository()
    {
        _categories = new List<Category>()
        {
            new Category() { CategoryId = 1, Name = "Beverage", Description = "Beverage" },
            new Category() { CategoryId = 2, Name = "Bakery", Description = "Bakery" },
            new Category() { CategoryId = 3, Name = "Meat", Description = "Meat" }
        };
    }

    public void AddCategory(Category category)
    {
        if (_categories.Any(c => c.Name.Equals(category.Name, StringComparison.OrdinalIgnoreCase)))
        {
            return;
        }
        if (_categories is not null && _categories.Count > 1)
        {
            var maxId = _categories.Max(c => c.CategoryId);
            category.CategoryId = maxId + 1;
        }
        else
        {
            category.CategoryId = 1;
        }
        _categories.Add(category);
    }

    public void DeleteCategory(int categoryId)
    {
        _categories.Remove(GetCategoryById(categoryId));
    }

    public IEnumerable<Category> GetCategories()
    {
        return _categories;
    }

    public Category GetCategoryById(int categoryId)
    {
        return _categories.FirstOrDefault(c => c.CategoryId == categoryId);
    }

    public void UpdateCategory(Category category)
    {
        var categoryToUpdate = GetCategoryById(category.CategoryId);
        if (categoryToUpdate is not null)
        {
            categoryToUpdate.Name = category.Name;
            categoryToUpdate.Description = category.Description;
        }
    }
}