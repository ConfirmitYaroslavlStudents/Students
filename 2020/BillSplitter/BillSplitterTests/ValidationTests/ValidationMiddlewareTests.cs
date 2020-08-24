using System.Threading.Tasks;
using BillSplitter.Attributes;
using BillSplitter.Validation.ValidationHandlers;
using BillSplitter.Validation.ValidationMiddleware;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace BillSplitterTests.ValidationTests
{
    public class ValidationMiddlewareTests : ValidationTestsBase
    {
        [Fact]
        public void NoRequireRolesAttribute_InvokesNext()
        {
            var httpContext = GetHttpContext(CreateEndpoint());
            bool called = false;

            var middleware = new ValidationMiddleware(
                context => { called = true; return Task.CompletedTask; }, 
                new RoleHandler());

            middleware.Invoke(httpContext);

            Assert.True(called);
        }

        [Fact]
        public void UserDontHaveRole_NoNextInvocation()
        {
            var httpContext = GetHttpContext(CreateEndpoint(new RequireRolesAttribute("Admin")));
            bool called = false;

            var middleware = new ValidationMiddleware(
                context => { called = true; return Task.CompletedTask; },
                new RoleHandler());

            middleware.Invoke(httpContext);

            Assert.False(called);
        }

        [Fact]
        public void UserHasRole_NoNextInvocation()
        {
            var httpContext = GetHttpContext(CreateEndpoint(new RequireRolesAttribute("Moderator")));
            bool called = false;

            var middleware = new ValidationMiddleware(
                context => { called = true; return Task.CompletedTask; },
                new RoleHandler());

            middleware.Invoke(httpContext);

            Assert.True(called);
        }

        private Endpoint CreateEndpoint(params object[] metadata)
        {
            return new Endpoint(
                context => Task.CompletedTask, 
                new EndpointMetadataCollection(metadata), 
                "Test endpoint");
        }
    }
}