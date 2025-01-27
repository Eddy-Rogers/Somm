using Godot;
using System;

public partial class game_manager : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public Label _money_label;
	public Label _satisfaction_label;
	
	public int _money = 200;
	public int _satisfaction = 50;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_money_label = GetNode<Label>("Money");
		_satisfaction_label = GetNode<Label>("Satisfaction");
		_UpdateUI(_money, _satisfaction);
	}

	public void _UpdateUI(int money, int satisfaction)
	{
		_money_label.Text = $"Money: ${money}";
		_satisfaction_label.Text = $"Renown: {satisfaction}%";
	}
	
	public void deduct_money(int amount)
	{
		_money -= amount;
		_UpdateUI(_money, _satisfaction);
	}
	
	public void add_money(int amount)
	{
		_money += amount;
		_UpdateUI(_money, _satisfaction);
	}

	public void deduct_satisfaction(int amount)
	{
		_satisfaction -= amount;
		_satisfaction = _satisfaction < 0 ? 0 : _satisfaction;
		_UpdateUI(_money, _satisfaction);
	}
	
	public void add_satisfaction(int amount)
	{
		_satisfaction += amount;
		_satisfaction = _satisfaction < 0 ? 0 : _satisfaction;
		_UpdateUI(_money, _satisfaction);
	}
}
