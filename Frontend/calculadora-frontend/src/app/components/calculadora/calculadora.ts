import { Component } from '@angular/core';

@Component({
  selector: 'app-calculadora',
  standalone: true,
  imports: [],
  templateUrl: './calculadora.html',
  styleUrl: './calculadora.css'
})
export class CalculadoraComponent {
  // Guarda la expresión que se va armando con los clics del usuario
  // (reemplaza al input de texto libre)
  expresion: string = '';

  // Se ejecuta cada vez que el usuario toca un botón de número/operador/paréntesis
  // Simplemente concatena el carácter al final de la expresión actual
  agregarCaracter(caracter: string): void {
    this.expresion += caracter;
  }

  // Borra el último carácter (botón ⌫), útil si el usuario se equivoca
  borrarUltimo(): void {
    this.expresion = this.expresion.slice(0, -1);
  }

  // Limpia toda la expresión (botón C)
  limpiar(): void {
    this.expresion = '';
  }
}