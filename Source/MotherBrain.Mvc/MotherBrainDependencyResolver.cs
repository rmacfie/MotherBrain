namespace MotherBrain.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class MotherBrainDependencyResolver : IDependencyResolver
    {
        readonly IContainer container;

        public MotherBrainDependencyResolver(IContainer container)
        {
            this.container = container;
        }

        public object GetService(Type serviceType)
        {
	        return container.Get(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
	        return container.GetAll(serviceType);
        }
    }
}