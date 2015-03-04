using System;
using OpenTK;
using System.Collections.Generic;
using System.Drawing;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using ViewInfrastructure;
namespace OSPlayer
{
	/// <summary>
	/// Representiert ein echtes Fenster, in welchem Views angezeigt werden können
	/// Jonas Ahlf 22.02.2015 17:44:55
	/// </summary>
	public class Window : GameWindow 
	{
		private volatile ViewStore _viewStore;

		public ViewStore ViewStore{
			get{ return _viewStore; }
			set{ _viewStore = value; }
		}
		public Window (ViewStore store) :base(800,600)
		{
			_viewStore = store;

			MouseMove += (sender, e) =>
			{
					// Scale mouse coordinates from
					// (0, 0):(Width, Height) to
					// (-1, -1):(+1, +1)
					// Note, we must flip the y-coordinate
					// since mouse is reported with (0, 0)
					// at top-left and our projection uses
					// (-1, -1) at bottom left.
					float x = (e.X - Width) / (float)Width;
					float y = (Height- e.Y) / (float)Height;
					OnMMove(2 * x + 1, 2 * y - 1);
					//lines.Add(new Vector2(2 * x + 1, 2 * y - 1));
			};
			MouseDown += (object sender, MouseButtonEventArgs e) => {
				float x = (e.X - Width) / (float)Width;
				float y = (Height- e.Y) / (float)Height;
				FireMouseDownEvent(2 * x + 1, 2 * y - 1);
			};
			MouseUp += (object sender, MouseButtonEventArgs e) => {
				float x = (e.X - Width) / (float)Width;
				float y = (Height- e.Y) / (float)Height;
				FireMouseUpEvent(2 * x + 1, 2 * y - 1);
			};
		}

		protected override void OnRenderFrame (FrameEventArgs e)
		{

			GL.ClearColor (Color.White);
			GL.Clear(ClearBufferMask.ColorBufferBit);
			GL.Viewport(ClientRectangle);
			GL.MatrixMode(MatrixMode.Modelview);
			GL.LoadIdentity();
			GL.Ortho(-1, 1, -1, 1, -1, 1); //Sollte vielleicht geändert werden
			RenderViews ();
			SwapBuffers();
		}


		private void RenderViews()
		{
			foreach (var view in _viewStore.Views) {


				if (view.GetType() == typeof(TextView)) {
					RenderTextView(((TextView)view));
					continue;
				}

				//Basis Rendern
				GL.Begin (((View)view).GeometricType);
				((View)view).Render ();
				foreach (var vec2 in ((View)view).Vector2D) {
					GL.Color4 (((View)view).Color);
					//recalculate 
					Vector2 calculated = new Vector2 (vec2.X + ((View)view).offsetX, vec2.Y + ((View)view).offsetY);
					GL.Vertex2 (calculated);
				}
				GL.End ();
				//Falls unterviews vorhanden die auch Zeichnen
				if (((View)view).InnerViews != null) {
					foreach (var innerView in ((View)view).InnerViews) {
						GL.Begin (innerView.GeometricType);
						innerView.Render ();
						foreach (var vec2 in innerView.Vector2D) {
							GL.Color4 (innerView.Color);
							Vector2 calculated = new Vector2 (vec2.X + innerView.offsetX, vec2.Y + innerView.offsetY);
							GL.Vertex2 (calculated);
						}
						GL.End ();
					}
				}
			}
		}

		void RenderTextView(TextView textView)
		{
			GL.Enable(EnableCap.Texture2D);
			GL.BindTexture(TextureTarget.Texture2D, textView.TextRenderer.Texture);
			GL.Begin(PrimitiveType.Quads);

			GL.TexCoord2(textView.TextCoords[0]); GL.Vertex2(textView.Vector2D[0]);
			GL.TexCoord2(textView.TextCoords[1]); GL.Vertex2(textView.Vector2D[1]);
			GL.TexCoord2(textView.TextCoords[2]); GL.Vertex2(textView.Vector2D[2]);
			GL.TexCoord2(textView.TextCoords[3]); GL.Vertex2(textView.Vector2D[3]);

			GL.End();
		}

		private void OnMMove(float x,float y)
		{
			//Console.WriteLine(String.Format("MouseMove x = {0}, y = {1}",x,y));
		}

		private void FireMouseDownEvent(float x,float y)
		{
			foreach (var view in _viewStore.Views) {
				foreach (var result in ((View)view).CheckPosition(x,y)) {
					((View)result.Item2).Pressed ();
				}
			}
			Console.WriteLine(String.Format("MouseDown x = {0}, y = {1}",x,y));
		}

		private void FireMouseUpEvent(float x,float y)
		{
			Console.WriteLine(String.Format("MouseUp x = {0}, y = {1}",x,y));
		}
	}
}

