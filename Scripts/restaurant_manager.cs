using Godot;
using System;

public partial class restaurant_manager : Node2D
{
	[Export] public Timer SeatingTimer;
	[Export] public int NumberOfTables;
	
	private bool[] _in_service;
	private Node2D[] _tables;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_in_service = new bool[NumberOfTables];
		_tables = new Node2D[NumberOfTables];

		// Probably bad? Tried to use export logic but no cigar
		for (int TableIndex = 1; TableIndex <= NumberOfTables; TableIndex++)
		{
			_tables[TableIndex - 1] = GetNode<Node2D>("Table" + TableIndex.ToString());
			_tables[TableIndex - 1].Call("SetTableID", TableIndex - 1);
		}
		
		SeatingTimer.Start();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

	public void _on_seating_timer_timeout()
	{
		// Do some things
		GD.Print("Populating Table");
		int toSeat = FindUnoccupiedTable();
		if (toSeat != -1)
		{
			_tables[toSeat].Call("populate_table");
			_in_service[toSeat] = true;
			SeatingTimer.Start();
		}
	}

	public int FindUnoccupiedTable()
	{
		int index = 0;
		bool found = false;
		while (!found && index < _tables.Length)
		{
			if (_in_service[index]) index++;
			else found = true;
		}
		return found ? index : -1;
	}

	public void ClearTable(int tableID)
	{
		_in_service[tableID] = false;
	}
}
