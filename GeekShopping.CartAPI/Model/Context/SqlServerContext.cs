using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CartAPI.Model.Context;

public class SqlServerContext : DbContext
{
    #region Constructor

    public SqlServerContext(DbContextOptions<SqlServerContext> options) : base(options)
    {
    }

    #endregion

    #region Tables

    public DbSet<Product> Products { get; set; }

    public DbSet<CartDetail> CartDetails { get; set; }

    public DbSet<CartHeader> CartHeaders { get; set; }

    #endregion
}
