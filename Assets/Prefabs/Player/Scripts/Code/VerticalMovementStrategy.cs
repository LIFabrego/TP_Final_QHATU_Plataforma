using Assets.Player.Scripts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Player.Scripts.Code
{
    public class VerticalMovementStrategy : IMovementStrategy
    {
        public void Move(Rigidbody2D rb, Jugador jugador, float direccion)
        {
            Vector2 velocidadFisica = rb.linearVelocity;
            velocidadFisica.y = direccion * jugador.Velocidad.y; 
            rb.linearVelocity = velocidadFisica;
        }
    }
}
