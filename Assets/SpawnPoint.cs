using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    RaycastHit hit;

    private void Update()
    {
        HeightSetting();
    }

    private void HeightSetting()
    {
        if(Physics.Raycast(transform.position, transform.up, out hit, 15f))
        {

        }
    }
}
