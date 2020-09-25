using System;

namespace CaveGenerator.CellularAutomata
{
	public class RuleSet
	{
		private readonly Func<CellRegion, int, bool> rule;

		public RuleSet(double initialRate, bool countEdges, Func<CellRegion, int, bool> rule)
		{
			InitialRate = initialRate;
			CountEdges = countEdges;
			this.rule = rule ?? throw new ArgumentNullException(nameof(rule));
		}

		public double InitialRate { get; }
		public bool CountEdges { get; }
		public bool EvaluateRule(CellRegion region, int generation) => rule(region, generation);
	}
}
