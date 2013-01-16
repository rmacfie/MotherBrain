namespace MotherBrain.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class MotherBrainDependencyResolver : IDependencyResolver
    {
        IContainer container;

        public MotherBrainDependencyResolver(IContainer container)
        {
            this.container = container;
        }

        public object GetService(Type serviceType)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            throw new NotImplementedException();
        }
    }
}