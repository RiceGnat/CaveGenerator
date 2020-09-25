namespace CaveGenerator.CellularAutomata
{
	public class CellRegion
	{
		private readonly bool[,] source;
		private readonly int x;
		private readonly int y;
		private readonly bool edge;

		public CellRegion(bool[,] source, int x, int y, bool edge)
		{
			this.source = source;
			this.x = x;
			this.y = y;
			this.edge = edge;
		}

		public bool Self => GetValue(x, y);
		public bool Top => GetValue(x, y - 1);
		public bool TopRight => GetValue(x + 1, y - 1);
		public bool Right => GetValue(x + 1, y);
		public bool BottomRight => GetValue(x + 1, y + 1);
		public bool Bottom => GetValue(x, y + 1);
		public bool BottomLeft => GetValue(x - 1, y + 1);
		public bool Left => GetValue(x - 1, y);
		public bool TopLeft => GetValue(x - 1, y - 1);

		public int GetNeighbors()
		{
			int count = 0;
			for (int i = x - 1; i <= x + 1; i++)
			{
				for (int j = y - 1; j <= y + 1; j++)
				{
					if (GetValue(i, j)) count++;
				}
			}
			return count;
		}

		private bool GetValue(int x, int y)
		{
			if (!source.CheckBounds(x, y))
			{
				return edge;
			}
			else return source[x, y];
		}
	}
}
