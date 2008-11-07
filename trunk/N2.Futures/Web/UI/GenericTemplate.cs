namespace System.Web.UI
{
	/// <summary>
	/// <see cref="http://couldbedone.blogspot.com/2007/07/implementing-itemplate-as-anonymous.html"/>
	/// </summary>
	public class GenericTemplate : ITemplate
	{
		Action<Control> m_instantiate;

		public GenericTemplate(Action<Control> instantiate)
		{
			this.m_instantiate = instantiate;
		}

		#region ITemplate Members

		void ITemplate.InstantiateIn(Control container)
		{
			this.m_instantiate(container);
		}

		#endregion
	}
}