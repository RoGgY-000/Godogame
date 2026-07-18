using Godot;

[GlobalClass]
public partial class EnemySpawnData : Resource
{
	[Export]
	public PackedScene EnemyTypeScene;

	[Export]
	public int Count;

	[Export]
	public float IntervalBetweenEnemies = 1f;

	public EnemySpawnData () { }
}
