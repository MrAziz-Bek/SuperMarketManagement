using System;
using CoreBusiness;
using UseCases.DataStorePluginInterfaces;

namespace UseCases;
public class ViewCategoriesUseCase : IViewCategoriesUseCase
{
    public readonly ICategoryRepository _categoryRepository;
    public ViewCategoriesUseCase(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public IEnumerable<Category> Execute()
    {
        return _categoryRepository.GetCategories();
    }
}