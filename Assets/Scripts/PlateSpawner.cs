using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateSpawner : MonoBehaviour
{
    public GameObject prefabPlate;

    private POTTERY_TYPE spawnMode = POTTERY_TYPE.PLATE;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if(spawnMode == POTTERY_TYPE.PLATE)
            {
                spawnMode = POTTERY_TYPE.MUG;
            }
            else
            {
                spawnMode = POTTERY_TYPE.PLATE;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject go = Instantiate(prefabPlate, transform.position, Quaternion.identity, transform);

            if (spawnMode == POTTERY_TYPE.PLATE)
            {               
                float radius = Random.Range(3f, 5f);
                float baseRadius = Random.Range(1f, 3f);
                float depth = Random.Range(0.25f, 2f);
                float thickness = Random.Range(0.05f, 0.2f);
                int segmentCount = Random.Range(3, 128);

                go.GetComponent<MeshFilter>().sharedMesh = ProcPlate.GeneratePlate(radius, baseRadius, depth, thickness, segmentCount);            
            }

            if (spawnMode == POTTERY_TYPE.MUG)
            {
                float radius = Random.Range(0.75f, 1.5f);
                float height = Random.Range(2f, 4f);
                float thickness = Random.Range(0.05f, 0.2f);
                int segmentCount = Random.Range(3, 128);

                go.GetComponent<MeshFilter>().sharedMesh = ProcMug.GenerateMug(radius, thickness, height, segmentCount, 0);                
            }

            go.GetComponent<MeshCollider>().sharedMesh = go.GetComponent<MeshFilter>().sharedMesh;
            Vector3 force = new Vector3(Random.Range(-10f, 10f), 10f, Random.Range(-10f, 10f));
            go.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        }
    }
}

public enum POTTERY_TYPE { PLATE, MUG }
