import { Component, signal } from '@angular/core';
import { Races } from './races';
import { Ponies } from "./ponies";

@Component({
  selector: 'ns-root',
  imports: [Races, Ponies],
  // templateUrl: './app.html',
  template: `
    <h1>PonyRacer</h1>
    <!-- <h2>{{ numUsers() }} users</h2> -->
    <h2>welcome {{ user().name }}</h2>
    <p [textContent]="user().name"></p>
    <!-- <h2>welcome {{ user()?.name }}</h2> -->
    <ns-races />
    <ns-ponies />
    `,
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('ponyracer');
  // protected readonly numUsers = 42;
  // protected readonly numUsers = signal(42);
  protected readonly user = signal({ name: 'Homer' });
  // protected readonly user = signal<{ name: 'Homer' } | undefined>(undefined);
}
