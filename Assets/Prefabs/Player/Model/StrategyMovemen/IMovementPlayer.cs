using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovementPlayer
{
    public void Move(Transform transform, Player player,Vector2 direction);
}
