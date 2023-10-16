using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballon : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int score;
    [SerializeField] private GameObject particle;

    private void Awake()
    {
        Invoke("DestroyBallon", 10);
    }

    void DestroyBallon()
    {
        Destroy(gameObject);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Dart"))
        {
            Instantiate(particle, transform.position, Quaternion.identity);
            GameManager.instance.ComboUp();
            GameManager.instance.ScoreUp(score * GameManager.instance.currentCombo);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}