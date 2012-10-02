using System;

namespace AndroidBinder
{
	[AttributeUsage (AttributeTargets.Method, AllowMultiple = true)]
	public class ActionAttribute : Attribute
	{
		public string Field   { get; private set; }
		public string Handler { get; private set; }

		public ActionAttribute (string field, string handler)
		{
			Field  = field;
			Handler = handler;
		}
	}
}
