## MotherBrain
MotherBrain is a simple IoC container for .Net.


### Usage
    using MotherBrain;

    static void Main()
    {
        // Configure
        IContainer container = new MotherBrain.Container();
        
        container.RegisterTransient<IOtherService, OtherServiceImpl>();
        
        container.RegisterSingletonPerContext<ISession>(c => SessionFactory.OpenSession());
        container.RegisterTransient<IMyService>(c => new MyServiceImpl(c.Get<ISession>()));
        
        var settings = new SettingsImpl();
        container.RegisterConstant<ISettings>(settings);
        
        
        // Resolve
        var myService = container.Get<IMyService>();
    }
