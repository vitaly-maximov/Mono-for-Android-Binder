using System;

using Android.App;
using Android.Widget;

namespace AndroidBinder
{
	[Activity (Label = "AndroidBinder", MainLauncher = true)]
	[Binder (Resource.Layout.Main, BindActions = true)]
	public class MainActivity : CustomActivity
	{
		private int _count = 0;

		[Outlet (Resource.Id.myButton)]
		private Button _button;

		[Action ("_button", "Click")]
		private void OnButtonClick(object sender, EventArgs e)
		{
			_button.Text = string.Format ("{0} clicks!", _count++);
		}
	}
}


