using System;
using System.Collections.Generic;

namespace CaveGenerator.Maps
{
	public class Blob
	{
		public Blob(int label, int area, IEnumerable<Pixel> pixels)
		{
			Label = label;
			Area = area;
			Pixels = pixels ?? throw new ArgumentNullException(nameof(pixels));
		}

		public int Label { get; }
		public int Area { get; }
		public IEnumerable<Pixel> Pixels { get; }
	}
}
