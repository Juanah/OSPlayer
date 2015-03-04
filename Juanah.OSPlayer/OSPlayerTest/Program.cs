using System;
using System.Collections.Generic;
using ViewInfrastructure;
using System.Drawing;
using OpenTK;
using OSPlayer;
namespace OSPlayerTest
{
	class MainClass
	{
		private static List<object> views = new List<object> ();
		private static float XOffset = 0.0f;
		public static void Main (string[] args)
		{
			Console.WriteLine ("Teste den OSPlayer");
			View baseView = GenerateTestView (ColorTranslator.FromHtml("#80deea"), 0.2f);
			baseView.onPressed += (object sender) => {
				Console.WriteLine ("baseView has been clicked");
				((View)sender).offsetX += 0.03f;
			};
			View innerView = GenerateTestView (Color.Red, 0.01f);
			innerView.onPressed += (object sender) => {
				Console.WriteLine ("innerView has been clicked");
				baseView.offsetX = 0.0f;
			};
			baseView.InnerViews = new List<View> ();
			baseView.InnerViews.Add (innerView);
			views.Add (baseView);
			OSInitializer initializer = new OSInitializer (views);
			initializer.Run ();
			Console.ReadKey ();
		}


		private static View GenerateTestView(Color color,float size)
		{
			List<Vector2> vectors = new List<Vector2> ();
			vectors.Add (new Vector2 (-size, -size));
			vectors.Add (new Vector2 (-size, size));
			vectors.Add (new Vector2 (size, size));
			vectors.Add (new Vector2 (size, -size));

			IHitbox hitbox = new Hitbox () {
				LeftCorner = new float[]{-size, size},
				RightCorner = new float[]{size, -size}
			};

			View view = new View () {
				Color = color,
				GeometricType = OpenTK.Graphics.OpenGL.PrimitiveType.Quads,
				Vector2D = vectors,
				Hitbox = hitbox,
			};

			return view;
		}

	}
}
