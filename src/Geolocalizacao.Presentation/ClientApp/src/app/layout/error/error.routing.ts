import { Routes } from '@angular/router';
import { AccessDeniedComponent } from './access-denied/access-denied.component';
import { NotFoundComponent } from './not-found/not-found.component';

export const errorRoute: Routes = [
    {
        path: 'notfound',
        component: NotFoundComponent,
        data: {
            authorities: [],
            pageTitle: 'error.title'
        },
    },
    {
        path: 'accessdenied',
        component: AccessDeniedComponent,
        data: {
            authorities: [],
            pageTitle: 'error.title',
            error403: true
        },
    }
];
