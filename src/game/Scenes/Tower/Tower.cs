using Godot;
using System;
using System.Collections.Generic;

public partial class Tower : MeshInstance2D
{
	[Export]
	public float ReloadTime { get; set; }
	[Export]
	public int Damage { get; set; }

	private Area2D TargetTrigger;
	private List<Enemy> TargetEnemies;
	private double timer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		TargetTrigger = GetNode<Area2D>("TargetTrigger");
		TargetTrigger.AreaEntered += AddTarget;
		TargetTrigger.AreaExited += RemoveTarget;
		TargetEnemies = new List<Enemy>();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		timer += delta;
		CheckTargets();
	}

	private void AddTarget (Area2D area)
	{
		Enemy enemy = area.GetParent<Enemy>();
		TargetEnemies.Add(enemy);
		
	}
	private void RemoveTarget (Area2D area)
	{
		Enemy enemy = area.GetParent<Enemy>();
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
	private void Fire (Enemy enemy)
	{
		enemy.Health -= Damage;
		timer = 0;
	}
}
