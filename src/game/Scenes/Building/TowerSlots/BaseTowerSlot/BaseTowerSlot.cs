using Godot;
using System;

public partial class BaseTowerSlot : Node2D
{
	[Export]
	private PackedScene _towerScene;
	private MeshInstance2D _mesh;
	private Area2D _hitBox;
	private bool _isMouseHover = false;
	private bool _towerSpawned = false;
	public override void _Ready()
	{
		_mesh = GetNode<MeshInstance2D>("Mesh");

		_hitBox = GetNode<Area2D>("Mesh/HitBox");
		ArgumentNullException.ThrowIfNull(_hitBox);
		_hitBox.MouseEntered += () => _isMouseHover = true;
		_hitBox.MouseExited += () => _isMouseHover = false;

		ArgumentNullException.ThrowIfNull(_towerScene);
		if ( _towerScene.Instantiate<BaseTower>() is BaseTower tower )
		{
			tower.QueueFree();
		}
	}

	public override void _Process(double delta)
	{
	}

	public override void _Input (InputEvent e)
	{
		if ( e is InputEventMouseButton mouseButtonEvent
			&& _isMouseHover )
		{
			switch ( mouseButtonEvent.ButtonIndex )
			{
				case MouseButton.Left:
					SpawnTower();
					break;
				default:
					break;
			}
		}
	}

	private void SpawnTower ()
	{
		if ( !_towerSpawned )
		{
			BaseTower tower = _towerScene.Instantiate<BaseTower>();
			_towerSpawned = true;
			if ( GameManager.Instance.Money >= tower.Cost )
			{
				AddChild(tower);
				GameManager.Instance.Money -= tower.Cost;
				_mesh.QueueFree();
			}
		}
	}
}
