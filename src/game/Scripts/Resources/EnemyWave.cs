using Godot;

[GlobalClass]
public partial class EnemyWave : Resource
{
	[Export]
	public float WaveDuration = 10f;

	[Export(PropertyHint.NodePathValidTypes, "Path2D")]
	public NodePath Path2DScenePath;

	[Export(PropertyHint.ResourceType, "EnemySpawnData")]
	public Godot.Collections.Array<EnemySpawnData> EnemySpawnDatas;

	public EnemyWave () { }
}
