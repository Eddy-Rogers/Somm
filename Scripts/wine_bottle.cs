using Godot;
using System;

public partial class wine_bottle : Node2D
{
	public bool _draggable = false;
	
	public bool _is_inside_droppable = false;

	public Vector2 offset;
	
	public Node2D droppable_node;

	public Vector2 initialPosition;

	public int _bottleID;

	public int _wm_index;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_bottleID = global_script.assign_bottle_id();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (_draggable)
		{
			if (Input.IsActionJustPressed("ui_touch"))
			{
				global_script._is_dragging = true;
				initialPosition = GlobalPosition;
				offset = GetGlobalMousePosition() - GlobalPosition;
			}
			if (Input.IsActionPressed("ui_touch") && global_script._current_drag_id == _bottleID)
			{
				GlobalPosition = GetGlobalMousePosition() - offset;
				GetTree().GetRoot().SetInputAsHandled();
			}
			else if (Input.IsActionJustReleased("ui_touch"))
			{
				global_script._is_dragging = false;
				global_script._current_drag_id = -1;
				if (_is_inside_droppable)
				{
					// Do some things
					GetNode("/root/Game/WineShelf").Call("_FreeSlot", new Variant[] { _wm_index });
					QueueFree();
				}
				else
				{
					
					var tween = GetTree().CreateTween();
					tween.TweenProperty(this, "global_position", initialPosition, 0.2).SetEase(Tween.EaseType.Out);
				}
			}
		}
	}

	public void set_wm_index(int wm_index)
	{
		GD.Print("Received" + wm_index);
		_wm_index = wm_index;
	}

	public void _on_area_2d_mouse_entered()
	{
		if (!global_script._is_dragging)
		{
			Scale = new Vector2(1.1f, 1.1f);
			_draggable = true;
			global_script._current_drag_id = _bottleID;
		}
	}

	public void _on_area_2d_input_event(Viewport viewport, InputEvent inputEvent)
	{
		if (inputEvent.IsActionPressed("ui_touch"))
		{
			
		}
	}
	
	public void _on_area_2d_mouse_exited()
	{
		if (!global_script._is_dragging)
		{
			Scale = new Vector2(1f, 1f);
			_draggable = false;
		}
	}

	public void _on_area_2d_area_entered(Node2D other)
	{
		if(other.IsInGroup("Droppable") && global_script._is_dragging)
		{
			_is_inside_droppable = true;
			other.GetParent().Call("grow");
		}
	}
	
	public void _on_area_2d_area_exited(Node2D other)
	{
		if(other.IsInGroup("Droppable") && global_script._is_dragging)
		{
			_is_inside_droppable = false;
			other.GetParent().Call("shrink");
		}
	}

	public void _setID(int id)
	{
		_bottleID = id;
	}
	
	public int getID() => _bottleID;
}
