using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPath : MonoBehaviour {

    [SerializeField]
    private Color lineColor;

    private List<Transform> nodes = new List<Transform>();


    void OnDrawGizmosSelected()
    {
        Gizmos.color = lineColor;
        Transform[] pathtransforms = GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();

        for(int i = 0; i<pathtransforms.Length; i++)
        {
            if (pathtransforms[i] != transform)
            {
                nodes.Add(pathtransforms[i]);
            }
        }
        for(int i = 0; i < nodes.Count; i++)
        {
            Vector3 currentNode = nodes[i].position;
            Vector3 previousNode = Vector3.zero;
            if (i > 0)
            {
               previousNode = nodes[i - 1].position;
            }
            else if(i==0 && nodes.Count>1)
            {
                previousNode = nodes[nodes.Count - 1].position;
            }
            Gizmos.DrawLine(previousNode, currentNode);
            Gizmos.DrawWireSphere(currentNode, 0.3f);
        }
    }
 
}
