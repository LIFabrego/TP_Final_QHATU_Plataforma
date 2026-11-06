using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class AcelerateMovement : IMovementPlayer
{

    public void Move(Transform transform,Player player,Vector2 direction)
    {
        Vector3 move = new Vector3(direction.x, direction.y, 0) * (player.Velocity + player.Acceleration) * Time.deltaTime;
        transform.Translate(move);
    }
}
