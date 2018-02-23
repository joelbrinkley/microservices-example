﻿using Domain;
using Domain.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Queries
{
    public class FindAccountQuery : IQuery
    {
        public Guid Id { get; set; }
    }
}
