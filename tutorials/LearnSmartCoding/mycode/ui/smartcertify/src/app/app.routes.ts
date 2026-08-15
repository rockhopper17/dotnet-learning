import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home';
import { DataBindingsComponent } from './data-bindings/data-bindings';
import { ContactUs } from './pages/contact-us/contact-us';
import { About } from './pages/about/about';

export const routes: Routes = [
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    { path: 'home', component: HomeComponent },
    // { path: 'data-bindings', component: DataBindingsComponent },
    { path: 'data-bindings', loadComponent: () => import('./data-bindings/data-bindings')
        .then(m => m.DataBindingsComponent)},
    { path: 'contact-us', loadComponent: () => import('./pages/contact-us/contact-us')
        .then(m => m.ContactUs)},
    { path: 'about', loadComponent: () => import('./pages/about/about')
        .then(m => m.About)},
    { path: '*', redirectTo: 'home' }
];
