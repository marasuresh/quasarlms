namespace N2.Lms
{
	using N2.Plugin;
	using N2.Templates.Services;
	
	[AutoInitialize]
	public class LmsInitializer: IPluginInitializer
	{
		#region IPluginInitializer Members

		public void Initialize(N2.Engine.IEngine engine)
		{
			//engine.AddComponent("n2.LmsProvider", typeof(LmsProvider));
			//engine.Resolve<LmsProvider>();
			
			System.Diagnostics.Trace.WriteLine("Lms initializer");
			
			engine.AddComponent(
				"n2.templates.notfoundhandler",
				typeof(NotFoundHandler));
		}

		#endregion
	}
}
