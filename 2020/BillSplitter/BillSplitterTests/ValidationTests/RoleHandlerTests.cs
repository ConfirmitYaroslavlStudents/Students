using System.Security.Claims;
using BillSplitter.Data;
using BillSplitter.Models;
using BillSplitter.Validation.ValidationHandlers;
using BillSplitter.Validation.ValidationMiddleware;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace BillSplitterTests.ValidationTests
{
    public class RoleHandlerTests : TestsBase
    {
        public RoleHandlerTests()
        {
            var user = new User{ Id = 1, Name = "Luke" };
            Context.Add(user);

            var bill = new Bill{ Id = 1, Name = "Pizza" };
            Context.Add(bill);

            var member = new Member{ Id = 1, User = user, Bill = bill, Role = "Moderator" };
            Context.Add(member);

            Context.SaveChanges();
        }

        [Fact]
        public void RouteWithoutBillId_ReturnsTrue()
        {
            var request = new Mock<HttpRequest>();
            request
                .Setup(r => r.RouteValues)
                .Returns(new RouteValueDictionary());

            var httpContext = new Mock<HttpContext>();
            httpContext
                .Setup(c => c.Request)
                .Returns(request.Object);

            var validationContext = new ValidationContext(httpContext.Object, null);

            var handler = new RoleHandler();

            Assert.True(handler.Handle(validationContext));
        }

        [Fact]
        public void MemberHasRequiredRole_ReturnsTrue()
        {
            var validationContext = new ValidationContext(GetHttpContext(), new []{ "Admin", "Moderator" });

            var handler = new RoleHandler();

            Assert.True(handler.Handle(validationContext));
        }

        [Fact]
        public void MemberHasNotRequiredRole_ReturnsTrue()
        {
            var validationContext = new ValidationContext(GetHttpContext(), new[] { "Admin" });

            var handler = new RoleHandler();

            Assert.False(handler.Handle(validationContext));
        }

        public HttpContext GetHttpContext()
        {
            var request = new Mock<HttpRequest>();
            request
                .Setup(r => r.RouteValues)
                .Returns(new RouteValueDictionary { { "billId", 1 } });

            var httpContext = new Mock<HttpContext>();
            httpContext
                .Setup(c => c.Request)
                .Returns(request.Object);

            var claims = new[]
            {
                new Claim("Id", "1"),
                new Claim(ClaimsIdentity.DefaultNameClaimType, "Luke"),
            };

            var user = new ClaimsPrincipal(new ClaimsIdentity(claims));
            httpContext
                .Setup(c => c.User)
                .Returns(user);

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped(p => new UnitOfWork(Context));
            httpContext
                .Setup(c => c.RequestServices)
                .Returns(serviceCollection.BuildServiceProvider());

            return httpContext.Object;
        }
    }
}