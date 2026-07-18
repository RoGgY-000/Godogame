using Godot;

public partial class PlayerScene : Node2D
{
	[Export]
	public float CameraSpeed = 5f;

	private Camera2D _camera;
	private Label _pauseLabel;
	private bool _inPauseMenu = false;

	public override void _Ready ()
	{
		_camera = GetNode<Camera2D>("Camera2D");
		_pauseLabel = GetNode<Label>("Camera2D/ESC Label");
	}

	public override void _Process (double delta)
	{
		Move();
	}

	public override void _Input (InputEvent e)
	{
		if ( e is InputEventKey keyEvent )
		{
			ProcessKeyboardInput(keyEvent);
		}
		else if ( e is InputEventMouseButton mouseButtonEvent )
		{
			ProcessMouseInput(mouseButtonEvent);
		}
	}

	private void Move ()
	{
		GlobalPosition +=
			new Vector2(
				Input.GetAxis("A", "D"),
				Input.GetAxis("W", "S"))
			* CameraSpeed;
	}

	private void MouseFocus ()
	{
		if ( _inPauseMenu )
		{
			Input.MouseMode = Input.MouseModeEnum.Visible;
		}
		else if ( !_inPauseMenu )
		{
			Input.MouseMode = Input.MouseModeEnum.Confined;
		}
	}

	private void ProcessKeyboardInput (InputEventKey keyEvent)
	{
		switch ( keyEvent.Keycode )
		{
			case Key.Escape when keyEvent.IsPressed():
				_inPauseMenu = !_inPauseMenu;
				_pauseLabel.Visible = _inPauseMenu;
				MouseFocus();
				break;
			default:
				break;
		}
	}

	private void ProcessMouseInput (InputEventMouseButton mouseButtonEvent)
	{
		switch ( mouseButtonEvent.ButtonIndex )
		{
			case MouseButton.WheelUp:
				_camera.Zoom += Vector2.One;
				break;
			case MouseButton.WheelDown:
				_camera.Zoom *= 0.5f;
				break;
			default:
				break;
		}
	}
}
