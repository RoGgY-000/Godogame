using Godot;
using System;
using System.ComponentModel;

public partial class PlayerScene : Node2D
{
	[Export]
	public float cameraSpeed = 5f;

	public override void _Ready()
	{
	}

	
	public override void _Process(double delta)
	{
		Move();
	}

    private void Move()
	{
		GlobalPosition += 
			new Vector2(
				Input.GetAxis("A", "D"),
				Input.GetAxis("W", "S")) * cameraSpeed;
	}
}
