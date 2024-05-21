using GeekShopping.ProductAPI.Data.ValueObjects;
using GeekShopping.ProductAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.ProductAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{

    #region Properties

    private IProductRepository _repository;

    #endregion

    #region Constructor

    public ProductController(IProductRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    #endregion

    #region Read Operations

    [HttpGet]

    public async Task<ActionResult<List<ProductVO>>> GetAll()
    {
        var products = await _repository.FindAllAsync();
        if (products == null) return NotFound();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductVO>> GetById(long id)
    {
        var product = await _repository.FindByIdAsync(id);
        if(product == null) return NotFound();
        return Ok(product);
    }

    #endregion

    #region Write Operations

    [HttpPost]
    public async Task<ActionResult<ProductVO>> Create(ProductVO vo)
    {
        if (vo == null) return BadRequest();
        var product = await _repository.CreateAsync(vo);
        return Ok(product);
    }

    [HttpPut]
    public async Task<ActionResult<ProductVO>> Update(ProductVO vo)
    {
        if (vo == null) return BadRequest();
        var product = await _repository.UpdateAsync(vo);
        return Ok(product); ;
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ProductVO>> Delete(long id)
    {
        var status = await _repository.DeleteByIdAsync(id);
        if (!status) return BadRequest();
        return Ok(status);
    }

    #endregion

}
