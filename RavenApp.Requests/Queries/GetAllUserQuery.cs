using MediatR;
using RavenApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenApp.Requests.Queries
{
    public class GetAllUserQuery: IRequest<IEnumerable<User>>
    {
        public bool AllowStaleResults { get; set; }
    }
}
