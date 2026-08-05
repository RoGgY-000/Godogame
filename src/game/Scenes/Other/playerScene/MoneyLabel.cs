using Godot;
using System;

public partial class MoneyLabel : Label
{
	[Export]
	private string _text = "Money: {0}";
	public override void _Process(double delta)
	{
		Text = string.Format(_text, GameManager.Instance.Money);
	}
}
