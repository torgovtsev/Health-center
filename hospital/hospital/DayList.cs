using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hospital
{
	public class DayList
	{
		public DayList()
		{
			for (int i = 0; i < 17; i++)
				values.Add(true);
		}

		public List<bool> values = new List<bool>(17); 
	}
}
