using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WebApplication.Tests
{
    public class FakeHttpContext : HttpContextBase
    {
        public override IPrincipal User { get; set; }

        public FakeHttpContext(string userName)
        {
            var mockIdentity = new Mock<IIdentity>();
            mockIdentity.SetupGet(x => x.Name).Returns(userName);
            mockIdentity.SetupGet(x => x.IsAuthenticated).Returns(true);
            mockIdentity.SetupGet(x => x.AuthenticationType).Returns("windows");

            var mockUser = new Mock<IPrincipal>();
            mockUser.SetupGet(x => x.Identity).Returns(mockIdentity.Object);

            User = mockUser.Object;
        }

    }
    
}
