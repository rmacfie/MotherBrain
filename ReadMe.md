## MotherBrain
MotherBrain is a simple IoC container for .Net.


### Usage
    using MotherBrain;

    static void Main()
    {
        IContainer container = new MotherBrain.Container();

        container.RegisterSingletonPerContext<ISession>(c => SessionFactory.OpenSession());
        container.RegisterTransient<IMyService>(c => new MyServiceImpl(c.Get<ISession>()));

        var session = container.Get<ISession>();
    }
