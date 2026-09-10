import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-calculadora',
  standalone: true,
  imports: [],
  templateUrl: './calculadora.html',
  styleUrl: './calculadora.css'
})
export class CalculadoraComponent {
  expresion: string = '';
  resultado: string = '-'; // lo que se muestra en "Resultado"

  constructor(private http: HttpClient) {}

  agregarCaracter(caracter: string): void {
    this.expresion += caracter;
  }

  borrarUltimo(): void {
    this.expresion = this.expresion.slice(0, -1);
  }

  limpiar(): void {
    this.expresion = '';
    this.resultado = '-';
  }

  // Envía la expresión al backend y muestra el resultado (o el error)
  calcular(): void {
    this.http.post<any>('http://localhost:5160/api/calculadora', { expresion: this.expresion })
      .subscribe({
        next: (res) => this.resultado = res.resultado,
        error: (err) => this.resultado = err.error?.mensaje ?? 'Error'
      });
  }
}