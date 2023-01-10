using GlassDataBas.Models;
using GlassDataBas.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlassDataBas.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GlassController : ControllerBase
{
    private readonly GlassShopService _glassService;

    public GlassController(GlassShopService glassService) =>
        _glassService = glassService;

    [HttpGet]
    public async Task<List<Glass>> Get() =>
        await _glassService.GetAsync();


    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Glass>> Get(string id)
    {
        var glass = await _glassService.GetAsync(id);

        if (glass is null)
        {
            return NotFound();
        }

        return glass;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Glass newGlass)
    {
        await _glassService.CreateAsync(newGlass);

        return CreatedAtAction(nameof(Get), new { id = newGlass.Id }, newGlass);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Glass updatedGlass)
    {
        var glass = await _glassService.GetAsync(id);

        if (glass is null)
        {
            return NotFound();
        }

        updatedGlass.Id = glass.Id;

        await _glassService.UpdateAsync(id, updatedGlass);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var glass = await _glassService.GetAsync(id);

        if (glass is null)
        {
            return NotFound();
        }

        await _glassService.RemoveAsync(id);

        return NoContent();
    }
}
