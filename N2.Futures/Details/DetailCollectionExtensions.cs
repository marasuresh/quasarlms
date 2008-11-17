namespace N2.Details
{
	public static class DetailCollectionExtensions
	{
		public static DetailCollectionDictionaryWrapper<T> AsDictionary<T>(this DetailCollection collection)
		{
			return new DetailCollectionDictionaryWrapper<T>(collection);
		}

		public static DetailCollectionListWrapper<T> AsList<T>(this DetailCollection collection)
		{
			return new DetailCollectionListWrapper<T>(collection);
		}
	}
}
