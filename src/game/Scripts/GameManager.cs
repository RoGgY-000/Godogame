using Godot;

public partial class GameManager : Node2D
{
	public static GameManager Instance { get; private set; }

	public bool IsInPause = false;

	public override void _EnterTree ()
	{
		if ( Instance == null )
		{
			Instance = this;
		}
		else
		{
			QueueFree();
		}
	}
}
