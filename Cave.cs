using System;
using System.Linq;
using CaveGenerator.CellularAutomata;
using CaveGenerator.Maps;
using CaveGenerator.Noise;

namespace CaveGenerator
{
	public class Cave : IHeightMap
	{
		private readonly IHeightMap map;

		public Cave(int seed, int x, int y, double wallDensity, double rockDensity)
		{
			Random random = new Random(seed);

			bool[,] bounds = GenerateBounds(random.Next(), x, y, 5, -1);
			bool[,] walls = GenerateWalls(random.Next(), x, y, wallDensity);
			bool[,] map = ValueArray.Create(x, y, (i, j) => bounds[i, j] || walls[i, j]);
			MainArea = FillHoles(map);
			IHeightMap rocks = GenerateRocks(random.Next(), x, y, rockDensity);

			SizeX = x;
			SizeY = y;
			MaxHeight = rocks.MaxHeight + 1;

			this.map = new HeightMap(MaxHeight, ValueArray.Create(x, y, (i, j) => map[i, j] ? MaxHeight : rocks[i, j]));
		}

		public int SizeX { get; }
		public int SizeY { get; }
		public int MaxHeight { get; }
		public Blob MainArea { get; }

		public int this[int x, int y] => map[x, y];

		private static bool[,] GenerateBounds(int seed, int x, int y, int width, double power)
		{
			Random random = new Random(seed);
			bool[,] output = new bool[x, y];

			INoise top = new SineNoise(random.Next(), x, width, f => Math.Pow(f, power));
			INoise right = new SineNoise(random.Next(), y, width, f => Math.Pow(f, power));
			INoise bottom = new SineNoise(random.Next(), x, width, f => Math.Pow(f, power));
			INoise left = new SineNoise(random.Next(), y, width, f => Math.Pow(f, power));

			for (int i = 0; i < x; i++)
			{
				for (int j = 0; j <= top[i]; j++)
				{
					output[i, Math.Min(j, y)] |= true;
				}

				for (int j = 0; j <= bottom[i]; j++)
				{
					output[i, Math.Max(y - j - 1, 0)] |= true;
				}
			}

			for (int i = 0; i < y; i++)
			{
				for (int j = 0; j <= left[i]; j++)
				{
					output[Math.Min(j, x), i] |= true;
				}

				for (int j = 0; j <= right[i]; j++)
				{
					output[Math.Max(x - j - 1, 0), i] |= true;
				}
			}

			return output;
		}

		private static bool[,] GenerateWalls(int seed, int x, int y, double density)
		{
			CellularAutomaton ca = new CellularAutomaton(seed, new RuleSet(Lerp(0.35, 0.5, density), true, (region, generation) => region.GetNeighbors() > 4));

			return ca.Generate(x, y, 11);
		}

		private static IHeightMap GenerateRocks(int seed, int x, int y, double density)
		{
			CellularAutomaton ca = new CellularAutomaton(seed, new RuleSet(Lerp(0.1, 0.3, density), false, (region, generation) => region.GetNeighbors() > 2));

			IHeightMap layer1 = new BooleanMap(ca.Generate(x, y, 4));
			IHeightMap layer2 = new BooleanMap(ca.Generate(x, y, 2));

			return new HeightMap(2, ValueArray.Create(x, y, (i, j) => layer1[i, j] + layer2[i, j]));
		}

		private static Blob FillHoles(bool[,] map)
		{
			BlobExtractor extractor = new BlobExtractor(map);

			foreach (Blob b in extractor.Blobs.Skip(1))
			{
				foreach (Pixel p in b.Pixels)
				{
					map[p.X, p.Y] = true;
				}
			}

			return extractor.Blobs.First();
		}

		private static double Lerp(double a, double b, double t)
		{
			if (t < 0 || t > 1) throw new ArgumentException($"{nameof(t)} is out of bounds");

			return a * (1 - t) + b * t;
		}
	}
}
