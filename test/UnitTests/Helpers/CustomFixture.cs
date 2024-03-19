using AutoFixture;
using AutoFixture.AutoMoq;

namespace UnitTests.Helpers;

public class CustomFixture
{
    public static IFixture CreateFixture()
    {
        var fixture = new Fixture().Customize(new AutoMoqCustomization());

        fixture.Customize<DateOnly>(composer => composer.FromFactory<DateTime>(DateOnly.FromDateTime));

        return fixture;
    }
}