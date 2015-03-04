using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using System.Drawing;

namespace ViewInfrastructure
{
	/// <summary>
	/// Stellt die Basis aller Views dar
	/// Jonas Ahlf 21.02.2015 12:15:55
	/// </summary>
	public class View : IView
	{

		public View ()
		{
			offsetX = 0.0f;
			offsetY = 0.0f;
		}
		

		public void Render()
		{
			if (onRender != null) {
				onRender (this);
			}
		}

		public void Display()
		{
			if (onDisplay != null) {
				onDisplay (this);
			}
		}

		public void Release()
		{
			if (onReleased != null) {
				onReleased (this);
			}
		}

		public void Pressed()
		{
			if (onPressed != null) {
				onPressed (this);
			}
		}

		public void Destroy()
		{
			if (onDestroy != null) {
				onDestroy (this);
			}
		}

		#region IView implementation
		public int Level {
			get {
				return Level;
			}
			set {
				Level = value;
			}
		}
		#endregion

		#region Member
		/// <summary>
		/// Gibt an welche form das View hat (Quadrat, Dreieck usw.)
		/// </summary>
		/// <value>The type of the geometric.</value>
		public PrimitiveType GeometricType{ get; set; }
		/// <summary>
		/// 2d Vectoren
		/// </summary>
		/// <value>The vector2 d.</value>
		public List<Vector2> Vector2D{ get; set; }
		/// <summary>
		/// 3D Vectoren
		/// </summary>
		/// <value>The vector3 d.</value>
		public List<Vector3> Vector3D{ get; set; }
		/// <summary>
		/// Gibt die Farbe des Views an
		/// </summary>
		/// <value>The color.</value>
		public Color Color{ get; set; }
		/// <summary>
		/// Die innerenViews welche immer nach dem baseView gerendert werden
		/// </summary>
		/// <value>The inner views.</value>
		public List<View> InnerViews{ get; set; }
		/// <summary>
		/// Gets or sets the offset x.
		/// </summary>
		/// <value>The offset x.</value>
		public float offsetX{ get; set; }
		/// <summary>
		/// Gets or sets the offset y.
		/// </summary>
		/// <value>The offset y.</value>
		public float offsetY{ get; set; }
		public IHitbox Hitbox{ get; set; }
		#endregion

		#region Events
		public delegate void OnRender(object sender);
		public event OnRender onRender;

		public delegate void OnDisplay(object sender);
		public event OnDisplay onDisplay;

		public delegate void OnPressed(object sender);
		public event OnPressed onPressed;

		public delegate void OnReleased(object sender);
		public event OnReleased onReleased;

		public delegate void OnDestroy(object sender);
		public event OnDestroy onDestroy;

		#endregion

		#region Util

		/// <summary>
		/// Gibt einen boolischen wert zurück, der prüft, ob der bereich der Aktion in dem des Views ist, 
		/// unter unterviews liegt
		/// </summary>
		/// <returns>The position.</returns>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public List<Tuple<bool,object>> CheckPosition (float x, float y)
		{

			var result = new List<Tuple<bool,object>> ();
			if (Hitbox == null) {
				return result;
			}
			if(Hitbox.IsHit(new float[]{offsetX,offsetY},new float[]{x,y}))
			{
				result.Add(new Tuple<bool, object>(true,this));
			}

			//Alle Innerviews
			if (InnerViews != null) {
				foreach (var view in InnerViews) {
					result.AddRange (view.CheckPosition (x, y));
				}
			}
			return result;
		}

		#endregion

	}
}

