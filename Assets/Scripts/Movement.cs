using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private CharacterController _characterController;
    [SerializeField]
    private float _speedMove;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    public void Move(Vector2 position)
    {
        Vector3 direction = new Vector3(position.x, 0, position.y);
        direction = transform.TransformDirection(direction);
        direction *= _speedMove;

        _characterController.Move(direction * Time.deltaTime);
        
        //transform.Translate(direction * _speedMove * Time.deltaTime);
    }

    //private void OnMove(InputAction inputAction) 
    //{
    //    _speedMove = inputAction.IsPressed() ? 1 : 2;
    //}
}
