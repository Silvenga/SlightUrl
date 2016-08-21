namespace SlightUrl.Service.Tests
{
    using System;
    using System.Net;
    using System.Threading.Tasks;

    using FluentAssertions;

    using Microsoft.Owin.Testing;

    using Xunit;
    using Xunit.Abstractions;

    public class ValidationExceptionHanderFacts : IDisposable
    {
        private readonly ITestOutputHelper _helper;
        private readonly TestServer _server;

        public ValidationExceptionHanderFacts(ITestOutputHelper helper)
        {
            _helper = helper;
            _server = TestServer.Create<TestStartup>();
        }

        [Fact]
        public async Task Works()
        {
            // Act
            var result = await _server.CreateRequest("api/urls")
                                       .PostAsync();

            // Assert
            _helper.WriteLine(await result.Content.ReadAsStringAsync());
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        public void Dispose()
        {
            _server.Dispose();
        }
    }
}