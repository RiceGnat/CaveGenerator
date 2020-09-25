using System;

namespace CaveGenerator.Noise
{
	public class UniformNoise : Noise
	{
		public UniformNoise(int seed, int length, int max) : base(Generate(seed, length, max), length, max) { }

		public static int[] Generate(int seed, int length, int max)
		{
			int[] output = new int[length];
			Random random = new Random(seed);

			for (int i = 0; i < length; i++)
			{
				output[i] = random.Next(max + 1);
			}

			return output;
		}
	}
}
