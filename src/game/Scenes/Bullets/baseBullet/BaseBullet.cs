using Godot;
using System;

public partial class BaseBullet : MeshInstance2D
{
	[Export]
	public float Speed { get; set; }
	[Export]
	public float Damage { get; set; }
	private PathFollow2D pathFollow;
	public override void _Ready()
	{
		pathFollow = GetParent<PathFollow2D>();
	}

	public override void _Process(double delta)
	{
		pathFollow.Progress += (float) delta * Speed;
	}
}
