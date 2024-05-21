using GeekShopping.ProductAPI.Data.ValueObjects;

namespace GeekShopping.ProductAPI.Repository;

public interface IProductRepository
{

    Task<IEnumerable<ProductVO>> FindAllAsync();

    Task<ProductVO> FindByIdAsync(long id);

    Task<ProductVO> CreateAsync(ProductVO vo);

    Task<ProductVO> UpdateAsync(ProductVO vo);

    Task<bool> DeleteByIdAsync(long id);

}
