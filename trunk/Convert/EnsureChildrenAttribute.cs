using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace N2.Definitions
{
	using N2.Integrity;
	
	[AttributeUsage(
		AttributeTargets.Class,
		AllowMultiple = true,
		Inherited = true)]
	public class EnsureChildrenAttribute: Attribute
	{
		public EnsureChildrenAttribute(params Type[] ensuredChildrenTypes)
		{
			this.Types = ensuredChildrenTypes;
		}

		public Type[] Types { get; set; }
	}
}
