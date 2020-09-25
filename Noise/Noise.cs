using System;

namespace CaveGenerator.Noise
{
	public abstract class Noise : INoise
	{
		private readonly int[] values;

		protected Noise(int[] values, int length, int max)
		{
			this.values = new int[length];
			Array.Copy(values, this.values, length);
			Length = length;
			Max = max;
		}

		public int Length { get; }
		public int Max { get; }

		public int this[int index] => values[index];
	}
}
