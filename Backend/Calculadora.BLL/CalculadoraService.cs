using Calculadora.Entities;
using System.Globalization;

namespace Calculadora.BLL;

// Servicio de cálculo: recibe una expresión como texto y devuelve el resultado.
// Usa arreglos (List<string>) para tokenizar y pilas (Stack<T>) para respetar
// la precedencia de operadores (algoritmo Shunting Yard + evaluación postfija).
public class CalculadoraService
{
    public OperacionResponse Calcular(string expresion)
    {
        try
        {
            var tokens = Tokenizar(expresion);          // "3+5*2" -> ["3","+","5","*","2"]
            var postfija = AInfijaAPostfija(tokens);     // respeta precedencia y paréntesis
            var resultado = EvaluarPostfija(postfija);   // calcula el valor final

            return new OperacionResponse { Resultado = resultado, Exito = true, Mensaje = "OK" };
        }
        catch (Exception ex)
        {
            // Cualquier error (paréntesis mal puestos, división entre cero, etc.)
            return new OperacionResponse { Exito = false, Mensaje = ex.Message };
        }
    }

    // Separa el string en números y operadores/paréntesis individuales
    private List<string> Tokenizar(string expr)
    {
        var tokens = new List<string>();
        int i = 0;

        while (i < expr.Length)
        {
            if (char.IsWhiteSpace(expr[i])) { i++; continue; }

            if (char.IsDigit(expr[i]) || expr[i] == '.')
            {
                int inicio = i;
                while (i < expr.Length && (char.IsDigit(expr[i]) || expr[i] == '.')) i++;
                tokens.Add(expr[inicio..i]);
                continue;
            }

            if ("+-*/()".Contains(expr[i]))
            {
                tokens.Add(expr[i].ToString());
                i++;
                continue;
            }

            throw new Exception($"Carácter no válido: '{expr[i]}'");
        }

        return tokens;
    }

    // + y - valen 1, * y / valen 2 (a mayor número, mayor prioridad)
    private int Precedencia(string op) => op is "+" or "-" ? 1 : op is "*" or "/" ? 2 : 0;

    // Shunting Yard: reordena los tokens para que la precedencia quede implícita
    // en el orden (así la evaluación después es simple, sin ambigüedad)
    private List<string> AInfijaAPostfija(List<string> tokens)
    {
        var salida = new List<string>();
        var pila = new Stack<string>();

        foreach (var t in tokens)
        {
            if (double.TryParse(t, NumberStyles.Any, CultureInfo.InvariantCulture, out _))
            {
                salida.Add(t);
            }
            else if (t == "(")
            {
                pila.Push(t);
            }
            else if (t == ")")
            {
                while (pila.Count > 0 && pila.Peek() != "(") salida.Add(pila.Pop());
                if (pila.Count == 0) throw new Exception("Paréntesis desbalanceados");
                pila.Pop(); // descarta el "("
            }
            else // operador
            {
                while (pila.Count > 0 && pila.Peek() != "(" && Precedencia(pila.Peek()) >= Precedencia(t))
                    salida.Add(pila.Pop());
                pila.Push(t);
            }
        }

        while (pila.Count > 0)
        {
            var op = pila.Pop();
            if (op is "(" or ")") throw new Exception("Paréntesis desbalanceados");
            salida.Add(op);
        }

        return salida;
    }

    // Evalúa la expresión postfija usando una pila de números
    private double EvaluarPostfija(List<string> postfija)
    {
        var pila = new Stack<double>();

        foreach (var t in postfija)
        {
            if (double.TryParse(t, NumberStyles.Any, CultureInfo.InvariantCulture, out double num))
            {
                pila.Push(num);
                continue;
            }

            if (pila.Count < 2) throw new Exception("Expresión inválida");
            double b = pila.Pop(), a = pila.Pop();

            pila.Push(t switch
            {
                "+" => a + b,
                "-" => a - b,
                "*" => a * b,
                "/" => b == 0 ? throw new Exception("No se puede dividir entre cero") : a / b,
                _ => throw new Exception($"Operador no reconocido: {t}")
            });
        }

        if (pila.Count != 1) throw new Exception("Expresión inválida");
        return pila.Pop();
    }
}