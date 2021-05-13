using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] private VoidEvent OnStartGame;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnStartGame.Raise();
        }
    }
}
