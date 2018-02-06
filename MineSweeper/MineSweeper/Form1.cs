using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MineSweeper
{
	public enum LevelGame
	{
		Beginner,
		Intermidiate,
		Advanced
	}
	
	public partial class Form1 : Form
	{
		Field field;
		MineCell[,] MineFiels;
		int X, Y, CountMine;
		int CellSize = 20;

		public Form1()
		{
			InitializeComponent();
			InitializeField(LevelGame.Beginner);
			NewGame();
			
		}


		void NewGame()
		{
			this.Hide();
			myPanel.Controls.Clear();
			field = new Field(X, Y, CountMine);
			AddButtons();
			field.Change += ChangeButton;
			field.Lose += Loose;
			field.Win += Win;
			ChangeForm();
			this.Show();
		}

		private void ChangeForm()
		{
			this.Width = Y * CellSize;
			do
			{
				this.Width++;
			} while (myPanel.Width!=Y*CellSize);
			
			this.Height = X * CellSize;
			do
			{
				this.Height++;
			} while (myPanel.Height != X*CellSize);
		}

		private void Win(object sender, EventArgs e)
		{
			ShowMines();
			MessageBox.Show("Победа!!!");
			field.Win -= Win;
			
		}

		private void Loose(object sender, EventArgs e)
		{
			ShowMines();
			var loose = MessageBox.Show("проиграл");
			field.Change -= ChangeButton;
			field.Lose -= Loose;
			//throw new NotImplementedException();
		}

		private void ShowMines()
		{
			for (int row = 0; row < X; row++)
			{
				for (int col = 0; col < Y; col++)
				{
					if (field.mineField[row,col].HasMine)
					{
						MineFiels[row, col].View = MineCellView.Mine;
						MineFiels[row, col].Refresh();
					}
				}
			}
		
		}

		private void ChangeButton(object sender, ChangeArgs e)
		{
			if (field.mineField[e.X, e.Y].HasMine)
			{
				MineFiels[e.X, e.Y].View = MineCellView.Mine;
			}
			else
			{
				MineFiels[e.X, e.Y].Number = e.Number;
				MineFiels[e.X, e.Y].View = MineCellView.Number;

			}
		
			MineFiels[e.X, e.Y].Refresh();
		}

		private void AddButtons()
		{
			MineFiels = new MineCell[X, Y];
			for (int i = 0; i < X; i++)
			{
				for (int j = 0; j < Y; j++)
				{
					MineCell C = new MineCell(field, i, j);
					myPanel.Controls.Add(C);
					C.Left = CellSize * j;
					C.Top = CellSize * i;
					C.Width = CellSize;
					C.Height = CellSize;
					
					C.i = i;
					C.j = j;
					C.View = MineCellView.Button;
					MineFiels[i, j] = C;
					MineFiels[i, j].Refresh();
					
				}
			}
			
		}
		//Уровень игры
		private void InitializeField(LevelGame levelGame)
		{
			switch (levelGame)
			{
				case LevelGame.Beginner:
					X = 9;
					Y = 9;
					CountMine = 11;
					
					break;
				case LevelGame.Intermidiate:
					X = 16;
					Y = 16;
					CountMine = 40;
					
					break;
				case LevelGame.Advanced:
					X = 16;
					Y = 30;
					CountMine = 99;
					
					break;
				default:
					break;
			}
		}

	

		private void beginnerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			InitializeField(LevelGame.Beginner);
			NewGame();
		}

		private void newToolStripMenuItem_Click(object sender, EventArgs e)
		{
			NewGame();
		}

		private void intermidiateToolStripMenuItem_Click(object sender, EventArgs e)
		{
			InitializeField(LevelGame.Intermidiate);
			NewGame();
		}

		private void advancedToolStripMenuItem_Click(object sender, EventArgs e)
		{
			InitializeField(LevelGame.Advanced);
			NewGame();
		}




		
	}
}
