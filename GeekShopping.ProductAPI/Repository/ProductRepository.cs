using AutoMapper;
using GeekShopping.ProductAPI.Data.ValueObjects;
using GeekShopping.ProductAPI.Model;
using GeekShopping.ProductAPI.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.ProductAPI.Repository;

public class ProductRepository : IProductRepository
{

    #region Properties

    private readonly SqlServerContext _context;
    private IMapper _mapper;

    #endregion

    #region Constructor

    public ProductRepository(SqlServerContext context, IMapper mapper)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    #endregion

    #region Methods

    public async Task<IEnumerable<ProductVO>> FindAllAsync()
    {
        List<Product> products = await _context.Products.AsNoTracking()
                                                        .ToListAsync()
                                                        .ConfigureAwait(false);

        return _mapper.Map<List<ProductVO>>(products);
    }

    public async Task<ProductVO> FindByIdAsync(long id)
    {
        Product? product = await _context.Products
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync(p => p.Id == id)
                                        .ConfigureAwait(false);

        return _mapper.Map<ProductVO>(product);
    }

    public async Task<ProductVO> CreateAsync(ProductVO vo)
    {
        Product product = _mapper.Map<Product>(vo);
        _context.Products.Add(product);

        await _context.SaveChangesAsync()
                      .ConfigureAwait(false);

        return _mapper.Map<ProductVO>(product);
    }

    public async Task<ProductVO> UpdateAsync(ProductVO vo)
    {
        Product product = _mapper.Map<Product>(vo);
        _context.Products.Update(product);

        await _context.SaveChangesAsync()
                      .ConfigureAwait(false);

        return _mapper.Map<ProductVO>(product);
    }

    public async Task<bool> DeleteByIdAsync(long id)
    {
        try
        {
            Product? product = await _context.Products
                                            .AsNoTracking()
                                            .FirstOrDefaultAsync(p => p.Id == id)
                                            .ConfigureAwait(false);

            if (product == null) return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync()
                          .ConfigureAwait(false);

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    #endregion

}
