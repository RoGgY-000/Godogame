using System;
using Godot;
using System.Collections.Generic;

public partial class BaseTower : MeshInstance2D
{
	[Export]
	public PackedScene BulletScene;

	[Export]
	public Node2D BulletSpawnPoint;

	[Export(PropertyHint.Range, "0.01, 100")]
	public float ReloadTime = 1f;

	[Export(PropertyHint.Range, "0, 1000000")]
	public int Damage = 1;

	[Export(PropertyHint.Range, "0, 10000")]
	public float Speed = 1000f;

	private List<BaseEnemy> _targetEnemies;
	private Area2D _targetTrigger;
	private ProgressBar _reloadBar;
	private double _timer;
	private float _attackRange;

	public override void _Ready ()
	{
		_targetTrigger = GetNode<Area2D>("TargetTrigger");
		if ( _targetTrigger != null )
		{
			_targetTrigger.AreaEntered += OnAreaEntered;
			_targetTrigger.AreaExited += OnAreaExited;
			CollisionShape2D shape = _targetTrigger.GetNode<CollisionShape2D>("AttackArea");
			if ( shape != null )
			{
				Vector2 size = shape.Shape.GetRect().Size;
				_attackRange = Mathf.Max(size.X/2, size.Y/2);
			}
		}

		_reloadBar = GetNode<ProgressBar>("ReloadBar");
		if ( _reloadBar != null )
		{
			_reloadBar.MaxValue = ReloadTime;
		}
		_targetEnemies = new List<BaseEnemy>();

		Node node = BulletScene.Instantiate();
		if ( node is not BaseBullet bullet )
		{
			throw new Exception("Wrong scene for Bullet");
		}
		node.QueueFree();
	}

	public override void _Process (double delta)
	{
		_timer += delta;
		CheckTargets();
		UpdateReloadBar();
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

	private void OnAreaEntered (Area2D area)
	{
		if ( area.GetParentOrNull<BaseEnemy>() is BaseEnemy enemy )
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

	private void SpawnBullet (BaseEnemy enemy)
	{
		BaseBullet bullet = BulletScene.Instantiate<BaseBullet>();
		bullet.Target = enemy;
		bullet.Position = BulletSpawnPoint.Position;
		bullet.Range = _attackRange;
		bullet.Damage = Damage;
		bullet.Speed = Speed;
		AddChild(bullet);
	}

	private void UpdateReloadBar ()
	{
		if ( _reloadBar != null )
		{
			_reloadBar.Value = _timer;
		}
	}
}
