using MediatR;
using Raven.Client;
using RavenApp.Requests.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RavenApp.Controllers
{
    public class HomeController : Controller
    {
        //private readonly IDocumentSession _session;
        private readonly IMediator _mediator;

        public HomeController(IMediator mediator)//IDocumentSession session)
        {
            //_session = session;
            _mediator = mediator;
        }

        public ActionResult Index()
        {
            var userId = _mediator.Send(new AddNewUserCommand { Login = Request.LogonUserIdentity.Name });
            var allUsers = _mediator.Send(new RavenApp.Requests.Queries.GetAllUserQuery());
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }

    
}