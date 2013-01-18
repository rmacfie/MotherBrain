namespace MotherBrain
{
    using System;
    using System.Linq;
    using System.Reflection.Emit;

    /// <summary>
    /// http://stackoverflow.com/questions/8219343/reflection-emit-create-object-with-parameters
    /// </summary>
    public class DynamicCreator<T>
    {
        readonly DynamicMethod creatorMethod;
        readonly Func<object[], T> factory;
        readonly Type[] parameters;
        readonly Type type;

        public DynamicCreator()
        {
            type = typeof(T);
            var ctor = type.GetConstructors().OrderBy(x => x.GetParameters().Count()).LastOrDefault();

            if (ctor == null)
                throw new MotherBrainException(string.Format("Couldn't find a public constructor on '{0}'.", type.FullName));

            parameters = ctor.GetParameters().Select(x => x.ParameterType).ToArray();
            creatorMethod = new DynamicMethod(string.Format("DynamicCtor-{0}", Guid.NewGuid()), type, new[] { typeof(object[]) }, true);

            var il = creatorMethod.GetILGenerator();

            //foreach (var parameter in parameters)
            //{
            //    il.DeclareLocal(parameter);
            //}
            
            il.Emit(OpCodes.Ldc_I4_0); // [0]
            il.Emit(OpCodes.Stloc_0); //[nothing]

            for (var i = 0; i < parameters.Length; i++)
            {
                EmitInt32(il, i); // [index]
                il.Emit(OpCodes.Stloc_0); // [nothing]
                il.Emit(OpCodes.Ldarg_0); //[args]
                EmitInt32(il, i); // [args][index]
                il.Emit(OpCodes.Ldelem_Ref); // [item-in-args-at-index]

                if (parameters[i] != typeof(object))
                    il.Emit(OpCodes.Unbox_Any, parameters[i]); // same as a cast if ref-type
            }

            il.Emit(OpCodes.Newobj, ctor);
            il.Emit(OpCodes.Ret);

            factory = (Func<object[], T>)creatorMethod.CreateDelegate(typeof(Func<object[], T>));
        }

        public T Create(Func<Type, object> argumentFactory)
        {
            var args = parameters.Select(argumentFactory.Invoke).ToArray();
            return factory.Invoke(args);
        }

        static void EmitInt32(ILGenerator il, int value)
        {
            switch (value)
            {
                case -1:
                    il.Emit(OpCodes.Ldc_I4_M1);
                    break;
                case 0:
                    il.Emit(OpCodes.Ldc_I4_0);
                    break;
                case 1:
                    il.Emit(OpCodes.Ldc_I4_1);
                    break;
                case 2:
                    il.Emit(OpCodes.Ldc_I4_2);
                    break;
                case 3:
                    il.Emit(OpCodes.Ldc_I4_3);
                    break;
                case 4:
                    il.Emit(OpCodes.Ldc_I4_4);
                    break;
                case 5:
                    il.Emit(OpCodes.Ldc_I4_5);
                    break;
                case 6:
                    il.Emit(OpCodes.Ldc_I4_6);
                    break;
                case 7:
                    il.Emit(OpCodes.Ldc_I4_7);
                    break;
                case 8:
                    il.Emit(OpCodes.Ldc_I4_8);
                    break;
                default:
                    if (value >= -128 && value <= 127)
                    {
                        il.Emit(OpCodes.Ldc_I4_S, (sbyte)value);
                    }
                    else
                    {
                        il.Emit(OpCodes.Ldc_I4, value);
                    }
                    break;
            }
        }
    }
}