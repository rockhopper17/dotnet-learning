import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
// import { About } from "./pages/about/about";
import { Header } from "./pages/header/header";
import { Footer } from "./pages/footer/footer";
// import { Home } from "./components/home/home";
// import { DataBindingsComponent } from "./data-bindings/data-bindings";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Header, Footer],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('smartcertify');
}
