using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Player.Scripts.Models
{
    public class Jugador
    {
        private Vector2 _posicion;
        private Vector2 _velocidad;

        public Jugador(Vector2 velocidad)
        {
            Velocidad = velocidad;
        }

        public Vector2 Velocidad { get => _velocidad; set => _velocidad = value; }
    }
}
