using Godot;
public partial class GameManager : Node2D
{
	public static GameManager Instance { get; private set; }

	[Export]
	public int PlayerHealth { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
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
