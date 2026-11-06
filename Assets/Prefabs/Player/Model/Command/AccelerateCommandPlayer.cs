using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class AccelerateCommandPlayer : ICommandPlayer
{
    private readonly PlayerMovement playerMovement;
    private readonly Vector2 input;

    public AccelerateCommandPlayer(PlayerMovement playerMovement, Vector2 input)
    {
        this.playerMovement = playerMovement;   
        this.input = input;
    }

    public void Execute()
    {
        playerMovement.SetMovementStrategy(new AcelerateMovement());
        playerMovement.MovePlayer(input);
    }
}
