using Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using Customers.Commands;
using System.Threading.Tasks;

namespace Customers.Commands
{
    public class CreateCustomerCommandHandler : ICommandHandler<CreateCustomerCommand>
    {
        public Task Handle(CreateCustomerCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
