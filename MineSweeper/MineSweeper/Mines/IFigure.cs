using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MineSweeper.Mines
{
	interface IFigure
	{
		void Draw(PaintEventArgs e);
	}
	class ClassicMine : IFigure
	{
		public void Draw(PaintEventArgs e)
		{
			var CRect = new RectangleF(-0.6f, -0.6f, 1.2f, 1.2f);
			var CBrush = new SolidBrush(Color.Black);
			e.Graphics.FillEllipse(CBrush, CRect);

			Single IRad = 0.5f;
			Single ORad = 0.8f;
			var Pen = new Pen(Color.Black, 0.15f);
			for (Single i = 0; i <= 1.75f * Math.PI; i += 0.25f * (float)Math.PI)
			{
				var Inner = new PointF((float)(IRad * Math.Cos(i)), (float)(IRad * Math.Sin(i)));
				var Outer = new PointF((float)(ORad * Math.Cos(i)), (float)(ORad * Math.Sin(i)));
				e.Graphics.DrawLine(Pen, Inner, Outer);
			}
		}
	}
	class Flag: IFigure
	{

		public void Draw(PaintEventArgs e)
		{
			var PoleTop = new PointF(0, -0.7f);
			var PoleBottom = new PointF(0, 0.5f);
			var FlagTip = new PointF(-0.7f, -0.3f);
			var FlagBottom = new PointF(0, 0.1f);
			var BaseTL = new PointF(-0.5f, 0.5f);
			var BaseBL = new PointF(-0.7f, 0.7f);
			var BaseTR = new PointF(0.5f, 0.5f);
			var BaseBR = new PointF(0.7f, 0.7f);

			var mPen = new Pen(Color.Brown, 0.1f);
			var mBrush = new SolidBrush(Color.Red);
			e.Graphics.DrawLine(mPen, PoleTop, PoleBottom);

			e.Graphics.DrawPolygon(mPen, new PointF[] { PoleTop, FlagTip, FlagBottom });
			e.Graphics.DrawPolygon(mPen, new PointF[] { BaseTL, BaseBL, BaseBR, BaseTR });
					
		}
	}
	
}
