using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dart : MonoBehaviour
{
    Rigidbody rig;
    bool isflying = false;
    public float dartPower = 0;
    public float flyingTime = 1;
    public bool IsFlying
    {
        set
        {
            if(!isflying && (isflying = value))
            {
                rig = GetComponent<Rigidbody>();
                rig.useGravity = true;
                transform.parent = null;
                rig.AddForce(transform.rotation * Vector3.back * dartPower);
                SoundManager.instance.PlaySound(SoundManager.instance.DartFlySound);
            }
        }
    }

    // Update is called once per frame
    private void Start()
    {
        Destroy(this.gameObject, 1.1f);
    }

    private void OnDestroy()
    {
        GameManager.instance.SpawnDart();
    }
}
