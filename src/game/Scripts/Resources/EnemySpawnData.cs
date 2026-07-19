using Godot;

[GlobalClass]
public partial class EnemySpawnData : Resource
{
	[Export]
	public PackedScene EnemyTypeScene;

	[Export(PropertyHint.Range, "0, 100, or_greater")]
	public int Count;

	[Export(PropertyHint.Range, "0, 60, or_greater")]
	public float IntervalBetweenEnemies = 1f;

	public EnemySpawnData () { }
}
