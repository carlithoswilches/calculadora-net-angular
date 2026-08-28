import { Component, signal } from '@angular/core';
import { CalculadoraComponent } from './components/calculadora/calculadora';

@Component({
  selector: 'app-root',
  imports: [CalculadoraComponent],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('calculadora-frontend');
}
