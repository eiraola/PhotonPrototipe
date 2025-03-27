using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
[RequireComponent(typeof(CharacterController))]
public class SimpleLocomotion : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1f;
    [SerializeField]
    private float _rotationSpeed = 1f;
    [SerializeField]
    private Transform _playerTarget;
    private PhotonView _photonView;
    private CharacterController _characterController;
    private Camera _camera;
    public Camera Camera { get => _camera; set => _camera = value; }

    // Start is called before the first frame update
    void Start()
    {
        _photonView = GetComponent<PhotonView>();
        _characterController = GetComponent<CharacterController>();
    }

   
    void Update()
    {
        if (_photonView.IsMine)
        {
            Vector3 newVector = _camera.transform.forward * Input.GetAxisRaw("Vertical");
            Vector3 newVector2 = _camera.transform.right * Input.GetAxisRaw("Horizontal");
            Vector3 newDirection = (newVector + newVector2).normalized;
            newDirection.y = 0f;
            _characterController.Move(newDirection.normalized * Time.deltaTime * _speed);
            if(newDirection.magnitude > 0.01f)
            {
                float singleStep = _rotationSpeed * Time.deltaTime;
                Vector3 newWjatchRotation = Vector3.RotateTowards(_playerTarget.forward, newDirection, singleStep, 0.0f);
                _playerTarget.rotation = Quaternion.LookRotation(newWjatchRotation);
            }
        }
    }
}
