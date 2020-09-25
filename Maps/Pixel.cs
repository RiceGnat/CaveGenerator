namespace CaveGenerator.Maps
{
	public struct Pixel
	{
		public Pixel(int x, int y, int label)
		{
			X = x;
			Y = y;
			Label = label;
		}

		public int X { get; }
		public int Y { get; }
		public int Label { get; }
	}
}
