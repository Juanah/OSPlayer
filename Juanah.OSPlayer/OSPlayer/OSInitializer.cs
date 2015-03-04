using System;
using System.Collections.Generic;
using ViewInfrastructure;

namespace OSPlayer
{
	/// <summary>
	/// Initialisiert den OSPlayer
	/// Jonas Ahlf 21.02.2015 15:17:53
	/// </summary>
	public class OSInitializer
	{
		private  List<object> _views;
		private Window Window{ get; set; }

		public OSInitializer (List<object> _views)
		{
			this._views = _views;
		}

		public void Run()
		{
			Window = new Window (new ViewStore (){ Views = _views });
			Window.Run (30, 30);
		}

		public void Stop()
		{
			Window.Close ();
		}
		
	}
}

