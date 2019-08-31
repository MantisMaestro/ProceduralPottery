using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGizmosForVertices : MonoBehaviour
{
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Mesh mesh = GetComponent<MeshFilter>().sharedMesh;

		foreach(Vector3 v in mesh.vertices)
		{
			Gizmos.DrawSphere(transform.rotation * v + transform.position, 0.01f);
		}
	}
}
