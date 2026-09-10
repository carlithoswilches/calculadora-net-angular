using Calculadora.BLL;
using Calculadora.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Calculadora.API.Controllers;

// Expone el servicio de cálculo como endpoint HTTP para que Angular lo consuma
[ApiController]
[Route("api/[controller]")]
public class CalculadoraController : ControllerBase
{
    private readonly CalculadoraService _servicio = new();

    // POST /api/calculadora  { "expresion": "3 + 5 * (2 - 8)" }
    [HttpPost]
    public ActionResult<OperacionResponse> Calcular([FromBody] ExpresionRequest request)
    {
        var resultado = _servicio.Calcular(request.Expresion);
        return resultado.Exito ? Ok(resultado) : BadRequest(resultado);
    }
}