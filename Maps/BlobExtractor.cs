using System.Collections.Generic;

namespace CaveGenerator.Maps
{
	public class BlobExtractor
	{
		private readonly List<Pixel> queue = new List<Pixel>();
		private readonly Dictionary<(int, int), int> labeled = new Dictionary<(int, int), int>();
		private readonly List<Blob> blobs = new List<Blob>();
		private readonly int label = 0;
		private readonly bool[,] map;

		public BlobExtractor(bool[,] map)
		{
			this.map = map;

			for (int i = 0; i < map.GetLength(0); i++)
			{
				for (int j = 0; j < map.GetLength(1); j++)
				{
					if (TryPushQueue(i, j))
					{
						List<Pixel> blob = new List<Pixel>();

						while (queue.Count > 0)
						{
							Pixel p = PopQueue();

							TryPushQueue(p.X + 1, p.Y);
							TryPushQueue(p.X, p.Y + 1);
							TryPushQueue(p.X - 1, p.Y);
							TryPushQueue(p.X, p.Y - 1);

							blob.Add(p);
						}

						blobs.Add(new Blob(label, blob.Count, blob.AsReadOnly()));
						label++;
					}
				}
			}

			Blobs = blobs.AsReadOnly();
			blobs.Sort((a, b) => b.Area.CompareTo(a.Area));
		}

		public IEnumerable<Blob> Blobs { get; }

		private bool TryPushQueue(int x, int y)
		{
			bool valid = map.CheckBounds(x, y) && !map[x, y] && !labeled.ContainsKey((x, y));

			if (valid)
			{
				queue.Add(new Pixel(x, y, label));
				labeled[(x, y)] = label;
			}

			return valid;
		}

		private Pixel PopQueue()
		{
			Pixel p = queue[0];
			queue.RemoveAt(0);
			return p;
		}
	}
}
