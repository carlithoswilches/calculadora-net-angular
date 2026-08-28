import { Component } from '@angular/core';

@Component({
  selector: 'app-calculadora',   // así se usa el componente en otro HTML: <app-calculadora>
  standalone: true,              // no necesita estar declarado en un NgModule
  imports: [],                   // aquí se agregan otros componentes/módulos que use
  templateUrl: './calculadora.html',
  styleUrl: './calculadora.css'
})
export class CalculadoraComponent {
  // Vacío a propósito: por ahora el componente solo muestra la interfaz.
  // Acá irá la lógica cuando hagamos la siguiente historia de usuario.
}