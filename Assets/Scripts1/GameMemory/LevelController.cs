using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [Serializable]
    public class LevelData
    {
        public int colums;
        public int rows;
        public int difficulty;
        public int movements;
    }

    [SerializeField]
    private CardController _cardPrefab;

    [Header("UI References")]
    [SerializeField]
    private TMP_Text levelText;
    [SerializeField]
    private TMP_Text movementsText;
    [SerializeField]
    private GameObject gameOverButton;

    [Header("LevelData")]
    [SerializeField]
    private List<LevelData> levels = new List<LevelData>();

    [Header("Card Container")]
    [SerializeField]
    private Transform cardContainer; // ← Nuevo contenedor de cartas

    private List<CardController> _cards = new List<CardController>();
    private CardController activeCard;
    private int movementsUsed = 0;
    private bool blockInput = true;
    private int level = 0;

    private void Start()
    {
        level = PlayerPrefs.GetInt("Level", 0);
        StarLevel();
    }

    public void StarLevel()
    {
        gameOverButton.SetActive(false);

        // Limpiar cartas anteriores (si las hay)
        foreach (Transform child in cardContainer)
        {
            Destroy(child.gameObject);
        }
        _cards.Clear();

        // Asegurarse de que el número de cartas sea par
        Debug.Assert((levels[level].rows * levels[level].colums) % 2 == 0);

        if (levels[level].difficulty > _cardPrefab.maxCardType)
        {
            levels[level].difficulty = Math.Min(levels[level].difficulty, _cardPrefab.maxCardType);
            Debug.Assert(false);
        }

        // Preparar tipos de carta posibles
        List<int> allTypes = new List<int>();
        for (int i = 0; i < _cardPrefab.maxCardType; i++)
        {
            allTypes.Add(i);
        }

        List<int> gameTypes = new List<int>();
        for (int i = 0; i < levels[level].difficulty; i++)
        {
            int chosenType = allTypes[UnityEngine.Random.Range(0, allTypes.Count)];
            allTypes.Remove(chosenType);
            gameTypes.Add(chosenType);
        }

        List<int> chosenTypes = new List<int>();
        for (int i = 0; i < levels[level].rows * levels[level].colums / 2; i++)
        {
            int chosenType = gameTypes[UnityEngine.Random.Range(0, gameTypes.Count)];
            chosenTypes.Add(chosenType);
            chosenTypes.Add(chosenType);
        }

        // Configurar posicionamiento de cartas
        float cardWidth = _cardPrefab.cardSize;
        float cardHeight = _cardPrefab.cardSize * 1.6f;
        Vector3 offset = new Vector3((levels[level].colums - 1) * cardWidth * 0.5f, (levels[level].rows - 1) * cardHeight * 0.5f, 0f);

        for (int y = 0; y < levels[level].rows; ++y)
        {
            for (int x = 0; x < levels[level].colums; ++x)
            {
                Vector3 position = new Vector3(x * cardWidth, y * cardHeight, 0f);
                var card = Instantiate(_cardPrefab, position - offset, Quaternion.identity, cardContainer); // ← Aquí usamos el contenedor
                card.cardType = chosenTypes[UnityEngine.Random.Range(0, chosenTypes.Count)];
                chosenTypes.Remove(card.cardType);
                card.onClicked.AddListener(OnCardClicked);
                _cards.Add(card);
            }
        }

        blockInput = false;
        movementsUsed = 0;
        levelText.text = $"Level: {level}";
        movementsText.text = $"Movimientos: {levels[level].movements}";
    }

    private void OnCardClicked(CardController card)
    {
        if (blockInput)
        {
            return;
        }
        blockInput = true;

        if (activeCard == null)
        {
            // Activar la carta
            StartCoroutine(SelectCard(card));
            return;
        }

        movementsUsed++;
        movementsText.text = $"Movimientos: {levels[level].movements - movementsUsed}";

        if (card.cardType == activeCard.cardType)
        {
            StartCoroutine(Score(card));
            return;
        }

        StartCoroutine(Fail(card));
    }

    private IEnumerator SelectCard(CardController card)
    {
        activeCard = card;
        activeCard.Reveal();
        yield return new WaitForSeconds(0.5f);
        blockInput = false;
    }

    private IEnumerator Score(CardController card)
    {
        card.Reveal();
        yield return new WaitForSeconds(1f);
        _cards.Remove(activeCard);
        _cards.Remove(card);
        Destroy(card.gameObject);
        Destroy(activeCard.gameObject);
        activeCard = null;

        if (_cards.Count < 1)
        {
            Win();
            yield break;
        }

        if (movementsUsed >= levels[level].movements)
        {
            Lose();
            yield break;
        }

        blockInput = false;
    }

    private IEnumerator Fail(CardController card)
    {
        card.Reveal();
        yield return new WaitForSeconds(1f);
        activeCard.Hide();
        card.Hide();
        activeCard = null;
        yield return new WaitForSeconds(0.5f);
        blockInput = false;

        if (movementsUsed >= levels[level].movements)
        {
            Lose();
            yield break;
        }
    }

    private void Win()
    {
        level++;
        if (level >= levels.Count)
        {
            level = 0;
        }
        PlayerPrefs.SetInt("Level", level);
        Debug.Log("Victoria");
        gameOverButton.SetActive(true);
    }

    private void Lose()
    {
        Debug.Log("Derrota");
        level = 0; // Reiniciar al nivel 0
        PlayerPrefs.SetInt("Level", level); // Guardar el nivel en PlayerPrefs
        StarLevel(); // Reiniciar inmediatamente el nivel 0
    }
    public void OnExitButtonClicked()
    {
        // Cambiar a la escena del menú, asumiendo que la escena del menú se llama "MainMenu"
        SceneManager.LoadScene("MenuMinijuegos");
    }
}