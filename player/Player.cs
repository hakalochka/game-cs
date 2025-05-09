using Godot;
using System;

namespace Player;

public partial class Player : CharacterBody3D
{
    [Export]
    public CollisionShape3D shape;
    [Export]
    public Timer timer;

    [Export]
    public int Speed = 10;

    [Export]
    public int FallAcceleration = 50;
    public const float JumpVelocity = 20.0f;

    [Export]
    public int maxHealth = 3;
    private int _currentHealth;
    
    private Vector3 _targetVelocity = Vector3.Zero;


    public override void _Ready()
    {
        _currentHealth = maxHealth;
        shape = GetNode<CollisionShape3D>("shape");
        timer = GetNode<Timer>("Timer");
        GD.Print(_currentHealth); 
    }

    public override void _PhysicsProcess (double delta) {
        var direction = Vector3.Zero;
        
        

        if (Input.IsActionPressed("right")){
            direction.X += 1.0f;
        
        }
            
        if (Input.IsActionPressed("left")){
            direction.X -= 1.0f;
        }
            
        if (Input.IsActionPressed("back")){
            direction.Z += 1.0f;
        }
            
        if (Input.IsActionPressed("foward")){
            direction.Z -= 1.0f;
        }

        if (Input.IsActionJustPressed("jump") && IsOnFloor()){
            GD.Print(_targetVelocity);

            _targetVelocity.Y = JumpVelocity;
            GD.Print(_targetVelocity);

        }

        if (!IsOnFloor()){
            _targetVelocity.Y -= FallAcceleration * (float)delta;
        }
            
        if (direction != Vector3.Zero){
            direction = direction.Normalized();
        }

        

        _targetVelocity.X = direction.X * Speed;
        _targetVelocity.Z = direction.Z * Speed;

        

        Velocity = _targetVelocity;
        
        MoveAndSlide();
    }

    public void health_changed(int dmg){
        GD.Print(_currentHealth); 

        _currentHealth -= dmg;
        GD.Print(_currentHealth); 
        if (_currentHealth <= 0){
            dead();
        }
    }

    public void dead(){
        shape.CallDeferred("set_disabled", true);
        timer.Start();
    }

    private void _on_timer_timeout(){
        GetTree().ReloadCurrentScene();
    }
}
