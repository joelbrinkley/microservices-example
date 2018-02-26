import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { SettingsProvider } from "../configuration/settings.provider";
import { CreateAccountCommand } from "./models/create-account-command";
import { BankAccount } from "./models/bank-account";

@Injectable()
export class AccountService {

    private baseUrl: string;

    constructor(private http: HttpClient, settings: SettingsProvider) {
        this.baseUrl = settings.configuration.baseUrl;
    }

    getAllAccounts(): Promise<Array<BankAccount>> {
        return this.http.get<Array<BankAccount>>(this.baseUrl + `api/accounts/`).toPromise<Array<BankAccount>>();
    }

    getAccount(bankAccountId: string): Promise<BankAccount> {
        return this.http.get<BankAccount>(this.baseUrl + `api/accounts/${bankAccountId}`).toPromise<BankAccount>();
    }

    createAccount(customerId: string, startingBalance: number) {
        var createAccountCommand = new CreateAccountCommand(customerId, startingBalance);
        return this.http.post(this.baseUrl + "api/accounts", createAccountCommand).toPromise();
    }
}