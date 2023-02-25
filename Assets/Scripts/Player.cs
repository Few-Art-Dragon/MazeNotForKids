using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using TMPro;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Image _eyeImage;
    
    [SerializeField]
    private float _fadeEyes = 0;
    [SerializeField]
    private CinemachineVirtualCamera _camera;
    [SerializeField]
    private GameObject _cameraFollow;
    private Movement _movement;
    private PlayerInput _input;
    private bool _closedEyes = false;


    Vector2 _acceleration;

    private void Awake()
    {
        _movement = GetComponent<Movement>();
        _input = GetComponent<PlayerInput>();
        
    }

    private void Update()
    {
        
        _movement.Move(_input.actions["Move"].ReadValue<Vector2>());
        
        _cameraFollow.transform.rotation = _camera.transform.rotation;
        transform.rotation = new Quaternion(transform.rotation.x, _camera.transform.rotation.y, transform.rotation.z, transform.rotation.w);
        PlusFadeEyes();
        _acceleration.x = Input.acceleration.x;
        _acceleration.y = Input.acceleration.y;
    }

    private void PlusFadeEyes()
    {
        if (!_closedEyes)
        {
            _fadeEyes = CheckFadeEyesOnFull() + Time.deltaTime / 10;
        }
    }

    private float CheckFadeEyesOnFull()
    {
        if (_fadeEyes >= 1.0f)
        {
            _fadeEyes = 0;
            _closedEyes = true;
            StartCoroutine(nameof(ISetAlphaColorImage));     
        }

        return _fadeEyes;
    }
    IEnumerator ISetAlphaColorImage()
    {
        while (_eyeImage.color.a < 1)
        {
            _eyeImage.color = new Color(_eyeImage.color.r, _eyeImage.color.g, _eyeImage.color.b, _eyeImage.color.a + 0.2f);
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(0.2f);
        while (_eyeImage.color.a > 0)
        {
            _eyeImage.color = new Color(_eyeImage.color.r, _eyeImage.color.g, _eyeImage.color.b, _eyeImage.color.a - 0.2f);
            yield return new WaitForSeconds(0.05f);
        }
        
        _closedEyes = false;

    }
}
