using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineSweeper
{
	//Передать во view координаты ячейки, на которую нажали и ее номер
	public class ChangeArgs: EventArgs
	{
		public int X { get; set; }
		public int Y { get; set; }

		public int Number { get; set; }
	}
}
