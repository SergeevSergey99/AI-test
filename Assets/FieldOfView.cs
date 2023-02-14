using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public GameObject player;
    ClickManager cm;
    private void Awake()
    {
        cm = FindObjectOfType<ClickManager>();
    }
    public bool isPlayerSeen()
    {
        return player != null;
    }
    public Vector3 GetTargetPosition()
    {
        return player.transform.position;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            var dir = cm.player.transform.position - transform.parent.position;
            Debug.Log(other.gameObject.name +" " + cm.player.transform.position +" " + transform.parent.position + " " + dir);
            dir.y = 0;
            RaycastHit hit;
            if (Physics.Raycast(transform.parent.position,
                dir,
                out hit, 10f))
            {
                Debug.DrawLine(transform.parent.position, hit.point, Color.red, 0.1f);
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    player = other.gameObject;
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = null;
        }
    }
}
