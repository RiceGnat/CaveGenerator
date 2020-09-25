namespace CaveGenerator.Maps
{
	public class BooleanMap : IHeightMap
	{
		private readonly bool[,] values;

		public BooleanMap(bool[,] values)
		{
			this.values = values.Clone() as bool[,];
		}

		public int SizeX => values.GetLength(0);
		public int SizeY => values.GetLength(1);
		public int MaxHeight => 1;

		public bool this[int x, int y] => values[x, y];
		int IHeightMap.this[int x, int y] => values[x, y] ? 1 : 0;
	}
}
