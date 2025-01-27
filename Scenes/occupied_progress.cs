using Godot;
using System;

public partial class occupied_progress : ProgressBar
{
	// Called when the node enters the scene tree for the first time.
	[Export] public Timer OccupiedTimer;
	private double max_time;
	public override void _Ready()
	{
		max_time = OccupiedTimer.WaitTime;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		this.SetValue(OccupiedTimer.TimeLeft / max_time * 100);
	}
}
