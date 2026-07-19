using System;
using System.Threading.Tasks;
using Godot;
using Godot.Collections;

public partial class EnemySpawner : Node2D
{
	[Export(PropertyHint.ResourceType, "EnemyWave")]
	private Array<EnemyWave> _waves;

	private int _waveCount;
	private int _currentWaveIndex;
	private EnemyWave _currentWave;

	public override void _Ready ()
	{
		if ( _waves == null
			|| _waves.Count == 0 )
		{
			throw new NullReferenceException("No waves found!");
		}

		_waveCount = _waves.Count;
		for ( int i = 0; i < _waveCount; i++ )
		{
			Array<EnemySpawnData> arr = _waves[i].EnemySpawnDatas;
			for ( int j = 0; j < arr.Count; j++ )
			{
				if ( arr[j] != null
					&& arr[j].EnemyTypeScene != null )
				{
					Node node = arr[j].EnemyTypeScene.Instantiate();
					if ( node is not BaseEnemy )
					{
						throw new Exception("Enemy type is not correct!");
					}
					node.QueueFree();
				}
			}
		}
	}

	public override void _Process (double delta)
	{
		if ( _currentWave == null 
			&& _currentWaveIndex < _waveCount)
		{
			SpawnWave(_waves[_currentWaveIndex++]);
		}
	}

	private async void SpawnWave (EnemyWave wave)
	{
		_currentWave = wave;
		Task[] spawn = new Task[wave.EnemySpawnDatas.Count];
		for ( int i = 0; i < spawn.Length; i++ )
		{
			EnemySpawnData data = wave.EnemySpawnDatas[i];
			spawn[i] = SpawnLine(data.IntervalBetweenEnemies, data.Count, data.EnemyTypeScene);
		}
		await Task.WhenAll(spawn);
		_currentWave = null;
	}

	private async Task SpawnLine (float period, int count, PackedScene enemyScene)
	{
		for ( int i = 0; i < count; i++ )
		{
			await Task.Delay(TimeSpan.FromSeconds(period));
			Node enemy = enemyScene.Instantiate();
			PathFollow2D pathFollow = new PathFollow2D();
			pathFollow.AddChild(enemy);
			GetNode(_currentWave.Path2DScenePath).AddChild(pathFollow);
		}
	}
}