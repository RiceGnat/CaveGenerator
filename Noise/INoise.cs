namespace CaveGenerator.Noise
{
	public interface INoise
	{
		int Length { get; }
		int Max { get; }

		int this[int index] { get; }
	}
}
