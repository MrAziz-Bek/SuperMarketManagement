using System;
using UseCases.DataStorePluginInterfaces;

namespace UseCases;
public class DeleteCategoryUseCase : IDeleteCategoryUseCase
{
    private readonly ICategoryRepository _categoryRepository;

    public DeleteCategoryUseCase(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public void Delete(int categoryId)
    {
        _categoryRepository.DeleteCategory(categoryId);
    }
}