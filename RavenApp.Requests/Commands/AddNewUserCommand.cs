using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenApp.Requests.Commands
{
    public class AddNewUserCommand: IRequest<int>
    {
        public string Login { get; set; }
    }
}
