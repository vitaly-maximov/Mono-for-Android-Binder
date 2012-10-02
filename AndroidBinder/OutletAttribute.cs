using System;

namespace AndroidBinder
{
	[AttributeUsage (AttributeTargets.Field)]
	public class OutletAttribute : Attribute
	{
		public int ResourceId { get; private set; }

		public OutletAttribute (int resourceId)
		{
			ResourceId = resourceId;
		}
	}
}

