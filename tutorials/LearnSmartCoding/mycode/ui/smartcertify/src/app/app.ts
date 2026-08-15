import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { About } from "./pages/about/about";
import { Header } from "./pages/header/header";
import { Footer } from "./pages/footer/footer";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, About, Header, Footer],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('smartcertify');
}
