using System;

namespace CaveGenerator.Maps
{
	public static class ValueArray
	{
		public static T[,] Create<T>(int x, int y, Func<int, int, T> func) where T : struct
		{
			T[,] values = new T[x, y];

			for (int i = 0; i < values.GetLength(0); i++)
			{
				for (int j = 0; j < values.GetLength(0); j++)
				{
					values[i, j] = func(i, j);
				}
			}

			return values;
		}
	}
}
