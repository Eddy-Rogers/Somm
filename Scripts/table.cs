using Godot;
using System;
using System.IO;

public partial class table : Node2D
{
	public int _numOccupants;
	public string _mealName;
	public bool _is_hovered = false;
	
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Modulate = new Color(Color.Color8(237, 135, 33));
		populate_table();
	}

	public void populate_table()
	{
		// This would break if files got moved around
		
		string[] meals = Directory.GetDirectories(ProjectSettings.GlobalizePath("res://Assets/Meals/"));
		Random rand = new();
		int index = rand.Next(0, meals.Length);
		_numOccupants = rand.Next(1, 6);
		_mealName = ParseFinalDirectory(meals[index]);
		GD.Print("Meal Name: " + _mealName);
	}
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (_is_hovered)
		{
			if (Input.IsActionJustPressed("ui_touch") && !global_script._is_dragging)
			{
				// Signals are almost certainly better here but honestly couldn't be bothered to learn
				GetNode("/root/Game/ContextMenu").Call("_UpdateContext", new Variant[] { _mealName, _numOccupants });
			}
		}
	}

	public void _on_area_2d_mouse_entered()
	{
		GD.Print("AREA ENTERED");
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
