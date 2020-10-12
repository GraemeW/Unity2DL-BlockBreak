using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    // State
    SceneLoader sceneLoader;
    [SerializeField] int numberOfBlocks = 0; // Serialized for debugging purposes

    // Start is called before the first frame update
    void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    public void IncrementBreakableBlocks()
    {
        numberOfBlocks++;
    }

    internal void DecrementBreakableBlocks()
    {
        numberOfBlocks--;
        if (numberOfBlocks <= 0)
        {
            sceneLoader.LoadNextScene();
        }
    }
}
