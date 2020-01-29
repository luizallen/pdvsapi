using AutoFixture.Idioms;
using PdvApi.Infrastructure.Repositories;
using PdvApi.Infrastructure.Repositories.Abstractions;
using PdvApi.UnitTests.AutoFixture;
using Xunit;

namespace PdvApi.UnitTests.Infrastructure.Repositories
{
    public class PdvRepositoryTests
    {
        [Theory, AutoNSubstituteData]
        public void Sut_ShouldGuardItsClause(GuardClauseAssertion assertion)
            => assertion.Verify(typeof(PdvRepository).GetConstructors());

        [Theory, AutoNSubstituteData]
        public void Sut_Is_ITedProcessor(PdvRepository sut)
            => Assert.IsAssignableFrom<IPdvRepository>(sut);
    }
}
