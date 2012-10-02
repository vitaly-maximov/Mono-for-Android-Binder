using System;

using Android.App;
using Android.OS;

namespace AndroidBinder
{
	public class CustomActivity : Activity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			object[] attributes = GetType().GetCustomAttributes(typeof(BinderAttribute), true);
			if ((attributes.Length == 0) || !(attributes[0] is BinderAttribute))
				return;

			BinderAttribute binder = attributes[0] as BinderAttribute;
			binder.Bind(this);
		}
	}
}

