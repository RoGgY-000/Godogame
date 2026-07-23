using Godot;

public partial class BaseBullet : StaticBody2D
{
	public BaseEnemy Target;

	public float Speed = 1;
	public float Range = 1;
	public float Damage = 1;

	private Vector2 _velocity;
	private float _sqrRange;

	public override void _Ready ()
	{
		_sqrRange = Range * Range;
		if ( IsInstanceValid(Target) )
		{
			LookAt(Target.GlobalPosition);
			_velocity = (Target.GlobalPosition - GlobalPosition).Normalized();
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
			MoveAndCollide(_velocity * Speed * (float) delta);
		}
		else
		{
			QueueFree();
		}
	}
}
