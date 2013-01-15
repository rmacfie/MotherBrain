﻿namespace MotherBrain
{
	using System;

	public class SingletonProvider<T> : IProvider
	{
		readonly Func<IContainer, T> factory;
		readonly object syncRoot = new object();

		T instance;
		bool isCreated;

		public SingletonProvider(Func<IContainer, T> factory)
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