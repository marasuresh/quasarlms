using System.Collections.Generic;
using System.Linq;

namespace N2.ACalendar
{
	partial class ACalendarContainer
	{
		public ACalendarContainer()
		{
			this.Title = "Хранилище календарей";
		}

		public IEnumerable<ACalendar> MyCalendars {
			get {
				return this.GetChildren().OfType<ACalendar>();
			}
		}
	}
}
