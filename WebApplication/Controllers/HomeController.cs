using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using WebApplication.Infrastructure;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {

        private readonly HttpContextBase _context;
        private readonly IPrincipal _user;

        public HomeController(IHttpContextFactory httpContextFactory)
        {
            _context = httpContextFactory.Create();
            _user = _context.User;
        }

        public ActionResult Index()
        {
            //check roles for user
            var role = RollCheck(_user.Identity.Name ?? "domain\\user");

            //create the view model based on role.
            var vm = new ButtonGroupViewModel();
            vm.Buttons = role == "admin" ? new List<string> { "Button 1", "Button 2", "Button 3", "Secret Button" } : new List<string> { "Button 1", "Button 2" };
            vm.CurrentUser = _user.Identity.Name?.Split('\\')[1] ?? "not found";
            vm.CurrentRole = role;
            vm.Message = $"Hello, {vm.CurrentUser}.  You currently have this role: {vm.CurrentRole}";
            return View(vm);
        }

        public ActionResult About()
        {
     
            //check roles for user
            var role = RollCheck(_user.Identity.Name ?? "domain\\user");

            //create the view model based on role.
            var vm = new FancyButtonGroupViewModel();
            vm.Buttons = role == "admin" ? new List<Button>
            {
                new Button {Name ="Default", Style="btn-default" },
                new Button {Name ="Primary", Style="btn-primary" },
                new Button {Name ="Success", Style="btn-success" },
                new Button {Name ="Info", Style="btn-info" },
                new Button {Name ="Warning", Style="btn-warning" },
                new Button {Name ="Danger", Style="btn-danger" }

            } : new List<Button> { new Button { Name = "Button 1", Style = "btn-default" } };

            vm.CurrentUser = _user.Identity.Name?.Split('\\')[1] ?? "not found";
            vm.CurrentRole = role;
            vm.Message = $"Hello, {vm.CurrentUser}.  You currently have this role: {vm.CurrentRole}";
            return View(vm);
        }


        /// <summary>
        /// do some role checking
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private string RollCheck(string user)
        {
            return user == "MAINPC\\Bryan" ? "admin" : "user";
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}