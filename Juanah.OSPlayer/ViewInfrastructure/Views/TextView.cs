using System;
using System.Drawing;
using System.Collections.Generic;
using OpenTK;
namespace ViewInfrastructure
{
	/// <summary>
	/// Text view.
	/// </summary>
	public class TextView : View
	{
		private string _text = "";
		/// <summary>
		/// Initializes a new instance of the <see cref="ViewInfrastructure.TextView"/> class.
		/// </summary>
		public TextView ()
		{

		}

		public TextView (string text)
		{
			this._text = text;
		}
		
		public Color BackColor{ get; set; }
		/// <summary>
		/// Gets or sets the font.
		/// </summary>
		/// <value>The font.</value>
		public Font Font{get;set;} 
		/// <summary>
		/// Gets or sets the color of the brush.
		/// </summary>
		/// <value>The color of the brush.</value>
		public Brushes BrushColor{ get; set; }
		/// <summary>
		/// Gets or sets the position.
		/// </summary>
		/// <value>The position.</value>
		public PointF Position{ get; set; }
		/// <summary>
		/// Gets or sets the text renderer.
		/// </summary>
		/// <value>The text renderer.</value>
		public TextRenderer TextRenderer{ get; set; }
		/// <summary>
		/// Gets or sets the text coords.
		/// </summary>
		/// <value>The text coords.</value>
		public List<Vector2> TextCoords{ get; set; } 
		/// <summary>
		/// Gets or sets the text.
		/// </summary>
		/// <value>The text.</value>
		public string Text{ get{ return _text; } set{ _text = value; SetText (); } }


		private void SetText()
		{
			TextRenderer.Clear (BackColor);
			TextRenderer.DrawString(_text, Font, Brushes.White, Position);
		}
	}
}

