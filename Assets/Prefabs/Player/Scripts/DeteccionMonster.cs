using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class DeteccionMonster : MonoBehaviour
{
    //[SerializeField] private PokemonManager2 pokemonManager; // Referencia al PokemonManager
    [SerializeField] private LayerMask grassLayer;          // Layer de la hierba
    [SerializeField] private float encounterChance = 0.3f;  // Probabilidad de encuentro

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verificar si el jugador entra en contacto con la hierba
        if (((1 << collision.gameObject.layer) & grassLayer) != 0)
        {
            // Chequear probabilidad de encuentro
            if (UnityEngine.Random.value < encounterChance)
            {
                DetectPokemonInGrass(collision.transform);
            }
        }
    }

    private void DetectPokemonInGrass(Transform grassTransform)
    {
        // Buscar Pokémon en la proximidad de la hierba
        Collider2D[] nearbyPokemons = Physics2D.OverlapCircleAll(grassTransform.position, 1f); // Radio ajustable

        foreach (Collider2D collider in nearbyPokemons)
        {
            // Verificar si el objeto es un Pokémon y está oculto
            var pokemon = collider.GetComponent<SpriteRenderer>();
            if (pokemon != null && !pokemon.enabled)
            {
                // Revelar al Pokémon encontrado
                pokemon.enabled = true;
                Debug.Log($"¡Has detectado un Pokémon: {collider.name}!");
                return; // Salir después de detectar uno
            }
        }

        Debug.Log("No hay Pokémon ocultos en esta área.");
    }
}
