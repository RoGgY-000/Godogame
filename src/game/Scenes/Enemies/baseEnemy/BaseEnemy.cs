using Godot;

public partial class BaseEnemy : MeshInstance2D
{
	PathFollow2D pathFollow;
	[Export]
	public int Health { get; set; }
	[Export]
	public float Speed { get; set; }

	public override void _Ready()
	{
		pathFollow = GetParent<PathFollow2D>();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		pathFollow.Progress += (float) delta * Speed;
		if ( Health <= 0 )
		{
			Kill();
		}
		if ( pathFollow.ProgressRatio >= 0.99f )
		{
			GameManager.Instance.PlayerHealth -= Health;
			Kill();
		}
	}

	public void Kill ()
	{
		QueueFree();
	}
}
