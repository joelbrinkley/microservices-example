export class CreateAccountCommand {
    customerId: string;
    startingBalance: number;

    constructor(customerId: string, startingBalance: number) {
        this.customerId = customerId;
        this.startingBalance = startingBalance;;
    }
}