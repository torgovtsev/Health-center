using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hospital
{
	public class List
	{

		public List()
		{ 
			for(int i = 0; i < 7; i++)
				list.Add(new DayList());
		}

		public List<DayList> list = new List<DayList>(7);
	}
}
