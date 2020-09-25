namespace CaveGenerator.CellularAutomata
{
	public static class RuleSets
	{
		public static RuleSet Basic => new RuleSet(0.45, true, (region, generation) => region.GetNeighbors() > 4);
	}
}
