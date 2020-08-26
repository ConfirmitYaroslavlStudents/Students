using System.Security.Claims;
using BillSplitter.Data;
using BillSplitter.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace BillSplitterTests.ValidationTests
{
    public abstract class ValidationTestsBase : TestsBase
    {
        protected ValidationTestsBase()
        {
            var user = new User { Id = 1, Name = "Luke" };
            Context.Add(user);

            var bill = new Bill { Id = 1, Name = "Pizza" };
            Context.Add(bill);

            var member = new Member { Id = 1, User = user, Bill = bill, Role = "Moderator" };
            Context.Add(member);

            Context.SaveChanges();
        }

        protected HttpContext GetHttpContext(Endpoint endpoint = null)
        {
            var claims = new[]
            {
                new Claim("Id", "1"),
                new Claim(ClaimsIdentity.DefaultNameClaimType, "Luke"),
            };

            var user = new ClaimsPrincipal(new ClaimsIdentity(claims));

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped(p => new UnitOfWork(Context));

            var routeValuesFeatureMock = new Mock<IRouteValuesFeature>();
            routeValuesFeatureMock
                .Setup(r => r.RouteValues)
                .Returns(new RouteValueDictionary { { "billId", 1 } });

            var responseFeatureMock = new Mock<IHttpResponseFeature>();
            responseFeatureMock
                .Setup(r => r.StatusCode)
                .Returns(1);

            var collection = new FeatureCollection();
            collection.Set(routeValuesFeatureMock.Object);
            collection.Set(responseFeatureMock.Object);

            var context = new DefaultHttpContext(collection)
            {
                RequestServices = serviceCollection.BuildServiceProvider()
            };
            context.SetEndpoint(endpoint);
            context.User = user;

            return context;
        }
    }
}