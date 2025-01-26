using Godot;
using System;
using System.IO;

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

	// Give me the name of the meal and I'll handle the rest
	public void _UpdateContext(string meal_name, int occupants)
	{
		string[] meals = Directory.GetDirectories(ProjectSettings.GlobalizePath("res://Assets/Meals/"));
		
		GD.Print(ProjectSettings.GlobalizePath("res://Assets/Meals/") + meal_name + "/" + meal_name + ".txt");
		
		string description = File.ReadAllText( ProjectSettings.GlobalizePath("res://Assets/Meals/") + meal_name + "/" + meal_name + ".txt");
		
		// Set Meal Name
		GetNode<Label>("MealName").Text = "Meal: " + meal_name;
		GetNode<Label>("MealDescription").Text = "Meal Description: " + description;
		GetNode<Label>("TableDescription").Text = "Number of Occupants: " + occupants.ToString();
	}
}
