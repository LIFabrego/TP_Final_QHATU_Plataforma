using UnityEngine;
using UnityEngine.UI;

public class FichaManager : MonoBehaviour
{
    public static FichaManager instance;

    public int fichasDisponibles = 4;
    public bool minijuegoCompletado = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Evita que se destruya al cambiar de escena
        }
        else
        {
            Destroy(gameObject); // Evita duplicados
        }
    }

    public bool UsarFicha()
    {
        if (fichasDisponibles > 0)
        {
            fichasDisponibles--;
            return true;
        }
        else
        {
            return false;
        }
    }

    public int ObtenerFichas()
    {
        return fichasDisponibles;
    }
}