using System;

namespace ViewInfrastructure
{
	/// <summary>
	/// Stellt den breich dar, indem eine Aktion erwartet wird
	/// Jonas Ahlf 22.02.2015 23:21:24
	/// </summary>
	public interface IHitbox
	{
		bool IsHit (float[] offset,float[] cursor);
	}
}

