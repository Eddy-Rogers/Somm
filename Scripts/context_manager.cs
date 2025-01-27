using Godot;
using System;
using System.IO;
using System.Text.RegularExpressions;
using FileAccess = Godot.FileAccess;

public partial class context_manager : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Clear()
	{
		GD.Print("Clearing");
		GetNode<Label>("MealName").Text = "";
		GetNode<Label>("MealDescription").Text = "";
		GetNode<Label>("TableDescription").Text = "";
	}

	// Give me the name of the meal and I'll handle the rest
	public void _UpdateContext(string meal_name, int occupants)
	{
		meal_name = Regex.Replace(meal_name, @"\s+", "_");
		var file = FileAccess.Open("res://Assets/Meals/" + meal_name + "/" + meal_name + ".txt", FileAccess.ModeFlags.Read);
		string desc = file.GetAsText();
		
		// Set Meal Name
		GetNode<Label>("MealName").Text = "Meal: " + meal_name;
		GetNode<Label>("MealDescription").Text = "Meal Description: " + desc;
		GetNode<Label>("TableDescription").Text = "Number of Occupants: " + occupants.ToString();
	}
}
