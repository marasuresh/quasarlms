namespace N2.Lms.Install
{
	using Engine;
	using Plugin;

	[AutoInitialize]
	public class WorkflowInitializer : IPluginInitializer
	{
		public void Initialize(IEngine engine)
		{
			engine.AddComponent(
				"n2.lmsProvider",
				typeof(ILmsProvider),
				typeof(LmsProvider));
		}
	}
}
