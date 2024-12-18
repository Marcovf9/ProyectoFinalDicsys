using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFinalDicsys.Context;
using ProyectoFinalDicsys.Models;
using ProyectoFinalDicsys.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ProductoController : ControllerBase
{
    private readonly Context _context;
    private readonly IMapper _mapper;

    public ProductoController(Context context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductoDTO>>> TraerProductos()
    {
        var productos = await _context.Productos.Include(p => p.Categoria).ToListAsync();

        var productoDtos = _mapper.Map<List<ProductoDTO>>(productos);

        return Ok(productoDtos);
    }

    [HttpPost]
    public async Task<ActionResult<ProductoDTO>> AgregarProducto([FromBody] ActualizarProductosDTO productoDto)
    {
        var categoria = await _context.Categorias.FindAsync(productoDto.CategoriaId);
        if (categoria == null)
        {
            return BadRequest("Categoría no válida.");
        }

        var producto = new Producto
        {
            Nombre = productoDto.Nombre,
            Precio = productoDto.Precio,
            CategoriaId = productoDto.CategoriaId
        };

        _context.Productos.Add(producto);
        await _context.SaveChangesAsync();

        var productoDtoResponse = _mapper.Map<ProductoDTO>(producto);

        return CreatedAtAction(nameof(TraerProductos), new { id = producto.Id }, productoDtoResponse);
    }



    [HttpPut("{id}")]
    public async Task<IActionResult> ActualizarProducto(int id, [FromBody] ActualizarProductosDTO productoDto)
    {
        if (id != productoDto.Id)
        {
            return BadRequest("El id del producto no coincide.");
        }

        var productoExistente = await _context.Productos.Include(p => p.Categoria).FirstOrDefaultAsync(p => p.Id == id);
        if (productoExistente == null)
        {
            return NotFound("Producto no encontrado.");
        }

        var categoria = await _context.Categorias.FindAsync(productoDto.CategoriaId);
        if (categoria == null)
        {
            return BadRequest("Categoría no válida.");
        }

        productoExistente.Nombre = productoDto.Nombre;
        productoExistente.Precio = productoDto.Precio;
        productoExistente.CategoriaId = productoDto.CategoriaId;

        _context.Productos.Update(productoExistente);
        await _context.SaveChangesAsync();

        var productoDtoResponse = _mapper.Map<ProductoDTO>(productoExistente);

        return Ok(productoDtoResponse);
    }



    [HttpDelete("{id}")]
    public async Task<IActionResult> BorrarProducto(int id)
    {
        var producto = await _context.Productos.FindAsync(id);
        if (producto == null)
        {
            return NotFound("Producto no encontrado.");
        }

        _context.Productos.Remove(producto);
        await _context.SaveChangesAsync();

        return Ok($"Producto con id {id} eliminado exitosamente.");
    }
}
