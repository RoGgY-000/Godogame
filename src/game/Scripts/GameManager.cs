using Godot;
public partial class GameManager : Node2D
{
	public static GameManager Instance { get; private set; }

	[Export]
	public int PlayerHealth { get; set; }

	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}

	public override void _EnterTree ()
	{
		if ( Instance == null )
		{
			Instance = this;
		}
		else
		{
			Free();
		}
	}
}
