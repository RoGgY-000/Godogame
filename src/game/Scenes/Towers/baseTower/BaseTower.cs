using Godot;
using System.Collections.Generic;

public partial class BaseTower : MeshInstance2D
{
	[Export]
	public PackedScene BulletScene;

	[Export]
	public Node2D BulletSpawnPoint;

	[Export]
	public float ReloadTime;

	[Export]
	public int Damage;

	[Export]
	public float AttackRange;

	private Area2D _targetTrigger;
	private ProgressBar _reloadBar;
	private List<BaseEnemy> _targetEnemies;
	private double _timer;

	public override void _Ready ()
	{
		_targetTrigger = GetNode<Area2D>("TargetTrigger");
		if ( _targetTrigger != null )
		{
		_targetTrigger.AreaEntered += OnAreaEntered;
			_targetTrigger.AreaExited += OnAreaExited;
		}

		_reloadBar = GetNode<ProgressBar>("ReloadBar");
		if ( _reloadBar != null )
		{
			_reloadBar.MaxValue = ReloadTime;
		}
		_targetEnemies = new List<BaseEnemy>();
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
		bullet.Range *= AttackRange;
		bullet.Damage *= Damage;
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
