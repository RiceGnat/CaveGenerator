using System;

namespace CaveGenerator.CellularAutomata
{
	public class CellularAutomaton
	{
		private readonly Random random;
		private readonly RuleSet ruleSet;

		public CellularAutomaton(int seed, RuleSet ruleSet)
		{
			random = new Random(seed);
			this.ruleSet = ruleSet ?? throw new ArgumentNullException(nameof(ruleSet));
		}

		public bool[,] Generate(int x, int y, int generations)
		{
			bool[,] source = new bool[x, y];
			bool[,] output = source;

			for (int n = 0; n <= generations; n++)
			{
				output = new bool[x, y];

				for (int i = 0; i < x; i++)
				{
					for (int j = 0; j < y; j++)
					{
						output[i, j] = n == 0 ?
							random.NextDouble() < ruleSet.InitialRate :
							ruleSet.EvaluateRule(new CellRegion(source, i, j, ruleSet.CountEdges), n);
					}
				}

				source = output;
			}

			return output;
		}
	}
}
