using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryGate : MonoBehaviour
{
    [SerializeField] private LevelSceneController _levelSceneController;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            _levelSceneController.WinGame(true);
        }
    }
}
