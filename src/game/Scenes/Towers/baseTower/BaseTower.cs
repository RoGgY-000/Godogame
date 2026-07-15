using Godot;
using System;
using System.Collections.Generic;

public partial class BaseTower : MeshInstance2D
{
	[Export]
	public PackedScene BulletScene { get; set; }

	[Export]
	public Node2D BulletSpawnPoint { get; set; }

	[Export]
	public float ReloadTime { get; set; }

	[Export]
	public int Damage { get; set; }

	[Export]
	public float AttackRange { get; set; }

	private Area2D _targetTrigger;
	private List<BaseEnemy> _targetEnemies;
	private double _timer;

	public override void _Ready ()
	{
		_targetTrigger = GetNode<Area2D>("TargetTrigger");
		_targetTrigger.AreaEntered += OnAreaEntered;
		_targetTrigger.AreaExited += OnAreaExited;
		_targetEnemies = new List<BaseEnemy>();
	}

	public override void _Process (double delta)
	{
		_timer += delta;
		CheckTargets();
	}

	private void OnAreaEntered (Area2D area)
	{
		if ( area.GetParent<BaseEnemy>() is BaseEnemy enemy )
		{
			_targetEnemies.Add(enemy);
		}

	}
	private void OnAreaExited (Area2D area)
	{
		if ( area.GetParent<BaseEnemy>() is BaseEnemy enemy )
		{
			_targetEnemies.Remove(enemy);
		}
	}
	private void CheckTargets ()
	{
		if ( _timer >= ReloadTime
			&& _targetEnemies.Count > 0 )
		{
			Fire(_targetEnemies[0]);
		}
	}
	private void Fire (BaseEnemy enemy)
	{
		SpawnBullet(enemy);
		_timer = 0d;
	}

	private void SpawnBullet (BaseEnemy enemy)
	{
		BaseBullet bullet = BulletScene.Instantiate<BaseBullet>();
		bullet.Target = enemy;
		bullet.Position = BulletSpawnPoint.Position;
		bullet.Range *= AttackRange;
		bullet.Damage *= Damage;
		AddChild(bullet);
	}
}
