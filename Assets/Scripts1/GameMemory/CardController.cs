using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CardController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> prefabs;
    public int maxCardType => prefabs.Count;
    public float cardSize = 2f;
    public UnityEvent<CardController> onClicked;
    public int cardType = -1;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    void Start()
    {
        if (cardType < 0)
        {
            cardType = UnityEngine.Random.Range(0, prefabs.Count);
        }
        Instantiate(prefabs[cardType],transform.position, Quaternion.identity, transform);
       

    }
    private void OnMouseUpAsButton()
    {
        onClicked.Invoke(this);
    }
    public void TestAnimation()
    {
        IEnumerator AnimationCoroutine()
        {
            Reveal();
            yield return new WaitForSeconds(2);
            Hide();
        }
        StartCoroutine(AnimationCoroutine());
    }
    public void Reveal()
    {
        _animator.SetBool(name: "Revelado", value:true);
    }
    public void Hide()
    {
        _animator.SetBool(name: "Revelado", value: false);
    }
}
