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
	public class EnsureChildrenAttribute:
		TypeIntegrityAttribute,
		IInheritableDefinitionRefiner
	{
		public EnsureChildrenAttribute(params Type[] ensuredChildrenTypes)
		{
			this.Types = ensuredChildrenTypes;
		}

		#region IInheritableDefinitionRefiner Members

		public void Refine(ItemDefinition currentDefinition, IList<ItemDefinition> allDefinitions)
		{
			
			throw new NotImplementedException();
		}

		#endregion
	}
}
