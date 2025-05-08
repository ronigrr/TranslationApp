using Microsoft.AspNetCore.Mvc;
using TranslationApp.Infrastructure;

namespace TranslationApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TranslationController : ControllerBase
{
    private readonly ITranslationService _translationService;

    public TranslationController(ITranslationService translationService)
    {
        _translationService = translationService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Translate([FromQuery] string word)
    {
        try
        {
            var translation = await _translationService.TranslateToHebrewAsync(word);
            return Ok(translation);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
       
    }
}