using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace N2.Lms.Install
{
	using Persistence;
	using Lms.Items;
	using N2.Web;
	
	class LmsProvider: ILmsProvider
	{
		#region Fields

		readonly IPersister persister;
		readonly IHost host;

		#endregion Fields

		#region Constructor

		public LmsProvider(IPersister persister, IHost host)
		{
			this.persister = persister;
			this.host = host;
		}

		#endregion Constructor

		#region ILmsProvider Members

		Storage m_storage;
		public IStorageItem Storage {
			get {
				return this.m_storage
					?? (this.m_storage = this.GetStorage());
			}
		}

		#endregion

		#region Properties

		ContentItem Root {
			get { return this.persister.Get(this.host.CurrentSite.RootItemID); }
		}

		#endregion Properties

		#region Methods

		Storage GetStorage()
		{
			return this.Root.GetOrFindOrCreateChild<Storage>("Lms Storage", null);
		}

		#endregion Methods
	}
}
