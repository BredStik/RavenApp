using MediatR;
using Raven.Client;
using RavenApp.Entities;
using RavenApp.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenApp.RequestHandlers.QueryHandlers
{
    public class GetAllUserQueryHandler: IRequestHandler<GetAllUserQuery, IEnumerable<User>>
    {
        private readonly IDocumentSession _session;

        public GetAllUserQueryHandler(IDocumentSession session)
        {
            _session = session;
        }

        public IEnumerable<User>  Handle(GetAllUserQuery message)
        {
            var query = _session.Query<User>();

            if (!message.AllowStaleResults)
            {
                query.Customize(c => c.WaitForNonStaleResultsAsOfNow());
            }

            return query.ToList();
        }
    }
}
