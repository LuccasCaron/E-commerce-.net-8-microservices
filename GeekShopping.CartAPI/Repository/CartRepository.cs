using AutoMapper;
using GeekShopping.CartAPI.Data.ValueObjects;
using GeekShopping.CartAPI.Model;
using GeekShopping.CartAPI.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CartAPI.Repository;

public class CartRepository : ICartRepository
{

    #region Properties

    private readonly SqlServerContext _context;
    private IMapper _mapper;

    #endregion

    #region Constructor

    public CartRepository(SqlServerContext context, IMapper mapper)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    #endregion

    #region Read Operations
    public async Task<CartVO> FindCartByUserIdAsync(string userId)
    {
        Cart cart = new Cart()
        {
            CartHeader = await _context.CartHeaders.FirstOrDefaultAsync(c => c.UserId == userId)
        };

        cart.CartDetails = _context.CartDetails
                                   .Where(c => c.CartHeaderId == cart.CartHeader.Id)
                                   .Include(c => c.Product);

        return _mapper.Map<CartVO>(cart);
    }

    #endregion

    #region Write Operations

    public async Task<bool> ApplyCouponAsync(string userId, string couponCode)
    {
        throw new NotImplementedException();
    }

    public async Task<CartVO> SaveOrUpdateCartAsync(CartVO vo)
    {
        Cart cart = _mapper.Map<Cart>(vo);

        // verificar se o produto ja esta salvo no banco, se nao tiver, salvar
        var product = await _context.Products
                                    .FirstOrDefaultAsync(p => p.Id == cart.CartDetails.FirstOrDefault().ProductId)
                                    .ConfigureAwait(false);

        if(product == null)
        {
            _context.Products.Add(cart.CartDetails.FirstOrDefault().Product);
            await _context.SaveChangesAsync();
        }

        //  verificar se o cart header é null

        var cartHeader = await _context.CartHeaders
                                       .AsNoTracking()
                                       .FirstOrDefaultAsync(c => c.UserId == cart.CartHeader.UserId)
                                       .ConfigureAwait(false);
        if(cartHeader == null)
        {
            _context.CartHeaders.Add(cart.CartHeader);
            await _context.SaveChangesAsync();
            cart.CartDetails.FirstOrDefault().CartHeaderId = cart.CartHeader.Id;
            cart.CartDetails.FirstOrDefault().Product = null; //setar como null pq ja salvamos o product no banco
            _context.CartDetails.Add(cart.CartDetails.FirstOrDefault());
            await _context.SaveChangesAsync();
        }
        else
        {
            //verificar se o cart details é do mesmo produto
            var cartDetail = await _context.CartDetails
                                           .AsNoTracking()
                                           .FirstOrDefaultAsync(
                                            p => p.ProductId == cart.CartDetails.FirstOrDefault().ProductId
                                            &&
                                            p.CartHeaderId == cartHeader.Id
                                            )
                                           .ConfigureAwait(false);

            if(cartDetail == null)
            {
                //criar cart details
                cart.CartDetails.FirstOrDefault().CartHeaderId = cart.CartHeader.Id;
                cart.CartDetails.FirstOrDefault().Product = null; //setar como null pq ja salvamos o product no banco
                _context.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                await _context.SaveChangesAsync();

            }
            else
            {
                //update product e cart details
                cart.CartDetails.FirstOrDefault().Product = null;
                cart.CartDetails.FirstOrDefault().Count += cartDetail.Count;
                cart.CartDetails.FirstOrDefault().Id = cartDetail.Id;
                cart.CartDetails.FirstOrDefault().CartHeaderId += cartDetail.CartHeaderId;
                _context.CartDetails.Update(cart.CartDetails.FirstOrDefault());
                await _context.SaveChangesAsync();
            }
        }

        return _mapper.Map<CartVO>(cart);
    }

    public async Task<bool> ClearCartAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> RemoveCouponAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> RemoveFromCartAsync(long cartDetailsId)
    {
        throw new NotImplementedException();
    }

    #endregion

}
