using Godot;

public partial class PlayerBase : MeshInstance2D
{
	[Export(PropertyHint.Range, "1, 1000000")]
	public int Health = 1;

	private Area2D _hitBox;
	private Label _hpText;
	private ProgressBar _hpBar;

	public override void _Ready ()
	{
		_hitBox = GetNode<Area2D>("HitBox");
		if ( _hitBox != null )
		{
			_hitBox.AreaEntered += OnHitBoxEntered;
		}

		_hpText = GetNode<Label>("HPText");
		if ( _hpText != null )
		{
			_hpText.Text = Health.ToString();
		}

		_hpBar = GetNode<ProgressBar>("HPBar");
		if ( _hpBar != null )
		{
			_hpBar.MaxValue = Health;
			_hpBar.Value = Health;
		}
	}

	private void UpdateHP ()
	{
		if ( _hpText != null )
		{
			_hpText.Text = Health.ToString();
		}
		if ( _hpBar != null )
		{
			_hpBar.Value = Health;
		}
	}

	private void OnHitBoxEntered (Area2D area)
	{
		if ( area.GetParent<BaseEnemy>() is BaseEnemy enemy )
		{
			Health -= enemy.Health;
			enemy.Kill();
			UpdateHP();
		}
		if ( Health <= 0 )
		{
			QueueFree();
		}
	}
}
