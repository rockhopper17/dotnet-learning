import { Component, Input, ChangeDetectionStrategy } from '@angular/core';

import { HousingLocation } from '../housing-location';
import { Router, RouterModule } from '@angular/router';
@Component({
    selector: 'app-housing-location',
    imports: [RouterModule],
    template: `
    <section>
      <img class="listing-photo" [src]="housingLocation.photo" alt="exterior photo of {{housingLocation.name}}">
      <h2 class="listing-heading">{{housingLocation.name}}</h2>
      <p class="listing-location">{{housingLocation.city}}, {{housingLocation.state}}</p>
      <a [routerLink]="['/details',housingLocation.id]">learn more</a>
    </section>
  `,
    changeDetection: ChangeDetectionStrategy.Eager,
    styleUrls: ['./housing-location.component.css']
})
export class HousingLocationComponent {
  @Input() housingLocation!:HousingLocation;
}
