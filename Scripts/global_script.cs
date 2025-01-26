using Godot;
using System;

public partial class global_script : Node2D
{
	public static bool _is_dragging = false;
	public static int _current_drag_id = -1;
	public static int _id_counter = 1;
	public static decimal _money = 0;
	public static float _satisfaction = 0.0f;
	
	public static void toggle_dragging()
	{
		_is_dragging = !_is_dragging;
	}

	public static int assign_bottle_id()
	{
		return _current_drag_id++;
	}
	
	
}
