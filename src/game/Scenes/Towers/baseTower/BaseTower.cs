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

	private Area2D TargetTrigger;
	private List<BaseEnemy> TargetEnemies;
	private double timer;

	public override void _Ready()
	{
		TargetTrigger = GetNode<Area2D>("TargetTrigger");
		TargetTrigger.AreaEntered += OnAreaEntered;
		TargetTrigger.AreaExited += OnAreaExited;
		TargetEnemies = new List<BaseEnemy>();
	}

	public override void _Process(double delta)
	{
		timer += delta;
		CheckTargets();
	}

	private void OnAreaEntered (Area2D area)
	{
		if ( area.GetParent<BaseEnemy>() is BaseEnemy enemy )
		{
			TargetEnemies.Add(enemy);
		}
		
	}
	private void OnAreaExited (Area2D area)
	{
		if ( area.GetParent<BaseEnemy>() is BaseEnemy enemy )
		{
			TargetEnemies.Remove(enemy);
		}
	}
	private void CheckTargets ()
	{
		if ( timer >= ReloadTime
			&& TargetEnemies.Count > 0 )
		{
			Fire(TargetEnemies[0]);
		}
	}
	private void Fire (BaseEnemy enemy)
	{
		SpawnBullet(enemy);
		timer = 0d;
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
