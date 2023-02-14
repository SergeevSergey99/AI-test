using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public List<GameObject> pathPoints;
    public float waitTime = 1;
    NavMeshAgent nma;
    FieldOfView fov;
    int targetPointindex = 0;
    Coroutine cor;
    private void Start()
    {
        nma = GetComponent<NavMeshAgent>();
        fov = GetComponentInChildren<FieldOfView>();
        cor = StartCoroutine(MoveToTarget());
    }

    private void Update()
    {
        if (fov.isPlayerSeen())
        {
            // Останавливаем патрулирование
            if (cor != null)
            {
                StopCoroutine(cor);
                cor = null;
            }
            // И бежи к плееру
            nma.destination = fov.GetTargetPosition();
        }
        else 
        {
            if (cor == null && isMoving() == false)
            {
                cor = StartCoroutine(waitAndPatrol());
            }
        }
    }

    IEnumerator waitAndPatrol()
    {
        yield return new WaitForSeconds(3);
        cor = StartCoroutine(MoveToTarget());
    }

    bool isMoving()
    {
        if(nma.pathPending == false)
        {
            if(nma.remainingDistance <= nma.stoppingDistance)
            {
                if(nma.hasPath == false || nma.velocity.magnitude == 0f)
                {
                    return false;
                }
            }
        }

        return true;
    }

    IEnumerator MoveToTarget()
    {
        nma.destination = pathPoints[targetPointindex].transform.position;

        while (isMoving())
        {
            yield return null;
        }

        yield return new WaitForSeconds(waitTime);
        targetPointindex = (targetPointindex + 1) % pathPoints.Count;
        cor = StartCoroutine(MoveToTarget());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if(pathPoints.Count > 1)
        {
            for (int i = 0; i < pathPoints.Count - 1; i++)
            {
                if(pathPoints[i] != null && pathPoints[i + 1] != null)
                {
                    Gizmos.DrawLine(
                        pathPoints[i].transform.position,
                        pathPoints[i + 1].transform.position);
                }
            }
        }
    }
}
