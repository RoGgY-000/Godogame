using Godot;
using System;

public partial class BaseBullet : StaticBody2D
{
	[Export]
	public Vector2 Velocity { get; set; }
	[Export]
	public float Speed { get; set; }
	[Export]
	public int Damage { get; set; }
	[Export]
	public float Range { get; set; }
	private float sqrRange { get; set; }

	public BaseEnemy Target { get; set; }
	public override void _Ready()
	{
		sqrRange = Range * Range;
		if ( IsInstanceValid(Target) )
		{
			LookAt(Target.GlobalPosition);
			Velocity = (Target.GlobalPosition - GlobalPosition).Normalized();
		}
		else
		{
			QueueFree();
		}
	}

	public override void _PhysicsProcess (double delta)
	{
		float sqrDistance = Position.LengthSquared();
		if ( IsInstanceValid(Target) 
			&& sqrDistance < sqrRange)
		{
			MoveAndCollide(Velocity * Speed * (float) delta);
		}
		else
		{
			QueueFree();
		}
		
	}

	public override void _Process(double delta)
	{
		
	}
}
