using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtraparAlpaca : MonoBehaviour
{
    private GameObject currentPlayerModel; // El modelo actual del jugador
    [SerializeField] private GameObject rojo;
    [SerializeField] private GameObject celeste;
    [SerializeField] private GameObject verde;
    [SerializeField] private GameObject negro;
    [SerializeField] private GameObject gris;

    void Start()
    {
         currentPlayerModel = GetComponentInChildren<GameObject>();
    }
    //colision con alpaca
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Rojo") || other.CompareTag("Celeste") || 
            other.CompareTag("Verde") || other.CompareTag("Negro") || 
            other.CompareTag("Gris"))
        {
            string alpacaTag = other.gameObject.tag;//Tag Alpaca
            string playerTag = currentPlayerModel.tag;// Tag Player
            if (alpacaTag == playerTag)
            {
                Debug.Log("Alpaca atrapada!");
                if (playerTag == "Rojo")
                {
                    GameObject.Instantiate(rojo, transform.position, transform.rotation);
                }
                if (playerTag == "Celeste")
                {
                    GameObject.Instantiate(celeste, transform.position, transform.rotation);
                }
                if (playerTag == "Verde")
                {
                    GameObject.Instantiate(verde, transform.position, transform.rotation);
                }
                if (playerTag == "Negro")
                {
                    GameObject.Instantiate(negro, transform.position, transform.rotation);
                }
                if (playerTag == "Gris")
                {
                    GameObject.Instantiate(gris, transform.position, transform.rotation);
                }
                Destroy(other.gameObject); 
            }
            else
            {
                Debug.Log("Color no coincide, no puedes atrapar esta alpaca.");
            }
        }
    }
}

