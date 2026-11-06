using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInButton : MonoBehaviour
{
    public GameObject skipButton;

    IEnumerator Start()
    {
        skipButton.SetActive(false);
        yield return new WaitForSeconds(3f);
        skipButton.SetActive(true);
    }
}
