using System;
using ViewInfrastructure;
using System.Collections.Generic;

namespace OSPlayer
{
	/// <summary>
	/// Hält alle Views
	/// </summary>
	public class ViewStore
	{
		public List<View> Views{ get{ return Views; } set{ Views = value; Views.Sort ((x,y) => y.Level -x.Level); } }
	}
}

