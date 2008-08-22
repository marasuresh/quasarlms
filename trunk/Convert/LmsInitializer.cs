namespace N2.Lms
{
	using N2.Plugin;
	
	[AutoInitialize]
	public class LmsInitializer: IPluginInitializer
	{
		#region IPluginInitializer Members

		public void Initialize(N2.Engine.IEngine engine)
		{
			engine.AddComponent("n2.LmsProvider", typeof(LmsProvider));
			engine.Resolve<LmsProvider>();
		}

		#endregion
	}
}
