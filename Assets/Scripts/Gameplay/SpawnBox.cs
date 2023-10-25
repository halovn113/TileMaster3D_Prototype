using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBox : MonoBehaviour
{
    public Vector3 center;
    public Vector3 size;
    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.position, size);
    }

    public void SpawnObjectFromRandomPosition(GameObject gameObject)
    {
        gameObject.transform.eulerAngles = new Vector3(0, Random.Range(-360, 360), 0);
        gameObject.transform.position = transform.position +
                    new Vector3(Random.Range(-size.x / 2, size.x / 2),
                    Random.Range(-size.y / 2, size.y / 2),
                    Random.Range(-size.z / 2, size.z / 2));
    }
}
