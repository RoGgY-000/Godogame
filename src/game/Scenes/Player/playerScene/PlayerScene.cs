using Godot;
using System;
using System.ComponentModel;

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
		// Input.MouseMode = Input.MouseModeEnum.Confined;
	}

	public override void _Process (double delta)
	{
		Move();
		Zoom();
		MouseFocus();
	}

	public override void _Input (InputEvent e)
	{

	}

	private void Move ()
	{
		GlobalPosition +=
			new Vector2(
				Input.GetAxis("A", "D"),
				Input.GetAxis("W", "S"))
			* CameraSpeed;
	}

	private void Zoom ()
	{
		float zoomScale = Input.GetAxis("MouseWheelDown", "MouseWheelUp");
		if ( zoomScale != 0 )
		{
			GD.Print(zoomScale);
			_camera.Zoom *= zoomScale * 2;
		}
	}

	private void MouseFocus ()
	{
		if ( Input.IsActionJustPressed("PAUSE (ESC)") )
		{
			_inPauseMenu = !_inPauseMenu;
			_pauseLabel.Visible = _inPauseMenu;
		}

		if ( _inPauseMenu )
		{
			Input.MouseMode = Input.MouseModeEnum.Visible;
		}
		else if ( !_inPauseMenu )
		{
			Input.MouseMode = Input.MouseModeEnum.Confined;
		}
	}
}
