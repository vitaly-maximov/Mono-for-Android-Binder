using System;
using System.Reflection;

using Android.App;

namespace AndroidBinder 
{
	[AttributeUsage (AttributeTargets.Class, Inherited = true)]
	public class BinderAttribute : Attribute
	{
		public int ResourceId { get; private set; }

		public bool BindOutlets { get; set; }

		public bool BindActions { get; set; }

		
		public BinderAttribute(int resourceId)
		{
			ResourceId = resourceId;

			BindOutlets = true;
			BindActions = false;
		}


		public void Bind(Activity activity)
		{
			if (activity == null)
				throw new ArgumentNullException();

			activity.SetContentView(ResourceId);

			if (BindOutlets)
			{
				FieldInfo[] fields = activity.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
				foreach (FieldInfo field in fields)
				{
					object[] outlets = field.GetCustomAttributes(typeof(OutletAttribute), false);
					if (outlets.Length == 0)
						continue;
					
					OutletAttribute outlet = (OutletAttribute) outlets[0];				
					field.SetValue(activity, activity.FindViewById(outlet.ResourceId));
				}
			}

			if (BindActions)
			{
				MethodInfo[] methods = activity.GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);
				foreach (MethodInfo method in methods)
				{
					object[] actions = method.GetCustomAttributes(typeof(ActionAttribute), false);
					if (actions.Length == 0)
						continue;

					foreach (ActionAttribute action in actions)
					{
						try 
						{
							FieldInfo field = activity.GetType().GetField(action.Field, BindingFlags.NonPublic | BindingFlags.Instance);
							if (field == null)
								throw new Exception();

							EventInfo handler = field.FieldType.GetEvent(action.Handler, BindingFlags.Public | BindingFlags.Instance);
							if (handler == null)
								throw new Exception();

							handler.AddEventHandler(field.GetValue(activity), Delegate.CreateDelegate(handler.EventHandlerType, activity, method));
						}
						catch
						{
							Console.WriteLine("Can't bind {0}.{1} with {2}", action.Field, action.Handler, method.Name); 
							continue;
						}
					}
				}
			}
		}
	}
}