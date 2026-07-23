using Godot;
using System;
using System.ComponentModel;

public partial class PlayerScene : Node2D
{
	[Export]
	public float cameraSpeed = 5f;
	public bool inPauseMenu = false;
	public Label healthLabel;

	public override void _Ready()
	{
		healthLabel = GetNode<Label>("Camera2D/Health Label");
		// Input.MouseMode = Input.MouseModeEnum.Confined;
	}

	
	public override void _Process(double delta)
	{
		Move();
		// MouseFocus();
	}


    private void Move()
	{
		GlobalPosition += 
			new Vector2(
				Input.GetAxis("A", "D"),
				Input.GetAxis("W", "S")) * cameraSpeed;
	}

	private void MouseFocus()
	{
		if(Input.IsActionJustPressed("PAUSE (ESC)"))
		{
			inPauseMenu = !inPauseMenu;
			GD.Print("1");
		}

		if(inPauseMenu) {Input.MouseMode = Input.MouseModeEnum.Visible;}
		else if(!inPauseMenu) {Input.MouseMode = Input.MouseModeEnum.Confined;}
	}
}
