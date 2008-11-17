using System;

namespace N2.Definitions
{
	using N2.Plugin;

	[AutoInitialize, Obsolete]
	public class ItemBuilderInitializer: IPluginInitializer
	{
		#region IPluginInitializer Members

		public void Initialize(N2.Engine.IEngine engine)
		{
			engine.AddComponent("n2.itemBuilder", typeof(ItemBuilder));
		}

		#endregion
	}
}
