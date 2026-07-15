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

	public BaseEnemy Target { get; set; }

	private float _sqrRange;

	public override void _Ready ()
	{
		_sqrRange = Range * Range;
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
			&& sqrDistance < _sqrRange )
		{
			MoveAndCollide(Velocity * Speed * (float) delta);
		}
		else
		{
			QueueFree();
		}
	}

	public override void _Process (double delta)
	{

	}
}
