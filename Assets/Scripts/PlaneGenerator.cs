using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] plane;
    private float spawnPos = 0;
    private float planeLength = 100;
    private List<GameObject> activePlane = new List<GameObject>();
    [SerializeField] private Transform player;

    private int startPlanes = 6;


    void Start()
    {
        for (int i = 0; i < startPlanes; i++)
        {
            SpawnPlane(Random.Range(0, plane.Length));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(player.position.z-60>spawnPos-(startPlanes*planeLength))
        {
            SpawnPlane(Random.Range(0, plane.Length));
            DeletePlane();
        }
        
    }

    private void SpawnPlane(int PlaneIndex)
    {
        GameObject nextPlane=Instantiate(plane[PlaneIndex], transform.forward * spawnPos, transform.rotation);
        activePlane.Add(nextPlane);
        spawnPos += planeLength;
    }
    private void DeletePlane()
    {
        Destroy(activePlane[0]);
        activePlane.RemoveAt(0);
    }
}
