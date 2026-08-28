namespace Calculadora.Entities
{
    // Ahora el frontend ya no manda dos números sueltos, sino una expresión completa,
    // por ejemplo: "3 + 5 * (2 - 8) / 4"
    public class ExpresionRequest
    {
        // La expresión matemática completa, como texto
        public string Expresion { get; set; } = string.Empty;
    }

    // La respuesta se mantiene igual: resultado, si hubo éxito, y un mensaje
    public class OperacionResponse
    {
        public double Resultado { get; set; }
        public bool Exito { get; set; }
        public string Mensaje { get; set; } = string.Empty;
    }
}