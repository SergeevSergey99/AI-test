using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickManager : MonoBehaviour
{
    public GameObject player;

    private void Update()
    {
        Debug.Log("Player " + player.transform.position);
        if (Input.GetMouseButtonDown(0) && player != null)
        {
            RaycastHit hit;
            Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.DrawLine(ray.origin, hit.point, Color.red, 2f);
                if (hit.collider.gameObject.CompareTag("Ground"))
                    player.GetComponent<NavMeshAgent>().destination = 
                        new Vector3(hit.point.x, player.transform.position.y, hit.point.z);
            }
        }
    }
}
