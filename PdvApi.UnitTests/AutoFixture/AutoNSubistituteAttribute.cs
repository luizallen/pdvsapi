using System;
using System.Linq;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PdvApi.UnitTests.AutoFixture
{
    public class AutoNSubstituteDataAttribute : AutoDataAttribute
    {
        public AutoNSubstituteDataAttribute()
            : this(FixtureFactory)
        {
        }

        public AutoNSubstituteDataAttribute(Func<IFixture> fixtureFactory)
            : base(fixtureFactory)
        {
        }

        public static IFixture FixtureFactory()
        {
            var fixture = new Fixture()
                .Customize(new AutoNSubstituteCustomization { ConfigureMembers = true });

            fixture.Customize<BindingInfo>(c => c.OmitAutoProperties());

            fixture.Behaviors
                .OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));

            fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));
            fixture.RepeatCount = 1;

            return fixture;
        }
    }
}
