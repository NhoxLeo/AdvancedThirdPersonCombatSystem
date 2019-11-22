using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class WarpController : MonoBehaviour
{
    [Header("Current Target")]
    public Transform target;
    [Space]
    public float minDistance; //The distance between the closest target and cursor
    public int allowedDistance; //The distance allowed between the target and cursor to receive a target
    [Space]
    public List<Transform> screenTargets = new List<Transform>();

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (screenTargets.Count < 1)
        {
            target = null;
            return;
        }

        target = screenTargets[targetIndex()];

        foreach (Transform gm in screenTargets)
        {
            gm.GetComponent<TargetScript>().uiLock.SetActive(false);
        }
        if (minDistance < allowedDistance)
        {
            target.GetComponent<TargetScript>().uiLock.SetActive(true);
        }
        else
        {
            target = null;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.None;
        }
    }
    
    public int targetIndex()
    {
        float[] distances = new float[screenTargets.Count];

        for (int i = 0; i < screenTargets.Count; i++)
        {
            distances[i] = Vector2.Distance(Camera.main.WorldToScreenPoint(screenTargets[i].position), new Vector2(Screen.width / 2, Screen.height / 2));
        }

        minDistance = Mathf.Min(distances);
        int index = 0;

        for (int i = 0; i < distances.Length; i++)
        {
            if (minDistance == distances[i])
            {
                index = i;
            }
        }
        return index;

    }

}