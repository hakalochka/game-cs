using Godot;
using System;
using Player;

public partial class Trap : Area3D
{
    [Export]
    private int dmg = 1;
    


    private void _on_body_entered(Godot.Node3D body)
	{
        

		if (body is Player.Player player)
        {
            player.health_changed(dmg);
        }
	}

    
}
