﻿using GeekShopping.CartAPI.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.CartAPI.Model;

[Table("cart_detail")]
public class CartDetail : BaseEntity
{

    #region Properties

    public long CartHeaderId { get; set; }

    [ForeignKey("CartHeaderId")]
    public CartHeader CartHeader { get; set; }

    public long ProductId { get; set; }

    [ForeignKey("ProductId")]

    public Product Product { get; set; }

    [Column("count")]

    public int Count { get; set; }

    #endregion

}
