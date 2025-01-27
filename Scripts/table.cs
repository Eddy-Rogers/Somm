using Godot;
using System;
using System.IO;

public partial class table : Node2D
{
	public int _numOccupants;
	public string _mealName;
	public bool _is_hovered = false;
	public bool _occupied = false;
	public int _tableID;
	[Export] Timer OccupiedTimer = new Timer();

	public void SetTableID(int tableID) => _tableID = tableID;
	public int GetTableID() => _tableID;
	public bool IsOccupied() => _occupied;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Modulate = new Color(Color.Color8(237, 135, 33));
	}

	public void populate_table()
	{
		// This would break if files got moved around
		Random rand = new();
		int MealID = rand.Next(1, 4);
		string meal;
		if(!global_script._meal_list.TryGetValue(MealID, out meal))
			throw new Exception("Meal not found");
		_numOccupants = rand.Next(1, 6);
		_mealName = meal;
		_occupied = true;
		OccupiedTimer.Start();
		GD.Print(_mealName);
	}

	public void ClearTable()
	{
		_numOccupants = -1;
		_occupied = false;
		_mealName = "";
		OccupiedTimer.Stop();
		shrink();
		GetNode("/root/Game/Restaurant").Call("ClearTable", new Variant[] { _tableID });
	}
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (_is_hovered)
		{
			if (Input.IsActionJustPressed("ui_touch") && !global_script._is_dragging)
			{
				if (_occupied)
				{
					// Signals are almost certainly better here but honestly couldn't be bothered to learn
					GetNode("/root/Game/ContextMenu").Call("_UpdateContext", new Variant[] { _mealName, _numOccupants });
				}
				else
				{
					GetNode("/root/Game/ContextMenu").Call("Clear");
				}
			}
		}
	}

	public void _on_occupied_timer_timeout()
	{
		GetNode("/root/Game/GameManager").Call("deduct_satisfaction", new Variant[] { 10 });
		ClearTable();
	}

	public void _on_area_2d_mouse_entered()
	{
		_is_hovered = true;
	}
	
	public void _on_area_2d_mouse_exited()
	{
		_is_hovered = false;
	}

	public void grow()
	{
		this.Scale = new Vector2(1.1f, 1.1f);
	}

	public void shrink()
	{
		this.Scale = new Vector2(1f, 1f);
	}

	public string ParseFinalDirectory(string meal_name)
	{
		int index = meal_name.Length - 1;
		while (meal_name[index] != '/' && index >= 0)
		{
			index--;
		}
		return meal_name.Substring(index + 1, meal_name.Length - index - 1);
	}
}
