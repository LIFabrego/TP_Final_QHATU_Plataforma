using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SmoothMovement : IMovementPlayer
{
    public void Move(Transform transform, Player player,Vector2 direction)
    {
        float moveInX = Input.GetAxis("Horizontal") * player.Velocity * Time.deltaTime;
        float moveInY = Input.GetAxis("Vertical") * player.Velocity* Time.deltaTime;

        transform.Translate(new Vector2(moveInX, moveInY));

    }
}
