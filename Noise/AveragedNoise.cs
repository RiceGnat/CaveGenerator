namespace CaveGenerator.Noise
{
	public class AveragedNoise : Noise
	{
		public AveragedNoise(int seed, int length, int max, int passes) : base(Generate(seed, length, max, passes), length, max) { }

		public static int[] Generate(int seed, int length, int max, int passes)
		{
			int[] source = UniformNoise.Generate(seed, length + passes, max);
			int[] output = source;

			for (int n = passes - 1; n >= 0; n--)
			{
				output = new int[length + n];
				for (int i = 0; i < length; i++)
				{
					output[i] = (source[i] + source[i + 1]) / 2;
				}
				source = output;
			}

			return output;
		}
	}
}
