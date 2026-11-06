using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogoManager2 : MonoBehaviour
{
    [System.Serializable]
    public class DialogueLine
    {
        public string speaker;
        [TextArea(2, 5)]
        public string text;
        public AudioClip voiceClip;
    }

    [SerializeField] private List<DialogueLine> dialogueLines;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI speakerNameText;
    [SerializeField] private Button nextButton;
    [SerializeField] private AudioSource AudioSource;

    [SerializeField] private Image imagenMama;
    [SerializeField] private Image imagenTomas;

    [Header("Sprites Humanos")]
    [SerializeField] private Sprite spriteMamaHablando;
    [SerializeField] private Sprite spriteMamaEscuchando;
    [SerializeField] private Sprite spriteTomasHablando;
    [SerializeField] private Sprite spriteTomasEscuchando;
    [SerializeField] private Sprite spriteAmbosHumanos;

    [Header("Sprites Gato")]
    [SerializeField] private Sprite spriteMamaGatoHablando;
    [SerializeField] private Sprite spriteMamaGatoEscuchando;
    [SerializeField] private Sprite spriteTomasGatoHablando;
    [SerializeField] private Sprite spriteTomasGatoEscuchando;
    [SerializeField] private Sprite spriteAmbosGatos;

    [SerializeField] private GameObject imagenAmbos; // Para el final

    private int currentLine = 0;
    private bool isTyping = false;

    void Start()
    {
        nextButton.onClick.AddListener(OnNextButtonClicked);
        nextButton.gameObject.SetActive(false);
        if (SeleccionMundoManager.Instance != null && SeleccionMundoManager.Instance.mundoSeleccionado == SeleccionMundoManager.Mundo.Gato)
        {
            spriteMamaHablando = spriteMamaGatoHablando;
            spriteMamaEscuchando = spriteMamaGatoEscuchando;
            spriteTomasHablando = spriteTomasGatoHablando;
            spriteTomasEscuchando = spriteTomasGatoEscuchando;
        }
        if (AudioSource == null)
        {
            Debug.LogWarning("Sin asignacion de audio");
        }
        ShowLine();
    }

    private void OnNextButtonClicked()
    {
        if (!isTyping)
        {
            NextLine();
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (!isTyping)
            {
                NextLine();
            }
        }
    }

    void ShowLine()
    {
        if (currentLine < dialogueLines.Count)
        {
            DialogueLine line = dialogueLines[currentLine];
            speakerNameText.text = line.speaker;
            StartCoroutine(TypeText(line.text));

            // Determinar quién habla
            string speaker = line.speaker.ToLower();

            if (speaker.Contains("mamá") || speaker.Contains("mama"))
            {
                imagenMama.sprite = spriteMamaHablando;
                imagenTomas.sprite = spriteTomasEscuchando;
            }
            else if (speaker.Contains("tomás") || speaker.Contains("tomas"))
            {
                imagenMama.sprite = spriteMamaEscuchando;
                imagenTomas.sprite = spriteTomasHablando;
            }

            // Audio
            if (line.voiceClip != null && AudioSource != null)
            {
                AudioSource.Stop();
                AudioSource.clip = line.voiceClip;
                AudioSource.Play();
            }
            if (currentLine == dialogueLines.Count-1)
            {
                imagenMama.gameObject.SetActive(false);
                imagenTomas.gameObject.SetActive(false);

                // Cambiar el sprite según el mundo seleccionado
                Image ambosImage = imagenAmbos.GetComponent<Image>();
                if (ambosImage != null && SeleccionMundoManager.Instance != null)
                {
                    if (SeleccionMundoManager.Instance.mundoSeleccionado == SeleccionMundoManager.Mundo.Gato)
                    {
                        ambosImage.sprite = spriteAmbosGatos;
                    }
                    else
                    {
                        ambosImage.sprite = spriteAmbosHumanos;
                    }
                }

                imagenAmbos.SetActive(true);
            }
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    void NextLine()
    {
        currentLine++;
        nextButton.gameObject.SetActive(false);
        if (AudioSource != null)
        {
            AudioSource.Stop();
        }
        ShowLine();
    }

    IEnumerator TypeText(string line)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f); // velocidad de tipeo
        }

        isTyping = false;
        nextButton.gameObject.SetActive(true);
    }
}
