using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace N2.Lms.Items.Lms
{
	using N2.Collections;
	
	internal interface IVirtualizable
	{
		bool IsVirtual { get; }
		
		ContentItem OriginalParent { get; set; }
		
		//ItemList GetChildren(ItemFilter filter);
	}
}
