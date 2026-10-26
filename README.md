# Calculadora .NET + Angular

> **EN:** Learning project. A web calculator with an Angular frontend and an ASP.NET Core Web API backend in layers. It evaluates full expressions such as `3 + 5 * (2 - 8) / 4`, respecting operator precedence and parentheses, using the Shunting Yard algorithm and stacks.

![.NET](https://img.shields.io/badge/.NET-10-512BD4?logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?logo=csharp&logoColor=white)
![Angular](https://img.shields.io/badge/Angular-22-DD0031?logo=angular&logoColor=white)
![TypeScript](https://img.shields.io/badge/TypeScript-3178C6?logo=typescript&logoColor=white)
![Estado](https://img.shields.io/badge/estado-proyecto_de_aprendizaje-blue)

## Qué es

Una calculadora web dividida en dos partes:

- **Frontend (Angular):** el usuario arma la expresión con botones.
- **Backend (ASP.NET Core):** recibe la expresión como texto, la evalúa y devuelve el resultado o un mensaje de error.

Lo hice para practicar cómo un frontend y un backend se comunican por HTTP, cómo se organiza una API en capas y cómo se usan las estructuras de datos para resolver un problema real.

## Cómo funciona el cálculo

El servicio `CalculadoraService` resuelve la expresión en tres pasos:

1. **Tokenizar:** separa el texto en números, operadores y paréntesis. Por ejemplo, `"3+5*2"` se convierte en `["3","+","5","*","2"]`.
2. **Pasar a notación postfija con el algoritmo Shunting Yard:** usa una **pila** para reordenar los tokens. Así la precedencia (`*` y `/` antes que `+` y `-`) y los paréntesis quedan resueltos por el orden.
3. **Evaluar la notación postfija:** recorre los tokens con otra **pila** de números y calcula el resultado.

Si la expresión tiene un carácter inválido, paréntesis desbalanceados o una división entre cero, la API responde **400 Bad Request** con un mensaje claro.

## Arquitectura

```
Backend/
├── Calculadora.API        → Controller, CORS y configuración (capa de presentación)
├── Calculadora.BLL        → CalculadoraService: lógica de negocio
└── Calculadora.Entities   → Modelos de entrada y salida (ExpresionRequest, OperacionResponse)
Frontend/
└── calculadora-frontend   → Angular: componente standalone con los botones y la llamada HTTP
```

### Endpoint

```http
POST /api/calculadora
Content-Type: application/json

{ "expresion": "3 + 5 * (2 - 8)" }
```

Respuesta:

```json
{ "resultado": -27, "exito": true, "mensaje": "OK" }
```

## Cómo ejecutarlo

Necesitas el .NET 10 SDK y Node.js con Angular CLI.

**1. Backend**, que queda en `http://localhost:5160`:

```bash
cd Backend/Calculadora.API
dotnet run
```

**2. Frontend**, que queda en `http://localhost:4200`:

```bash
cd Frontend/calculadora-frontend
npm install
npx ng serve
```

## Lo que aprendí

- A organizar una API en capas (API, BLL, Entities) y a separar responsabilidades.
- A configurar CORS para que Angular pueda llamar a la API.
- A consumir una API desde Angular con `HttpClient` y a manejar los errores que devuelve el servidor.
- A aplicar pilas y el algoritmo Shunting Yard en un problema concreto.
- A trabajar por historias de usuario: el frontend se construyó a partir de la HU1-FRONT.

## Mejoras pendientes

- [ ] Soportar números negativos, como `-3` o `2 * -1`. Hoy el signo menos solo funciona como resta.
- [ ] Agregar pruebas unitarias del `CalculadoraService` con xUnit.
- [ ] Inyectar el servicio con inyección de dependencias en vez de crearlo con `new()` en el controller.
- [ ] Mover la URL de la API a los `environments` de Angular.

## Autor

**Carlos Andrés Wilches Guerra** · [GitHub](https://github.com/carlithoswilches) · candreswilches@gmail.com
