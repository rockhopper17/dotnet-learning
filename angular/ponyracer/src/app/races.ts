import { Component, signal } from "@angular/core";
import { RaceModel } from "./models/races.model";

@Component({
    selector: 'ns-races',
    template: `
        <h2>Races</h2>
        <button (click)="refreshRaces()">refresh the races list</button>
        <p>{{ races().length }} races</p>
    `
})
export class Races {
    protected readonly races = signal<Array<RaceModel>>([]);

    protected refreshRaces(): void {
        this.races.set([{ name: 'London' }, { name: 'Lyom' }]);
    }
}