using System;
using Godot;
using System.Collections.Generic;

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
		
		_pinot = GD.Load<PackedScene>("res://Assets/Nodes/pinot.tscn");
		
		debug();
		
	}

	public void debug()
	{
		for(int i = 0; i < _maxshelves * _maxslots; i++)
			_StockWine();
	}

	public bool _StockWine()
	{
		int openindex = _FindOpenShelf();
		if (openindex == -1)
		{
			return false;
		}
		
		// Create Wine Node
		Node2D scene = _pinot.Instantiate<Node2D>();
		
		AddChild(scene);
		
		scene.Position = new Vector2(calcx(openindex), calcy(openindex));
		
		GD.Print($"Adding wine to x: {calcx(openindex)}, y: {calcy(openindex)}");
		
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
