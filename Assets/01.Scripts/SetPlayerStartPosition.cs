using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerStartPosition : MonoBehaviour
{
    private Vector3 startPosition;
    private Player _player;

    private void Start()
    {
        _player = GameManager.Instance.Player;

        if(_player == null)
        {
            Debug.Log("플레이어가 존재하지 않음");
        }

        startPosition = transform.position;

        InitializePlayerPosition();
    }

    private void InitializePlayerPosition()
    {
        if (_player == null) return;

        _player.Agent.enabled = false;
        _player.transform.position = startPosition;
        _player.Agent.enabled = true;

        _player.StateMachine.ChangeState(_player.StateMachine.RunState);
    }
}
