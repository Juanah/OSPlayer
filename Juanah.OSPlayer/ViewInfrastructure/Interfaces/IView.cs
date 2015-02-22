using System;

namespace ViewInfrastructure
{
	/// <summary>
	/// Stellt das Basis View dar welches von jedem View Implementiert sein muss
	/// Jonas Ahlf 21.02.2015 12:20:55
	/// </summary>
	public interface IView
	{
		int Level{ get; set; }
	}
}

