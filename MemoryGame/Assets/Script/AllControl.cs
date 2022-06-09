using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllControl : MonoBehaviour
{
    static GameObject instance;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance==null)
        {
            instance = gameObject;
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
