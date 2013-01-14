namespace MotherBrain.Tests
{
    using System.Diagnostics;
    using Machine.Specifications;

    public class When_speedtesting_simple_lambda_registrations
    {
        const int iterations = 100000;
        const int maximumTimeMs = 100;

        static Stopwatch stopwatch;
        static IContainer container;

        Establish context = () =>
        {
            stopwatch = new Stopwatch();
            container = new Container();
            container.Register<AService, IService>(c => new AService());
        };

        Because of = () =>
        {
            stopwatch.Start();
            for (var i = 0; i < iterations; i++)
            {
                var instance = container.Resolve<IService>();
            }
            stopwatch.Stop();

            Debug.WriteLine("Built {0} instances in {1} ms.", iterations, stopwatch.ElapsedMilliseconds);
        };

        It should_be_quick = () =>
            stopwatch.ElapsedMilliseconds.ShouldBeLessThan(maximumTimeMs);
    }
}