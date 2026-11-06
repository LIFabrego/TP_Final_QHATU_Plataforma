using Assets.Player.Scripts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Player.Scripts.Code
{
    public class HorizontalCommand : ICommand
    {
        private readonly float _input;
        private Rigidbody2D _rb;
        private Jugador _jugador;
        private IMovementStrategy _movementStrategy;

        public HorizontalCommand(float input, Rigidbody2D rb)
        { 
            _input = input;
            _rb = rb;   
            _jugador = new Jugador(new Vector2(5, 0));
            _movementStrategy = new HorizontalMovementStrategy();
        }

        public void execute()
        {
            _movementStrategy.Move(_rb, _jugador, _input); 
        }
    }
}
