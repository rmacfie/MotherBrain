namespace MotherBrain.Tests
{
	using Machine.Specifications;
	using System;
	using System.Linq;
	using System.Reflection.Emit;

	public class When_dynamically_creating_simple_object
	{
		static DynamicCreator<AService> creator;
		static AService instance;

		Establish context = () =>
		{
			creator = new DynamicCreator<AService>();
		};

		Because of = () =>
		{
			instance = creator.Create(t => null);
		};

		It should_create_an_instance = () =>
		{
			instance.ShouldNotBeNull();
			instance.ShouldBeOfType<AService>();
		};
	}
	public class When_dynamically_creating_complex_object
	{
		static DynamicCreator<AComposedService> creator;
		static Func<Type, object> factory;
		static AComposedService instance;

		Establish context = () =>
		{
			factory = t => t == typeof(IService) ? new AService() : null;
			creator = new DynamicCreator<AComposedService>();
		};

		Because of = () =>
		{
			instance = creator.Create(factory);
		};

		It should_create_an_instance = () =>
		{
			instance.ShouldNotBeNull();
			instance.ShouldBeOfType<AComposedService>();
		};
	}

	public class DynamicCreator<T>
	{
		readonly DynamicMethod creatorMethod;
		readonly Type[] parameters;

		public DynamicCreator()
		{
			var type = typeof(T);
			var ctor = type.GetConstructors()
				.OrderBy(x => x.GetParameters().Count())
				.LastOrDefault();

			if (ctor == null)
				throw new MotherBrainException(string.Format("Couldn't find a public constructor on '{0}'.", type.FullName));

			parameters = ctor.GetParameters().Select(x => x.ParameterType).ToArray();
			creatorMethod = new DynamicMethod(string.Empty, typeof(object), parameters, type);

			var ilGenerator = creatorMethod.GetILGenerator();
			ilGenerator.DeclareLocal(type);
			ilGenerator.Emit(OpCodes.Newobj, ctor);
			ilGenerator.Emit(OpCodes.Stloc_0);
			ilGenerator.Emit(OpCodes.Ldloc_0);
			ilGenerator.Emit(OpCodes.Ret);
		}

		public T Create(Func<Type, object> factory)
		{
			var args = parameters.Select(factory.Invoke).ToArray();
			return (T)creatorMethod.Invoke(null, args);
		}
	}
}
