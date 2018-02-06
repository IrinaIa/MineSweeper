using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper
{
	//Ячейка, открыта ли она, есть мина, какое число в ней содержится (какое количество мин окружает ее)
	public struct Cell
	{
		public bool HasMine;
		public bool IsOpen;
		public int Number { get; set; }
	}
}
