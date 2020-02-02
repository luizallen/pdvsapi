using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;
using BAMCIS.GeoJSON;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Linq;

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

            var multiPolygon =
                "{\"type\":\"MultiPolygon\",\"coordinates\":[[[[30.0,20.0],[45.0,40.0],[10.0,40.0],[30.0,20.0]]],[[[15.0,5.0],[40.0,10.0],[10.0,20.0],[5.0,10.0],[15.0,5.0]]]]}";

            var point = "{\"type\":\"Point\",\"coordinates\":[-46.57421, -21.785741]}";

            fixture.Register(()=> JsonConvert.DeserializeObject<MultiPolygon>(multiPolygon));

            fixture.Register(() => JsonConvert.DeserializeObject<Point>(point));

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
