using System;
using Godot;

public partial class BaseEnemy : MeshInstance2D
{
	PathFollow2D pathFollow;
	Label hp;
	[Export]
	public int Health { get; set; }
	[Export]
	public float Speed { get; set; }

	public override void _Ready()
	{
		pathFollow = GetParent<PathFollow2D>();
		hp = GetNode<Label>("HitBox/Label");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		pathFollow.Progress += (float) delta * Speed;
		UpdateHPBar();
		CorrectRotation();
		GD.Print(GlobalRotationDegrees);
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
	private void CorrectRotation()
	{
		if ( GlobalRotationDegrees > 90 )
		{
			GlobalRotationDegrees -= 180;
		}
		else if ( GlobalRotationDegrees < -90 )
		{
			GlobalRotationDegrees += 180;
		}
	}
	private void UpdateHPBar ()
	{
		if ( hp != null )
		{
			hp.Text = Health.ToString();
		}
	}

	public void Kill ()
	{
		QueueFree();
	}
}
