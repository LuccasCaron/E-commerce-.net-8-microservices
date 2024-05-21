namespace GeekShopping.CartAPI.Model;

public class Cart
{

    #region Properties

    public CartHeader CartHeader { get; set; }

    public IEnumerable<CartDetail> CartDetails { get; set; }

    #endregion

}
