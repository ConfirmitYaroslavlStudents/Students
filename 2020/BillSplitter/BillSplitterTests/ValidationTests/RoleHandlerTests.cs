using BillSplitter.Validation.ValidationHandlers;
using BillSplitter.Validation.ValidationMiddleware;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Moq;
using Xunit;

namespace BillSplitterTests.ValidationTests
{
    public class RoleHandlerTests : ValidationTestsBase
    {
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
    }
}