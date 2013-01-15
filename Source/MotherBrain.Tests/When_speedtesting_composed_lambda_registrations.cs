namespace MotherBrain.Tests
{
    using System.Diagnostics;
    using Machine.Specifications;

    public class When_speedtesting_composed_lambda_registrations : With_container
    {
        const int iterations = 10000;
        const int maximumTimeMs = 100;

        static Stopwatch stopwatch;

        Establish context = () =>
        {
            stopwatch = new Stopwatch();
            container.RegisterTransient<IService>(c => new AService());
            container.RegisterTransient<IComposedService>(c => new AComposedService(c.Get<IService>()));
            container.RegisterTransient<IComposedService2>(c => new AComposedService2(c.Get<IService>(), c.Get<IComposedService>()));
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