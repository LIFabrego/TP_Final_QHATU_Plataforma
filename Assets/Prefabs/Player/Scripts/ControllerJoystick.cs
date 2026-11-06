using Assets.Player.Scripts.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;


public class ControllerJoystick: MonoBehaviour
{
    /*[SerializeField] private Joystick joystick;
    [SerializeField] private Rigidbody2D rb;
    private Animator _animator;

    private List<ICommand> commands = new List<ICommand>();

    void Start()
    {
        _animator = GetComponentInChildren<Animator>();

    }
    private void Update()
    {
        commands.Clear();

        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;

        //horizontalInput = Input.GetAxisRaw("Horizontal");
        //verticalInput = Input.GetAxisRaw("Vertical");

        // Crear y añadir comandos
        commands.Add(new HorizontalCommand(horizontalInput, rb));
        commands.Add(new VerticalCommand(verticalInput, rb));

        foreach (var command in commands)
        {
            command.execute();
        }

        _animator.SetFloat("Horizontal", horizontalInput);
        _animator.SetFloat("Vertical", verticalInput);
        if (horizontalInput != 0 || verticalInput != 0) // Si hay movimiento
        {
            _animator.SetBool("IsRunning", true); // Activa la animación de correr
        }
        else
        {
            _animator.SetBool("IsRunning", false); // Desactiva la animación de correr
        }
    }*/
}

