using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cont : MonoBehaviour
{
    // declare reference variables
    private PlayerInput _playerInput;
    private CharacterController _characterController;
    private Animator _animator;
    
    // vaiables to store optimized setter/getter parameter IDs
    private int _isWalkingHash;
    private int _isRunningHash;
    
    // variables to store player input values
    private Vector2 _currentMovementInput;
    private Vector3 _currentMovement;
    private Vector3 _currentRunMovement;
    private bool _isMovementPressed;
    private bool _isRunPressed;
    private float _rotationFactorPerFrame = 10f;
    private float _runMultiplier = 3f;
    private void Awake()
    {
        _playerInput = new PlayerInput();
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();

        _isRunningHash = Animator.StringToHash("run");
        _isWalkingHash = Animator.StringToHash("walk");
        
        _playerInput.CharacterControls.Move.started += OnMovementInput;
        _playerInput.CharacterControls.Move.canceled += OnMovementInput;
        _playerInput.CharacterControls.Move.performed += OnMovementInput;
        _playerInput.CharacterControls.Run.started += OnRun;
        _playerInput.CharacterControls.Run.canceled += OnRun;
        // "started" is specifically called when the input system first receives the input. But that's it
        // If we wanna track when the key is let go, we need to use the "Canceled" callback.
        // "Performed" will continue to update the changes in the player input
    }

    void OnMovementInput(InputAction.CallbackContext context)
    {
        _currentMovementInput = context.ReadValue<Vector2>();
        _currentMovement.x = _currentMovementInput.x;
        _currentMovement.z = _currentMovementInput.y;
        _currentRunMovement.x = _currentMovementInput.x * _runMultiplier;
        _currentRunMovement.z = _currentMovementInput.y * _runMultiplier;
        _isMovementPressed = (_currentMovementInput.x != 0 || _currentMovementInput.y != 0);
    }

    void OnRun(InputAction.CallbackContext context)
    {
        _isRunPressed = context.ReadValueAsButton();
    }

    // Update is called once per frame
    void Update()
    {
        HandleGravity();
        HandleRotation();
        HandleAnimation();
        if (_isRunPressed)
        {
            _characterController.Move(_currentRunMovement * Time.deltaTime);
        }
        else
        {
            _characterController.Move(_currentMovement * Time.deltaTime);    
        }
    }

    private void HandleAnimation()
    {
        bool isWalking = _animator.GetBool(_isWalkingHash);
        bool isRunning = _animator.GetBool(_isRunningHash);

        if (_isMovementPressed && !isWalking)
        {
            _animator.SetBool(_isWalkingHash,true);
        }
        else if(!_isMovementPressed && isWalking )
        {
            _animator.SetBool(_isWalkingHash,false);
        }

        if ((_isMovementPressed  && _isRunPressed )&& !isRunning)
        {
            _animator.SetBool(_isRunningHash,true);
        }else if((!_isMovementPressed || !_isRunPressed ) && isRunning)
        {
            _animator.SetBool(_isRunningHash,false);
        }        
    }
    private void HandleRotation()
    {
        Vector3 positionToLookAt;
        // the change in position character should point to
        positionToLookAt.x = _currentMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = _currentMovement.z;
        // the current rotation of our character
        Quaternion currentRotation = transform.rotation;

        if (_isMovementPressed)
        {
            Debug.Log("눌렸고 HandleRotation");
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, _rotationFactorPerFrame*Time.deltaTime);
        }
        
    }
    private void HandleGravity()
    {
        if (_characterController.isGrounded)
        {
            float groundedGravity = -0.05f;
            _currentMovement.y = groundedGravity;
            _currentRunMovement.y = groundedGravity;
        }
        else
        {
            float gravity = -9.8f;
            _currentMovement.y = gravity;
            _currentRunMovement.y = gravity;
        }
    }
    private void OnEnable()
    {
        _playerInput.CharacterControls.Enable();
    }

    private void OnDisable()
    {
        _playerInput.CharacterControls.Disable();
    }
}
