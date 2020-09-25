namespace CaveGenerator.Maps
{
	public interface IHeightMap
	{
		int SizeX { get; }
		int SizeY { get; }
		int MaxHeight { get; }

		int this[int x, int y] { get; }
	}
}
