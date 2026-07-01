using Godot;

public partial class BaseEnemy : MeshInstance2D
{
	[Export]
	public int Health { get; set; }
	[Export]
	public float Speed { get; set; }

	private PathFollow2D pathFollow;
	private Label HPText;
	private ProgressBar HPBar;

	public override void _Ready()
	{
		pathFollow = GetParent<PathFollow2D>();
		HPText = GetNode<Label>("HitBox/HPText");
		HPBar = GetNode<ProgressBar>("HitBox/HPBar");
		HPBar.MaxValue = Health;
	}

	public override void _Process(double delta)
	{
		pathFollow.Progress += (float) delta * Speed;
		UpdateHP();
		CorrectRotation();
		GD.Print(GlobalRotationDegrees);
		
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
	private void UpdateHP ()
	{
		if ( Health <= 0 )
		{
			Kill();
		}
		if ( pathFollow.ProgressRatio >= 0.99f )
		{
			GameManager.Instance.PlayerHealth -= Health;
			Kill();
		}
		if ( HPText != null )
		{
			HPText.Text = Health.ToString();
		}
		if ( HPBar != null )
		{
			HPBar.Value = Health;
		}
	}

	public void Kill ()
	{
		QueueFree();
	}
}
