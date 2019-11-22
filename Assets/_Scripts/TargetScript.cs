using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{

    WarpController warp;
    public GameObject uiLock;
    public Transform player;

    void Start()
    {
        warp = FindObjectOfType<WarpController>();
    }

    void Update()
    {
        uiLock.transform.LookAt(player);
    }

    private void OnBecameVisible()
    {
        if (!warp.screenTargets.Contains(transform))
            warp.screenTargets.Add(transform);
    }

    private void OnBecameInvisible()
    {
        if (warp.screenTargets.Contains(transform))
            warp.screenTargets.Remove(transform);
    }
}
