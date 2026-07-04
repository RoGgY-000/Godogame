using Godot;
using System;
using System.Collections.Generic;

public partial class BaseTower : MeshInstance2D
{
	[Export]
	public float ReloadTime { get; set; }
	[Export]
	public int Damage { get; set; }

	private Area2D TargetTrigger;
	private List<BaseEnemy> TargetEnemies;
	private double timer;

	public override void _Ready()
	{
		TargetTrigger = GetNode<Area2D>("TargetTrigger");
		TargetTrigger.AreaEntered += AddTarget;
		TargetTrigger.AreaExited += RemoveTarget;
		TargetEnemies = new List<BaseEnemy>();
	}

	public override void _Process(double delta)
	{
		timer += delta;
		CheckTargets();
	}

	private void AddTarget (Area2D area)
	{
		BaseEnemy enemy = area.GetParent<BaseEnemy>();
		TargetEnemies.Add(enemy);
		
	}
	private void RemoveTarget (Area2D area)
	{
		BaseEnemy enemy = area.GetParent<BaseEnemy>();
		TargetEnemies.Remove(enemy);
	}
	private void CheckTargets ()
	{
		if ( timer >= ReloadTime
			&& TargetEnemies.Count > 0 )
		{
			Fire(TargetEnemies[0]);
		}
	}
	// fix this sheet
	private void Fire (BaseEnemy enemy)
	{
		Path2D bulletPath = new Path2D();
		PackedScene p = (PackedScene) GD.Load("res://Scenes/Bullets/baseBullet/baseBullet.tscn");
		MeshInstance2D n =(MeshInstance2D) p.Instantiate();
		AddChild(n);
		n.GlobalPosition = new Vector2(5, 5);
		bulletPath.Curve = new Curve2D();
		bulletPath.Curve.AddPoint(GlobalPosition);
		bulletPath.Curve.AddPoint(enemy.GlobalPosition);
		PathFollow2D bulletFollow = new PathFollow2D();
		bulletPath.AddChild(bulletFollow);
		BaseBullet bullet = new BaseBullet();
		bulletFollow.AddChild(bullet);
		enemy.Health -= Damage;
		timer = 0;
	}
}
