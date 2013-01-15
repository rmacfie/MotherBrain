namespace MotherBrain
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;

    public sealed class InstanceStore : IDisposable
    {
        readonly ConcurrentDictionary<Key, object> instances = new ConcurrentDictionary<Key, object>();

        public void Dispose()
        {
            foreach (var disposable in instances.Values.OfType<IDisposable>())
            {
                disposable.Dispose();
            }
        }

        public object GetOrAdd(Key key, Func<Key, object> factory)
        {
            return instances.GetOrAdd(key, factory);
        }
    }
}