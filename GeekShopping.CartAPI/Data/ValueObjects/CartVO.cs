namespace GeekShopping.CartAPI.Data.ValueObjects;

public class CartVO
{

    #region Properties

    public CartHeaderVO CartHeader { get; set; }

    public IEnumerable<CartDetailVO> CartDetails { get; set; }

    #endregion

}
