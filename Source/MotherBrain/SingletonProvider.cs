namespace MotherBrain
{
	using System;

	public class SingletonProvider<TConcrete> : IProvider
	{
		readonly Func<IContainer, TConcrete> factory;
		readonly object syncRoot = new object();

		TConcrete instance;
		bool isCreated;

		public SingletonProvider(Func<IContainer, TConcrete> factory)
		{
			this.factory = factory;
		}

		public object GetInstance(IContainer container)
		{
			if (!isCreated)
			{
				lock (syncRoot)
				{
					if (!isCreated)
					{
						isCreated = true;
						instance = factory.Invoke(container);
					}
				}
			}

			return instance;
		}
	}
}