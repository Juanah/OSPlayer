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
			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadIdentity();
			GL.Ortho(-1, 1, -1, 1, -1, 1); //Sollte vielleicht geändert werden
			RenderViews ();
			SwapBuffers();
		}


		private void RenderViews()
		{
			foreach (var view in _viewStore.Views) {

				//Basis Rendern
				GL.Begin (view.GeometricType);
				view.Render ();
				foreach (var vec2 in view.Vector2D) {
					GL.Color4 (view.Color);
					//recalculate 
					Vector2 calculated = new Vector2 (vec2.X + view.offsetX, vec2.Y + view.offsetY);
					GL.Vertex2 (calculated);
				}
				GL.End ();
				//Falls unterviews vorhanden die auch Zeichnen
				if (view.InnerViews != null) {
					foreach (var innerView in view.InnerViews) {
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

		private void OnMMove(float x,float y)
		{
			//Console.WriteLine(String.Format("MouseMove x = {0}, y = {1}",x,y));
		}

		private void FireMouseDownEvent(float x,float y)
		{
			foreach (var view in _viewStore.Views) {
				foreach (var result in view.CheckPosition(x,y)) {
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

