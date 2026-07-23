using Godot;

public partial class PlayerCamera : Camera2D
{
	[Export(PropertyHint.Range, "0.01, 1")]
	private float _minZoom = 0.25f;

	[Export(PropertyHint.Range, "1, 100")]
	private float _maxZoom = 2.0f;

	[Export(PropertyHint.Range, "0.01, 100")]
	private float _zoomSpeed = 0.5f;

	[Export(PropertyHint.Range, "0, 1000000")]
	private float _moveSpeed = 1000f;

	private Vector2 _targetZoom;
	private Vector2 _targetPosition;

	private float _zoomStep = 0.25f;

	public override void _Ready ()
	{
		_targetZoom = Zoom;
		_targetPosition = GlobalPosition;
	}

	public override void _Process (double delta)
	{	
		if(_targetPosition != GlobalPosition && _targetZoom != Zoom)
		{
			GlobalPosition = GlobalPosition.Lerp(_targetPosition, 1f); // (float)delta);
			Zoom = Zoom.Lerp(_targetZoom, 1f); // (float)delta);
		}
		
		Move(delta);
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
					UpdateZoom(_zoomStep);
					break;
				case MouseButton.WheelDown:
					UpdateZoom(-_zoomStep);
					break;
				default:
					break;
			}
		}
	}


	private void UpdateZoom(float value)
	{	
		_targetPosition += GetLocalMousePosition() * Mathf.Sign(value);
		_targetZoom += Vector2.One*value;
		_targetZoom = _targetZoom.Clamp(new Vector2(_minZoom, _minZoom), new Vector2(_maxZoom, _maxZoom));
	}


	private void Move (double delta)
	{
		GlobalPosition += new Vector2(
			Input.GetAxis("A", "D"),
			Input.GetAxis("W", "S"))
			* _moveSpeed
			* (float) delta;
	}
}
