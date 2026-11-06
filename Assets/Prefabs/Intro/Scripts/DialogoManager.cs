using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogoManager : MonoBehaviour
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

    private int currentLine = 0;
    private bool isTyping = false;

    void Start()
    {
        nextButton.onClick.AddListener(OnNextButtonClicked);
        nextButton.gameObject.SetActive(false);
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
            speakerNameText.text = dialogueLines[currentLine].speaker;
            StartCoroutine(TypeText(dialogueLines[currentLine].text));

            if (line.voiceClip != null && AudioSource != null)
            {
                AudioSource.Stop();
                AudioSource.clip = line.voiceClip;
                AudioSource.Play();
            }
        }
        else
        {
            SceneManager.LoadScene("Intro");
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
