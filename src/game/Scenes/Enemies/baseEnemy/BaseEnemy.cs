using Godot;

public partial class BaseEnemy : MeshInstance2D
{
	[Export]
	public int Health { get; set; }

	[Export]
	public float Speed { get; set; }

	private Area2D _hitBox;
	private PathFollow2D _pathFollow;
	private Label _HPText;
	private ProgressBar _HPBar;

	public override void _Ready ()
	{
		_hitBox = GetNode<Area2D>("HitBox");
		_pathFollow = GetParent<PathFollow2D>();
		_HPText = GetNode<Label>("HitBox/HPText");
		_HPBar = GetNode<ProgressBar>("HitBox/HPBar");

		_HPBar.MaxValue = Health;
		_hitBox.BodyEntered += OnBodyEntered;
	}

	public override void _Process (double delta)
	{
		_pathFollow.Progress += (float) delta * Speed;
		UpdateHP();
		CorrectRotation();

	}
	private void CorrectRotation ()
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
		if ( _pathFollow.ProgressRatio >= 0.99f )
		{
			Kill();
		}
		if ( _HPText != null )
		{
			_HPText.Text = Health.ToString();
		}
		if ( _HPBar != null )
		{
			_HPBar.Value = Health;
		}
	}

	private void OnBodyEntered (Node2D body)
	{
		if ( body is BaseBullet bullet )
		{
			bullet.QueueFree();
			Health -= bullet.Damage;
		}
	}

	public void Kill ()
	{
		QueueFree();
	}
}
