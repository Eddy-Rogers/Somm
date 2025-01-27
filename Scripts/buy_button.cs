using Godot;
using System;

public partial class buy_button : Node2D
{
	public bool _is_hovered = false;
	// Called when the node enters the scene tree for the first time.
	public Node _manager;
	private Label _type_label;

	[Export] public int WineID;
	
	public override void _Ready()
	{
		_manager = GetNode("/root/Game/GameManager");
		_type_label = GetNode<Label>("Control/Label");
		string WineType;
		if (!global_script._wine_list.TryGetValue(WineID, out WineType))
			throw new Exception("Wuh Oh, Wine ID not found");
		_type_label.Text = "Buy: " + WineType;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (_is_hovered)
		{
			if (Input.IsActionJustPressed("ui_touch") && !global_script._is_dragging)
			{
				GetNode("/root/Game/WineShelf").Call("_StockWine", WineID);
				_manager.Call("deduct_money", new Variant[] { 30 });
			}
		}
	}

	public void _on_area_2d_mouse_entered()
	{
		_is_hovered = true;
	}
	
	public void _on_area_2d_mouse_exited()
	{
		_is_hovered = false;
	}
}
