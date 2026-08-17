import { Component, signal } from "@angular/core";
import { RaceModel } from "./models/races.model";

@Component({
    selector: 'ns-races',
    template: `
        <h2>Races</h2>
        <button (click)="refreshRaces()">refresh the races list</button>
        <p>{{ races().length }} races</p>
        <div>
            <ul>
                @for (race of races(); track race.id) {
                    <li [class.grey]="$even">{{ race.name }}</li>
                }
            </ul>
            </div>
    `
})
export class Races {
    protected readonly races = signal<Array<RaceModel>>([]);

    protected refreshRaces(): void {
        this.races.set([{ id: 1, name: 'London' }, { id: 2, name: 'Lyom' }]);
    }
}