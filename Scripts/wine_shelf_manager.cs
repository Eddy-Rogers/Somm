using System;
using Godot;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public partial class wine_shelf_manager : Node
{
	private Node2D[] _shelfslots;
	private bool[] _occupied;
	private const int _maxshelves = 3;
	private const int _maxslots = 5;
	
	public PackedScene _pinot;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Note: Both of these have to be maintained. I might regret this
		_shelfslots = new Node2D[(_maxshelves * _maxslots)];
		_occupied = new bool[_maxshelves * _maxslots];
		
		_pinot = GD.Load<PackedScene>("res://Assets/Nodes/wine_bottle.tscn");
		
		//Debug();
	}

	public void debug()
	{
		for(int i = 0; i < _maxshelves * _maxslots; i++)
			_StockWine(1);
	}

	public void _FreeSlot(int slot)
	{
		_occupied[slot] = false;
	}

	public bool _StockWine(int WineID)
	{
		int openindex = _FindOpenShelf();
		if (openindex == -1)
		{
			return false;
		}
		
		// Create Wine Node
		Node2D scene = _pinot.Instantiate<Node2D>();
		
		AddChild(scene);
		
		GD.Print($"Index: {openindex}");
		scene.Position = new Vector2(calcx(openindex), calcy(openindex));
		scene.Call("set_wm_index", new Variant[] { openindex });
		scene.Call("set_wine_id", WineID);

		string WineType;
		if (!global_script._wine_list.TryGetValue(WineID, out WineType))
			throw new Exception("Wuh Oh, Wine ID not found");

		scene.GetNode<Sprite2D>("Sprite2D").Texture =
			GD.Load<Texture2D>("res://Assets/Sprites/PNGs/Wine/" + Regex.Replace(WineType, @"\s+", "") + ".png");
		
		// Add Wine Node to List
		_shelfslots[openindex] = scene;
		
		// Update Boolean
		_occupied[openindex] = true;

		return true;
	}

	public int calcx(int index)
	{
		return (int)Math.Floor((float)index % (float)_maxslots) * 256;
	}

	public int calcy(int index)
	{
		return (int)Math.Floor((float)index % (float)_maxshelves) * 256;
	}

	public int _FindOpenShelf()
	{
		int index = 0;
		bool found = false;
		while (!found && index < _maxshelves * _maxslots)
		{
			if (!_occupied[index])
				return index;
			index++;
		}
		return -1;
	}
	
}
