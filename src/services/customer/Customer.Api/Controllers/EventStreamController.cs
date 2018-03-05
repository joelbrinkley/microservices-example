using Customers.Events;
using Customers.Queries;
using Domain;
using Domain.DomainEvents;
using Domain.Queries;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customers.Api.Controllers
{
    [Route("api/customers/events")]
    public class EventStreamController : Controller
    {
        private readonly IQueryHandler<GetCustomerEventStream, IEnumerable<DomainEvent<Customer>>> getAllEvents;

        public EventStreamController(IQueryHandler<GetCustomerEventStream, IEnumerable<DomainEvent<Customer>>> getAllEvents)
        {
            this.getAllEvents = getAllEvents;
        }

        [Route("")]
        public async Task<IActionResult> GetAllEvents()
        {
            var events = await getAllEvents.Execute(new GetCustomerEventStream());

            return Ok(events);
        }
    }
}
