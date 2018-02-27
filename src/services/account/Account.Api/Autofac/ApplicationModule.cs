using Autofac;
using Account.Commands;
using Domain.Commands;
using Domain.Queries;

namespace Account.Autofac
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<WithdrawFromAccountHandler>().As<ICommandHandler<WithdrawFromBankAccount>>();
            builder.RegisterType<CreateAccountHandler>().As<ICommandHandler<CreateAccount>>();
            builder.RegisterType<DepositIntoAccountHandler>().As<ICommandHandler<DepositMoneyIntoAccount>>();
                        
        }
    }
}
