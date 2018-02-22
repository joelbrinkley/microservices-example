using Account.Logging;
using EventStore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Account
{
    public class BankAccountRepository : IBankAccountRepository
    {
        private readonly IEventStore eventStore;
        private readonly ILog log;

        public BankAccountRepository(IEventStore eventStore, ILog log)
        {
            this.eventStore = eventStore;
            this.log = log;
        }

        public async Task AddOrUpdate(BankAccount bankAccount)
        {
            this.log.Information($"Adding or updating bank account {bankAccount.Id}");

            var uncommitedEvents = bankAccount.UncommittedEvents;

            if (!uncommitedEvents.Any()) return; //nothing to save
            
            await this.eventStore.AppendEvents(uncommitedEvents, bankAccount.Version);

            bankAccount.ClearUncommittedEvents();
        }

        public async Task<BankAccount> Find(Guid id)
        {
            this.log.Information($"Finding bank account {id}");

            var events = await this.eventStore.ReadEvents<BankAccount>(id.ToString());

            if (!events.Any())
            {
                this.log.Information($"Bank Account {id} was not found.");
                return null;
            }

            var bankAccount = new BankAccount();

            bankAccount.LoadFromHistory(events);

            return bankAccount;
        }
    }
}
