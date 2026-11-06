using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerChangePrefabs : MonoBehaviour
{
    [SerializeField] private GameObject[] skeletonPrefabs;
    [SerializeField] private GameObject rojo;
    [SerializeField] private GameObject celeste;
    [SerializeField] private GameObject verde;
    [SerializeField] private GameObject negro;
    [SerializeField] private GameObject gris;
    private GameObject currentPlayer;
    private void Start()
    {
        ChangePlayerPrefabs();
    }
    private void ChangePlayerPrefabs()
    {
        if (currentPlayer != null)
        {
            currentPlayer.SetActive(false);
        }
        int randomIndex = Random.Range(0, skeletonPrefabs.Length);
        currentPlayer = skeletonPrefabs[randomIndex];//elije el prefabs del array
        currentPlayer.SetActive(true);
    }
    public void DeactivateAndChangePlayer()
    {
        if (currentPlayer != null)
        {
            currentPlayer.SetActive(false);
        }

        ChangePlayerPrefabs();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Rojo") || other.CompareTag("Celeste") ||
            other.CompareTag("Naranja") || other.CompareTag("Negro") ||
            other.CompareTag("Gris"))
        {
            string alpacaTag = other.gameObject.tag;
            string playerTag = currentPlayer.tag;
            //Debug.Log(playerTag);
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
                if (playerTag == "Naranja")
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
                DeactivateAndChangePlayer();
            }
            else
            {
                Debug.Log("Color no coincide, no puedes atrapar esta alpaca.");
            }
        }
    }
}
