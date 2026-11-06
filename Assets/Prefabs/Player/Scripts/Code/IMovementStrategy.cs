using Assets.Player.Scripts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Player.Scripts.Code
{
    public interface IMovementStrategy
    {
        public void Move(Rigidbody2D rb, Jugador jugador, float direccion);
    }
}
