namespace MotoDeliveryManager.UnitTests.Helpers;

/// <summary>
/// Provides a base class for unit test classes, encapsulating common setup for dependency injection and AutoFixture configurations.
/// This class is designed to be inherited by test classes that require mock dependencies to be injected into the system under test (SUT).
/// </summary>
public abstract class TestBase
{
    /// <summary>
    /// Gets the AutoFixture fixture used to generate test data and auto-mock dependencies.
    /// The fixture is configured with AutoMoqCustomization, enabling automatic mock creation for interface and abstract class dependencies.
    /// </summary>
    protected IFixture Fixture { get; private set; } = new Fixture().Customize(new AutoMoqCustomization());

    /// <summary>
    /// Gets the ServiceProvider used to resolve dependencies. It is built from the ServiceCollection configured in the RegisterTestDependencies method.
    /// This property is nullable to allow for flexible initialization during test setup.
    /// </summary>
    protected ServiceProvider? ServiceProvider { get; private set; }

    /// <summary>
    /// Initializes the service provider by registering test dependencies into a new ServiceCollection,
    /// then building a ServiceProvider from it. This method should be called at the beginning of each test setup to ensure
    /// that all dependencies are correctly registered and can be resolved for the tests.
    /// </summary>
    public void InitializeTestServices()
    {
        var serviceCollection = new ServiceCollection();
        RegisterTestDependencies(serviceCollection);

        ServiceProvider = serviceCollection.BuildServiceProvider();
    }

    /// <summary>
    /// When implemented in a derived class, registers dependencies required for the tests into the provided ServiceCollection.
    /// This method is an abstraction point that allows derived test classes to configure their specific dependencies required for testing.
    /// </summary>
    /// <param name="services">The ServiceCollection to which test dependencies should be added.</param>
    protected abstract void RegisterTestDependencies(ServiceCollection services);
}
