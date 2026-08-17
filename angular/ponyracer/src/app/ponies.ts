import { Component, signal } from '@angular/core';
import { PonyModel } from './models/pony.model';

@Component({
    selector: 'ns-ponies',
    template: `
        <button (click)="refreshPonies()">Refresh</button>
        <ul>
            @for (pony of ponies(); track pony.id) {
                <li [style.color]="$even ? 'blue' : 'yellow'">
                    {{ pony.name }}
                </li>
            }
        </ul>
    `,
    imports: []
})

export class Ponies {
    protected readonly ponies = signal<Array<PonyModel>>([
        { id: 1, name: 'rainbos dash' },
        { id: 2, name: 'pinkie pie' }
    ]);

    protected refreshPonies(): void {
        this.ponies.set([
            { id: 3, name: 'fluttershy' },
            { id: 4, name: 'rarity' }
        ]);
    }
}