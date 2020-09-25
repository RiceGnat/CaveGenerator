namespace CaveGenerator.Maps
{
	public class HeightMap : IHeightMap
	{
		private readonly int[,] values;

		public HeightMap(int max, int[,] values)
		{
			this.values = values.Clone() as int[,];
			MaxHeight = max;
		}

		public int SizeX => values.GetLength(0);
		public int SizeY => values.GetLength(1);
		public int MaxHeight { get; }

		public int this[int x, int y] => values[x, y];
	}
}
