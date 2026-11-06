using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Player
{
    private float velocity;
    private float acceleration;
    private float jumped;

    public Player(float velocity, float acceleration, float jumped)
    {
        this.velocity = velocity;
        this.acceleration = acceleration;
        this.jumped = jumped;   
    }
    public float Velocity { get => velocity; set => velocity = value; }
    public float Acceleration { get => acceleration; set => acceleration = value; }
    public float Jumped { get => jumped; set => jumped = value; }
}
