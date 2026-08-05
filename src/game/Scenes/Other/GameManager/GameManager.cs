using Godot;

public partial class GameManager : Node
{
	[Export(PropertyHint.Range, "0, 1000, or_greater")]
	public int Money = 1;

	public static GameManager Instance { get; private set; }

	public bool IsInPause = false;

	public override void _Ready ()
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
