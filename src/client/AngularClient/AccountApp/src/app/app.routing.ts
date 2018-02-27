import { Routes, RouterModule } from '@angular/router';

import { ManageAccountsViewComponent } from './accounts/index';

const appRoutes: Routes = [

    { path: '', component: ManageAccountsViewComponent, pathMatch: 'full' },
    { path: 'account-management', component: ManageAccountsViewComponent, pathMatch: 'full' },
    //// otherwise redirect to home
    //{ path: '**', redirectTo: 'login' }
];

export const routing = RouterModule.forRoot(appRoutes);
