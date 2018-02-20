using EventStore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Account
{
    public class BankAccountRepository : IBankAccountRepository
    {
        private readonly IEventStore eventStore;

        public BankAccountRepository(IEventStore eventStore)
        {
            this.eventStore = eventStore;
        }

        public async Task AddOrUpdate(BankAccount bankAccount)
        {
            var uncommitedEvents = bankAccount.UncommittedEvents;

            if (!uncommitedEvents.Any()) return; //nothing to save
            
            await this.eventStore.AppendEvents(uncommitedEvents, bankAccount.Version);

            bankAccount.ClearUncommittedEvents();
        }

        public async Task<BankAccount> Find(Guid id)
        {
            var events = await this.eventStore.ReadEvents<BankAccount>(id.ToString());

            var bankAccount = new BankAccount();

            bankAccount.LoadFromHistory(events);

            return bankAccount;
        }
    }
}
