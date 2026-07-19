using System;
using Godot;

public partial class PlayerCamera : Camera2D
{
	[Export]
	private float _minZoom = 0.5f;

	[Export]
	private float _maxZoom = 4.0f;

	[Export]
	private float _zoomSpeed = 0.5f;

	[Export]
	private float _moveSpeed = 100f;

	private Vector2 _targetZoom;
	private Vector2 _targetPosition;

	public override void _Ready ()
	{
		_targetZoom = Zoom;
		_targetPosition = GlobalPosition;
	}

	public override void _Process (double delta)
	{
		//Move(delta);
		Zoom = Zoom.Lerp(_targetZoom, (float) delta);
		GlobalPosition = GlobalPosition.Lerp(_targetPosition, (float) delta);
	}

	public override void _Input (InputEvent e)
	{
		if ( e is InputEventMouseButton mouseButtonEvent )
		{
			switch ( mouseButtonEvent.ButtonIndex )
			{
				case MouseButton.Left:
					break;
				case MouseButton.Right:
					break;
				case MouseButton.Middle:
					break;
				case MouseButton.WheelUp:
					UpdateZoom(_zoomSpeed);
					break;
				case MouseButton.WheelDown:
					UpdateZoom(-_zoomSpeed);
					break;
				default:
					break;
			}
		}
		else if ( e is InputEventKey keyEvent 
			&& keyEvent.IsPressed())
		{
			Vector2 input = Input.GetVector("A", "D", "W", "S");
			_targetPosition += input * _moveSpeed / Zoom.X;
		}
	}
	//private void Move (double delta)
	//{
	//	GlobalPosition += new Vector2(
	//		Input.GetAxis("A", "D"),
	//		Input.GetAxis("W", "S"))
	//		* _moveSpeed
	//		* (float) delta;
	//}

	private void UpdateZoom (float value)
	{
		Vector2 oldZoom = _targetZoom;
		float targetX = Mathf.Clamp(_targetZoom.X + value, _minZoom, _maxZoom);
		_targetZoom = new Vector2(targetX, targetX);
		
		Vector2 mousePos = GetGlobalMousePosition();
		Vector2 zoomFactor = _targetZoom / oldZoom;
		_targetPosition = mousePos - (mousePos + _targetPosition) * zoomFactor;
	}
}
