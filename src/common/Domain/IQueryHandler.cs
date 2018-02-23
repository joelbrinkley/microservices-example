using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Queries
{
    public interface IQueryHandler<TQuery, TResult> where TQuery: IQuery
    {
        Task<TResult> Execute(TQuery query);
    }
}
