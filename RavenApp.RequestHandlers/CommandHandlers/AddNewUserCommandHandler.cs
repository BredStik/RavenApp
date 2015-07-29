using MediatR;
using Raven.Client;
using RavenApp.Entities;
using RavenApp.Requests.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenApp.RequestHandlers.CommandHandlers
{
    public class AddNewUserCommandHandler: IRequestHandler<AddNewUserCommand, int>
    {
        private readonly IDocumentSession _session;

        public AddNewUserCommandHandler(IDocumentSession session)
        {
            _session = session;
        }

        public int Handle(AddNewUserCommand message)
        {
            var newUser = new User{Login = message.Login};
            _session.Store(newUser);
            _session.SaveChanges();

            return newUser.Id;
        }
    }
}
