using Godot;

public partial class BaseBullet : StaticBody2D
{
	[Export]
	public Vector2 Velocity;

	[Export]
	public float Speed;

	[Export]
	public float Range;

	[Export]
	public int Damage;

	public BaseEnemy Target;

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
}
