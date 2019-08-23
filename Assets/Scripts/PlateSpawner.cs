using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateSpawner : MonoBehaviour
{
    public GameObject prefabPlate;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            GameObject go = Instantiate(prefabPlate, transform.position, Quaternion.identity, transform);

            PlateParams plateParams = new PlateParams
            {
                radius = Random.Range(3f, 5f),
                baseRadius = Random.Range(1f, 3f),
                depth = Random.Range(0.25f, 2f),
                thickness = Random.Range(0.05f, 0.2f),
                segmentCount = Random.Range(3, 128)
            };

            go.GetComponent<MeshFilter>().sharedMesh = ProcPlate.GeneratePlate(plateParams);
            go.GetComponent<MeshCollider>().sharedMesh = go.GetComponent<MeshFilter>().sharedMesh;

            Vector3 force = new Vector3(Random.Range(-10f, 10f), 10f, Random.Range(-10f, 10f));
            go.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        }
    }
}
