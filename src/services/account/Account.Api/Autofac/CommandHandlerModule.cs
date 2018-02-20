using Autofac;
using Account.Commands;
using Domain.Commands;

namespace Account.Autofac
{
    public class CommandHandlerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ICommandHandler<WithdrawFromBankAccount>>().As<WithdrawFromAccountHandler>();
            builder.RegisterType<ICommandHandler<CreateAccount>>().As<CreateAccountHandler>();
            builder.RegisterType<ICommandHandler<DepositMoneyIntoAccount>>().As<DepositMoneyIntoAccount>();
        }
    }
}
