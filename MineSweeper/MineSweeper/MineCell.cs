using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MineSweeper.Mines;
namespace MineSweeper
{
	public enum MineCellView //Что может быть отображено в ячейке
	{
		Button,
		Mine,
		Number, 
		Flag
	}
	//Ячейка, где будут отображаться мины, цифры, флаг
	public partial class MineCell : UserControl
	{
		public MineCell()
		{
			InitializeComponent();
		}
		Field field;
		public int i, j;
		MineCellView mView;
		int mNumber;

		public MineCell(Field field, int i1, int j1)
		{
			// TODO: Complete member initialization
			this.field = field;
			this.i = i1;
			this.j = j1;
			InitializeComponent();
		}

		
		
		Color mButtonColor = Color.Gray;
		private int i1;
		private int j1;

		public Color ButtonColor 
		{ 
			get { return mButtonColor; } 
			set { mButtonColor = value; } 
		}
		
		public int Number 
		{ 
			get { return mNumber; } 
			set { mNumber = value; } 
		}
		public MineCellView View
		{
			get { return mView; }
			set { mView = value; }
		}

		private void MineCell_Paint(object sender, PaintEventArgs e)
		{
			switch (View)
			{
				case MineCellView.Button:
					ButtonColor = Color.Gray;
					DrawCell(e);
					break;
				case MineCellView.Mine:
					ButtonColor = Color.LightGray;
					DrawCell(e);
					DrawMine(e, new ClassicMine()); //отрисовка мины
					break;
				case MineCellView.Number:
					ButtonColor = Color.LightGray;
					DrawCell(e);
					
					Color[] colors = new Color[] { Color.Blue, Color.Green, Color.Red, Color.Navy, Color.DarkGreen, Color.DarkBlue,
					Color.Brown, Color.Black};
					if (mNumber>0 && mNumber<9)
					{
						var NBrush = new SolidBrush(colors[mNumber - 1]);
						var myFont = new Font("Times", 1.5f, FontStyle.Bold, GraphicsUnit.World);
						SizeF SS = e.Graphics.MeasureString(mNumber.ToString(), myFont);
						e.Graphics.DrawString(mNumber.ToString(), myFont, NBrush, -SS.Width / 2, -SS.Height / 2);

					}
					//else
					//{
					//	if (mNumber == 0)
					//	{
					//		e.Graphics.Clear(Color.LightGray);
					//	}
					//}
					

					break;
				case MineCellView.Flag:
					ButtonColor = Color.LightGray;
					DrawCell(e);
					DrawMine(e, new Flag()); //отрисовка флага
					
					break;
				default:
					break;
			}
		
		}

		private void DrawCell(PaintEventArgs e)
		{
			//Прорисовка ячейки
			e.Graphics.ResetTransform();

			e.Graphics.TranslateTransform(this.Width / 2, this.Height / 2);
			e.Graphics.ScaleTransform(this.Width / 2, this.Height / 2);
			e.Graphics.Clear(ButtonColor);

			var TopLeft = new PointF(-1, -1);
			var TopRight = new PointF(1, -1);
			var BottomLeft = new PointF(-1, 1);
			var BottomRight = new PointF(1, 1);
			var myPen = new Pen(Color.LightGray, 0.2f);
			e.Graphics.DrawLines(myPen, new PointF[] { TopLeft, TopRight, BottomRight, BottomLeft, TopLeft });
					
		}

		private void DrawMine(PaintEventArgs e, IFigure figure)
		{
			//Прорисовка фигуры
			figure.Draw(e);
		}

		
		private void MineCell_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button ==System.Windows.Forms.MouseButtons.Right)
			{
				if (!field.mineField[i, j].IsOpen)
				{
					switch (this.View)
					{
						case MineCellView.Button:
							this.View = MineCellView.Flag;
							break;
						case MineCellView.Mine:
							break;
						case MineCellView.Number:
							break;
						case MineCellView.Flag:
							this.View = MineCellView.Button;
							break;
						default:
							break;
					}
					
					this.Refresh();
				}
			}
			else
			{
				field.OpenCell(i, j);
			}
		}


	}
}
