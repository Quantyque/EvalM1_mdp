import { Routes } from '@angular/router';
import { ApplicationComponent } from './application/application.component';

export const routes: Routes = [
    { path: '', redirectTo: 'application', pathMatch: 'full' },
    { path: 'application',
      loadComponent: () => import('./application/application.component').then(m => m.ApplicationComponent)
    }
];
