using System;

namespace CaveGenerator
{
	public static class Extensions
	{
		public static bool CheckBounds(this bool[,] array, int x, int y) =>
			!(x < array.GetLowerBound(0) || x > array.GetUpperBound(0) ||
			y < array.GetLowerBound(1) || y > array.GetUpperBound(1));

		public static T[,] MergeWith<T>(this T[,] source, T[,] other, Func<T, T, T> func)
		{
			T[,] output = new T[source.GetLength(0), source.GetLength(1)];

			for (int i = 0; i < output.GetLength(0); i++)
			{
				for (int j = 0; j < output.GetLength(0); j++)
				{
					output[i, j] = func(source[i, j], other[i, j]);
				}
			}

			return output;
		}
	}
}
