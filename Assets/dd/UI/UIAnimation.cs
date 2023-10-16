using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimation : MonoBehaviour
{
    public Animator anime;

    void OnEnable()
    {
        anime.SetTrigger("Start");
    }

    public void OffGameObject()
    {
        GameManager.instance.StartTimer();
        gameObject.SetActive(false);
    }
}
