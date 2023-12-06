using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;
using System.Diagnostics;

public class BallonGenerator : MonoBehaviour
{
    public ARPlaneManager planeManager;
    [SerializeField] private GameObject[] ballons = new GameObject[3];
    public float[] spawnTime = new float[3] { 1f, 0.7f, 0.5f };
    public int currentSpawnTime = 0;
    public Transform playerPosition;

    public IEnumerator StartSpawnBallon()
    {
        SpawnBallon();
        yield return new WaitForSeconds(spawnTime[currentSpawnTime]);
        StartCoroutine(StartSpawnBallon());
    }

    private void SpawnBallon()
    {
        int i = Random.Range(0, spawnPointList.Count);

        GameObject ballon = Instantiate(GetBallon(), spawnPointList[i], Quaternion.Euler(-90, 0, 0));
    }

    private GameObject GetBallon()
    {
        int ballonIndex = Random.Range(1, 10);

        if (ballonIndex <= 5)
            return ballons[0];
        else if (ballonIndex > 5 && ballonIndex <= 8)
            return ballons[1];
        else
            return ballons[2];
    }

    [SerializeField] private List<Vector3> vertices;
    [SerializeField] private List<GameObject> points;
    [SerializeField] private float radius = 0.5f;

    [SerializeField] private GameObject spawnPointPrefabs;
    [SerializeField] private Transform cameraTransform;

    public List<Vector3> spawnPointList;

    public void SetSpawnPoints()
    {
        planeManager.enabled = false;

        foreach (var plane in planeManager.trackables)
        {
            for (int i = 0; i < plane.GetComponent<MeshFilter>().mesh.vertices.Length; i++)
            {
                if (Vector3.Distance(playerPosition.position, plane.GetComponent<MeshFilter>().mesh.vertices[i]) >= 1.5f)
                {
                    spawnPointList.Add(plane.GetComponent<MeshFilter>().mesh.vertices[i]);
                    UnityEngine.Debug.Log(plane.GetComponent<MeshFilter>().mesh.vertices[i]);
                } 
            }
        }

        StartCoroutine(StartSpawnBallon());
    }

    private void Start()
    {
        //SetSpawnPoint();
    }

    private void SetSpawnPoint()
    {
        vertices = new List<Vector3>();
        float heading;
        for (int a = 0; a < 360; a += 360 / 120)
        {
            heading = a * Mathf.Deg2Rad;
            vertices.Add(new Vector3(Mathf.Cos(heading) * radius, transform.position.y, Mathf.Sin(heading) * radius));
        }

        for (int i = 0; i < vertices.Count - 1; i++)
        {
            Vector3 viewPos = Camera.main.WorldToViewportPoint(vertices[i]);
            if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0)
            {
                GameObject point = Instantiate(spawnPointPrefabs, vertices[i], Quaternion.identity);

                point.transform.SetParent(cameraTransform);
                points.Add(point);
            }
        }
    }

    public void EndSpawnBallon()
    {
        StopAllCoroutines();
    }
}
