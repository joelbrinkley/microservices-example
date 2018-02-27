import { Component, ViewChild } from '@angular/core';
import { BankAccount } from '../models/bank-account';

@Component({
    selector: 'manage-accounts-view',
    templateUrl: './manage-accounts-view.component.html'
})
export class ManageAccountsViewComponent {

    constructor() {

    }

    selectAccount($event: BankAccount) {
        console.log('selected account: ' + $event.id)
    }
}