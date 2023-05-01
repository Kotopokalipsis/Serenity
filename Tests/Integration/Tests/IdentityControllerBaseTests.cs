using Xunit;

[assembly: TestCaseOrderer("Xunit.Extensions.Ordering.TestCaseOrderer", "Xunit.Extensions.Ordering")]

namespace Integration.Tests
{
    public class IdentityControllerBaseTests : IClassFixture<InitialFixture>
    {
        private readonly InitialFixture _initialFixture;

        public IdentityControllerBaseTests(InitialFixture initialFixture)
        {
            _initialFixture = initialFixture;
        }

        [Fact]
        public async Task GetTokenFlow_SuccessTest()
        {
            await _initialFixture.InitialTest();
        }
    }
}