import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-data-bindings',
  imports: [FormsModule, CommonModule],
  templateUrl: './data-bindings.html',
  styleUrl: './data-bindings.css'
})
export class DataBindingsComponent {
// Interpolation and Property Binding
appName = 'SmartCertify';
logoUrl = 'https://smartlearnbykarthik.azurewebsites.net/assets/android-chrome-512x512.png';
appDescription = 'An amazing platform to test and certify your knowledge.';

// Two-Way Binding
username = '';

// *ngIf, *ngElse
isWelcomeVisible = true;

toggleWelcome() {
  this.isWelcomeVisible = !this.isWelcomeVisible;
}

// *ngFor
technologies = ['Angular', '.NET Core', 'C#', 'Azure', 'React'];

// Event Binding
clickCounter = 0;

incrementCounter() {
  this.clickCounter++;
}

// Attribute Directives: ngClass and ngStyle
isHighlighted = false;

getClass() {
  return this.isHighlighted ? 'text-primary' : 'text-secondary';
}

getStyle() {
  return this.isHighlighted
    ? { fontWeight: 'bold', fontSize: '24px' }
    : { fontWeight: 'normal', fontSize: '20px' };
}

toggleHighlight() {
  this.isHighlighted = !this.isHighlighted;
}
}
