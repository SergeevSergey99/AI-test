using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffInGame : MonoBehaviour
{
    private void Awake()
    {
        gameObject.SetActive(false);
    }
}
