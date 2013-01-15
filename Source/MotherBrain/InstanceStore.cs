using System;
using System.Collections.Concurrent;
using System.Linq;

namespace MotherBrain
{
	public class InstanceStore : IDisposable
	{
		readonly ConcurrentDictionary<Key, object> instances = new ConcurrentDictionary<Key, object>();

		public void Dispose()
		{
			foreach (var disposable in instances.Values.OfType<IDisposable>())
			{
				disposable.Dispose();
			}
		}
	}
}