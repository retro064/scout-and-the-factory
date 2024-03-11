using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public float speed;
    public Vector3 direction;
    public List<GameObject> onBelt;

    void Update()
    {
        for (int i = 0; i <= onBelt.Count -1; i++)
        {
            onBelt[i].GetComponent<Rigidbody>().velocity = speed * direction * Time.deltaTime;
        }
    }
    
    void OnCollisionEnter(Collision collision)
    {
        onBelt.Add(collision.gameObject);
    }

    void OnCollisionExit(Collision collision)
    {
        onBelt.Remove(collision.gameObject);
    }
}
