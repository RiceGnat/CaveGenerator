using System;

namespace CaveGenerator.Noise
{
	public class SineNoise : Noise
	{
		public SineNoise(int seed, int length, int max, Func<double, double> amplitude) : base(Generate(seed, length, max, amplitude), length, max) { }

		private static double Sin(double x, double frequency, double phase) => Math.Sin(2 * Math.PI * (frequency * x + phase));

		public static int[] Generate(int seed, int length, int max, Func<double, double> amplitude)
		{
			Random random = new Random(seed);
			double[] sum = new double[length];
			
			for (int f = 1; f <= length / 4; f++)
			{
				double a = amplitude(f);
				double phase = random.NextDouble();

				for (int i = 0; i < length; i++)
				{
					sum[i] += Sin((double)i / length, f, phase) * a;
				}
			}

			double minVal = 0;
			double maxVal = 0;
			for (int i = 0; i < length; i++)
			{
				if (sum[i] < minVal) minVal = sum[i];
				else if (sum[i] > maxVal) maxVal = sum[i];
			}

			double range = maxVal - minVal;

			int[] output = new int[length];
			for (int i = 0; i < length; i++)
			{
				output[i] = (int)Math.Round((sum[i] - minVal) / range * max);
			}

			return output;
		}

	}
}
