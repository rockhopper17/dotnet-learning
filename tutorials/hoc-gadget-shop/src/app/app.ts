import { Component, signal } from '@angular/core';
import { RouterOutlet, RouterLink } from '@angular/router';
import { routes } from './app.routes';
// import { Inventory } from './AppComponents/inventory/inventory';

@Component({
  selector: 'app-root',
  // imports: [RouterOutlet, Inventory],
  imports: [RouterOutlet, RouterLink],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('hoc-gadget-shop');
}
