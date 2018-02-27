import { Component, OnInit, ViewChild, Output, EventEmitter } from '@angular/core';
import { AccountService } from '../account-service';
import { BankAccount } from '../models/bank-account';

@Component({
    selector: 'all-accounts-list',
    templateUrl: './all-accounts-list.component.html'
})
export class ViewAllAccountsComponent implements OnInit {

    accounts: Array<BankAccount> = new Array<BankAccount>();
    selectedAccount: BankAccount;

    @Output() onSelected: EventEmitter<BankAccount> = new EventEmitter<BankAccount>();

    constructor(private accountService: AccountService) {

    }

    ngOnInit(): void {
        this.accountService.getAllAccounts()
            .then(accounts => this.accounts = accounts as Array<BankAccount>);
    }

    select(account: BankAccount) {
        this.selectedAccount = account;
        this.onSelected.emit(account);
    }
}