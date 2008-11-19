using System;
using System.Collections.Generic;

namespace N2.Lms.Web.UI.WebControls
{
	public class TestChangedEventArgs: EventArgs
	{
		public TestChangedEventArgs()
		{
			this.NewAnswers = new Dictionary<int, string>();
		}

		public readonly IDictionary<int, string> NewAnswers;
	}
}
