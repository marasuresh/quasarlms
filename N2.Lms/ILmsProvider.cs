namespace N2.Lms
{
	using Items;
	
	public interface ILmsProvider
	{
		IStorageItem Storage { get; }
	}
}
