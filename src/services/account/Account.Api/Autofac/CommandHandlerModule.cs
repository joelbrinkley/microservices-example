using Autofac;
using Account.Commands;
using Domain.Commands;

namespace Account.Autofac
{
    public class CommandHandlerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<WithdrawFromAccountHandler>().As<ICommandHandler<WithdrawFromBankAccount>>();
            builder.RegisterType<CreateAccountHandler>().As<ICommandHandler<CreateAccount>>();
            builder.RegisterType<DepositIntoAccountHandler>().As<ICommandHandler<DepositMoneyIntoAccount>>();
        }
    }
}
