namespace MotherBrain.Tests
{
    using System.Diagnostics;
    using Machine.Specifications;

    public class When_speedtesting_composed_lambda_registrations
    {
        const int iterations = 10000;
        const int maximumTimeMs = 100;

        static Stopwatch stopwatch;
        static IContainer container;

        Establish context = () =>
        {
            stopwatch = new Stopwatch();
            container = new Container();
            container.RegisterTransient<AService, IService>(c => new AService());
            container.RegisterTransient<AComposedService, IComposedService>(c => new AComposedService(c.Get<IService>()));
            container.RegisterTransient<AComposedService2, IComposedService2>(c => new AComposedService2(c.Get<IService>(), c.Get<IComposedService>()));
        };

        Because of = () =>
        {
            stopwatch.Start();
            for (var i = 0; i < iterations; i++)
            {
                var instance = container.Get<IComposedService2>();
            }
            stopwatch.Stop();

            Debug.WriteLine("Built {0} composed instances in {1} ms.", iterations, stopwatch.ElapsedMilliseconds);
        };

        It should_be_quick = () =>
            stopwatch.ElapsedMilliseconds.ShouldBeLessThan(maximumTimeMs);
    }
}