using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLight : MonoBehaviour
{
    public Material mat;
    public Transform player;

    void Update()
    {
        if (mat != null)
        {
            mat.SetVector("_PlayerPos", player.position);
        }
    }
}