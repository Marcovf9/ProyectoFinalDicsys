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
public class CategoriaController : ControllerBase
{
    private readonly Context _context;
    private readonly IMapper _mapper;

    public CategoriaController(Context context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetCategories()
    {
        var categorias = await _context.Categorias.ToListAsync();

        var categoriaDTO = _mapper.Map<List<CategoriaDTO>>(categorias);

        return Ok(categoriaDTO);
    }
}