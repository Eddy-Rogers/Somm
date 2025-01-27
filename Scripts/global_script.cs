using Godot;
using System;
using Godot.Collections;

public partial class global_script : Node2D
{
	public static bool _is_dragging = false;
	public static int _current_drag_id = -1;
	public static int _id_counter = 1;
	
	public static readonly Dictionary<int, String> _wine_list = 
		new Dictionary<int, string>()
		{
			{ 1 , "Champagne" },
			{ 2 , "Riesling" },
			{ 3 , "Cabernet Sauvignon" },
			{ 4 , "Pinot Noir" },
		};
	
	public static readonly Dictionary<int, String> _meal_list = 
		new Dictionary<int, string>()
		{
			{ 1 , "Spaghetti Bolognese" },
			{ 2 , "Fettucine Alfredo" },
			{ 3 , "Filet Mignon" },
			{ 4 , "Grilled Salmon" },
		};

	
	public static void toggle_dragging()
	{
		_is_dragging = !_is_dragging;
	}

	public static int assign_bottle_id()
	{
		return _current_drag_id++;
	}
	
}
