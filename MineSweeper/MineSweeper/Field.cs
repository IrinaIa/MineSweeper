using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper
{
	public class Field
	{
		public Cell[,] mineField;
		int fRowCount = 9;
		int fColumnCount = 9;
		int fMineCount = 11;

		public event EventHandler Win;
		public event EventHandler Lose;
		public event EventHandler<ChangeArgs> Change;

		//инициализаци поля
		public Field(int rowCount, int columnCount, int mineCount)
		{
			fRowCount = rowCount;
			fColumnCount = columnCount;
			fMineCount = mineCount;
			mineField = new Cell[fRowCount, fColumnCount];

			GenerateMines();
			CalculateNumbers();
		}
		//Генерация мин
		void GenerateMines()
		{
			Random RX = new Random();
			for (int i = 0; i < fMineCount; i++)
			{
				int X = 0;
				int Y = 0;
				do
				{

					X = RX.Next(0, fRowCount);
					Y = RX.Next(0, fColumnCount);

				} while (mineField[X, Y].HasMine);
				mineField[X, Y].HasMine = true;
			}
		}
		//Подсчет, сколько мин окружает ячейку, если она не мина
		void CalculateNumbers()
		{
			for (int row = 0; row < fRowCount; row++)
				for (int col = 0; col < fColumnCount; col++)
				{
					if (!mineField[row, col].HasMine)
						for (int r = row - 1; r <= row + 1; r++)
							for (int c = col - 1; c <= col + 1; c++)
								if (r >= 0 && r < fRowCount && c >= 0 && c < fColumnCount && !(row == r && c == col))
									if (mineField[r, c].HasMine)
									{
										mineField[row, col].Number++;
									}
				}

		}
		//Проверка открыты ли все ячейки
		public void OpenLast()   
		{
			foreach (Cell t in mineField)
			{
				if (!t.IsOpen && !t.HasMine) return;
			}
			if (Win != null)
			{
				Win(this, EventArgs.Empty);
			}
		}
		//Открытие ячейки
		public void OpenCell(int X, int Y)
		{
			if (mineField[X,Y].HasMine)
			{
				if (Lose !=null)
				{
					Lose(this, EventArgs.Empty);
				}
			}
			mineField[X, Y].IsOpen = true;
			if (Change != null)
			{
				Change(this, new ChangeArgs { X = X, Y = Y, Number = mineField[X,Y].Number });
			}
			if (mineField[X,Y].Number==0)
			{
				for (int row = X - 1; row <= X + 1; row++)
				for (int col = Y - 1; col <= Y + 1; col++)
					if (row >= 0 && row < fRowCount && col >= 0 && col < fColumnCount && !mineField[row, col].IsOpen)
						{
							OpenCell(row, col);
						}
			}
			OpenLast();
		}
	}
}
