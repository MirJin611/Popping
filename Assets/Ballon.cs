using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballon : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int score;
    [SerializeField] private GameObject particle;
    [SerializeField] AudioSource audioSource;

    private void Awake()
    {
        Destroy(gameObject,10);
        speed += UnityEngine.Random.Range(0, 21)*0.001f;
    }


    void Update()
    {
        transform.Translate(Vector3.forward * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dart"))
        {
            SoundManager.instance.PlaySound(SoundManager.instance.BallonBoomSound);
            Instantiate(particle, transform.position, Quaternion.identity);
            GameManager.instance.ComboUp();
            GameManager.instance.ScoreUp(score * GameManager.instance.currentCombo);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
