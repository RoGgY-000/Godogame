using Godot;

public partial class EnemySpawner : Node2D
{
	[Export(PropertyHint.ResourceType, "EnemyWave")]
	private Godot.Collections.Array<EnemyWave> _waves;

	public override void _Ready ()
	{
	}

	public override void _Process (double delta)
	{
	}
}

