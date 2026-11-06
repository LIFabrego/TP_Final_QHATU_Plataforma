using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [Header("Game Elements")]
    [Range(2, 6)]
    [SerializeField]
    private int dificultad = 4;
    [SerializeField]
    private Transform gameHolder;
    [SerializeField]
    private Transform piezasPrefab;


    [Header("UI Elements")]
    [SerializeField]
    private List<Texture2D> imageTextures;
    [SerializeField]
    private Transform levelSelectPanel;
    [SerializeField]
    private Image levelSelectPrefab;
    [SerializeField]
    private GameObject playAgainButton;


    private List<Transform> piezas;
    private Vector2Int dimensiones;
    private float width;
    private float height;

    private Transform draggingPiece = null;
    private Vector3 offset;

    private int piezasCorrectas;
    void Start()
    {
        //Crea la UI
        foreach (Texture2D texture in imageTextures)
        {
            Image image = Instantiate(levelSelectPrefab, levelSelectPanel);
            image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            //Asignar el boton de accion
            image.GetComponent<Button>().onClick.AddListener(delegate { StartGame(texture); });
        }

    }
    public void StartGame(Texture2D jigsawTexture)
    {
        levelSelectPanel.gameObject.SetActive(false);
        piezas = new List<Transform>();
        //Calcular las piezas
        dimensiones = GetDimensions(jigsawTexture, dificultad);
        CreateJigsawPieces(jigsawTexture);

        Scatter();

        UpdateBorder();

        piezasCorrectas = 0;
    }

    private Vector2Int GetDimensions(Texture2D jigsawTexture, int dificultad)
    {
        Vector2Int dimenciones = Vector2Int.zero;
        if (jigsawTexture.width < jigsawTexture.height)
        {
            dimenciones.x = dificultad;
            dimenciones.y = (dificultad * jigsawTexture.height) / jigsawTexture.width;
        }
        else
        {
            dimenciones.x = (dificultad * jigsawTexture.width) / jigsawTexture.height;
            dimenciones.y = dificultad;
        }
        return dimenciones;
    }
    void CreateJigsawPieces(Texture2D jigsawTexture)
    {
        height = 1f / dimensiones.y;
        float aspect = (float)jigsawTexture.width / jigsawTexture.height;
        width = aspect / dimensiones.x;
        for (int row = 0; row < dimensiones.y; row++)
        {
            for (int col = 0; col < dimensiones.x; col++)
            {
                Transform piece = Instantiate(piezasPrefab, gameHolder);
                piece.localPosition = new Vector3((-width * dimensiones.x / 2) + (width * col) + (width / 2), (-height * dimensiones.y / 2) + (height * row) + (height / 2), -1);
                piece.localScale = new Vector3(width, height, 1f);
                piece.name = $"Piezas{(row * dimensiones.x) + col} ";
                piezas.Add(piece);


                float width1 = 1f / dimensiones.x;
                float height1 = 1f / dimensiones.y;

                Vector2[] uv = new Vector2[4];
                uv[0] = new Vector2(width1 * col, height1 * row);
                uv[1] = new Vector2(width1 * (col + 1), height1 * row);
                uv[2] = new Vector2(width1 * col, height1 * (row + 1));
                uv[3] = new Vector2(width1 * (col + 1), height1 * (row + 1));

                Mesh mesh = piece.GetComponent<MeshFilter>().mesh;
                mesh.uv = uv;
                piece.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", jigsawTexture);

            }
        }

    }
    private void Scatter()
    {
        float orthoHeight = Camera.main.orthographicSize;
        float screenAspect = (float)Screen.width / Screen.height;
        float orthoWidth = (screenAspect * orthoHeight);

        //
        float pieceWidth = width * gameHolder.localScale.x;
        float pieceHeight = height * gameHolder.localScale.y;

        orthoHeight -= pieceHeight;
        orthoWidth -= pieceWidth;

        // 
        foreach (Transform piece in piezas)
        {
            float x = Random.Range(-orthoWidth, orthoWidth);
            float y = Random.Range(-orthoHeight, orthoHeight);
            piece.position = new Vector3(x, y, -1);
        }
    }
    private void UpdateBorder()
    {
        LineRenderer lineRenderer = gameHolder.GetComponent<LineRenderer>();
        //Calcula el tamaño mediante codigo
        float halfWidht = (width * dimensiones.x) / 2f;
        float halfHeight = (height * dimensiones.y) / 2f;
        //
        float borderZ = 0f;
        //
        lineRenderer.SetPosition(0, new Vector3(-halfWidht, halfHeight, borderZ));
        lineRenderer.SetPosition(1, new Vector3(halfWidht, halfHeight, borderZ));
        lineRenderer.SetPosition(2, new Vector3(halfWidht, -halfHeight, borderZ));
        lineRenderer.SetPosition(3, new Vector3(-halfWidht, -halfHeight, borderZ));
        //
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        //
        lineRenderer.enabled = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit)
            {
                draggingPiece = hit.transform;
                offset = draggingPiece.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                offset += Vector3.back;
            }
        }
        if (draggingPiece && Input.GetMouseButtonUp(0))
        {
            SnapAndDisableIfCorrect();
            draggingPiece.position += Vector3.forward;
            draggingPiece = null;
        }
        if (draggingPiece)
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newPosition.z = draggingPiece.position.z;
            draggingPiece.position = newPosition;
        }
    }
    private void SnapAndDisableIfCorrect()
    {
        int piezaIndex = piezas.IndexOf(draggingPiece);
        //
        int col = piezaIndex % dimensiones.x;
        int row = piezaIndex / dimensiones.x;
        //
        Vector2 targetPosition = new Vector2((-width * dimensiones.x / 2) + (width * col) + (width / 2),
            (-height * dimensiones.y / 2) + (height * row) + (height / 2));
        //Verifica si esta en la posicion correcta
        if (Vector2.Distance(draggingPiece.localPosition, targetPosition) < (width / 2))
        {
            draggingPiece.localPosition = targetPosition;
            draggingPiece.GetComponent<BoxCollider2D>().enabled = false;
            //
            piezasCorrectas++;
            if (piezasCorrectas == piezas.Count)
            {
                playAgainButton.SetActive(true);
            }
        }
    }
    public void RestartGame()
    {
        /*
                    foreach (Transform piece in piezas) {
                        Destroy(piece.gameObject);
                    }
                    piezas.Clear();
                    gameHolder.GetComponent<LineRenderer>().enabled = false;
                    //
                    playAgainButton.SetActive(false);
                    levelSelectPanel.gameObject.SetActive(true);

                }
        */
        FichaManager.instance.minijuegoCompletado = true;
        SceneManager.LoadScene("MenuMinijuegos");

    }
}