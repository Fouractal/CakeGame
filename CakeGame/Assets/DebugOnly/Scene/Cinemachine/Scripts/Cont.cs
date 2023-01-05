using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cont : MonoBehaviour
{

    #region declare reference variables
    private PlayerInput _playerInput;
    private CharacterController _characterController;
    private Animator _animator;
    #endregion

    #region vaiables to store optimized setter/getter parameter IDs
    private int _isWalkingHash;
    private int _isRunningHash;
    private int _isJumpingHash;
    private int _jumpCountHash;
    #endregion

    #region variables to store player input values
    private Vector2 _currentMovementInput;
    private Vector3 _currentMovement;
    private Vector3 _currentRunMovement;
    private bool _isMovementPressed;
    private bool _isRunPressed;
    #endregion

    #region constants
    private float _rotationFactorPerFrame = 10f;
    private float _runMultiplier = 3f;
    float _groundedGravity = -0.05f;
    private float _gravity = -9.8f;
    #endregion

    #region Jumping variables
    private bool _isJumpPressed = false;
    private bool _isJumping = false;
    private float _initialJumpVelocity;
    private float _maxJumpHeight = 4f;
    private float _maxJumpTime = 0.75f;
    private bool _isJumpAnimating = false;
    private int _jumpCount = 0;
    private Dictionary<int, float> _initialJumpVelocities = new Dictionary<int, float>();
    private Dictionary<int, float> _jumpGravities = new Dictionary<int, float>();
    private Coroutine _currentJumpResetRoutine = null;
    #endregion
    
    private void Awake()
    {
        _playerInput = new PlayerInput();
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();

        _isRunningHash = Animator.StringToHash("run");
        _isWalkingHash = Animator.StringToHash("walk");
        _isJumpingHash = Animator.StringToHash("isJumping");
        _jumpCountHash = Animator.StringToHash("jumpCount");
        
        _playerInput.CharacterControls.Move.started += OnMovementInput;
        _playerInput.CharacterControls.Move.canceled += OnMovementInput;
        _playerInput.CharacterControls.Move.performed += OnMovementInput;
        _playerInput.CharacterControls.Run.started += OnRun;
        _playerInput.CharacterControls.Run.canceled += OnRun;
        _playerInput.CharacterControls.Jump.started += OnJump;
        _playerInput.CharacterControls.Jump.canceled += OnJump;
        // "started" is specifically called when the input system first receives the input. But that's it
        // If we wanna track when the key is let go, we need to use the "Canceled" callback.
        // "Performed" will continue to update the changes in the player input

        SetUpJumpVariables();
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

    void OnJump(InputAction.CallbackContext context)
    {
        _isJumpPressed = context.ReadValueAsButton();
    }

    void SetUpJumpVariables()
    {
        float timeToApex = _maxJumpTime / 2;
        _gravity = (-2 * _maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        _initialJumpVelocity = (2 * _maxJumpHeight) / timeToApex;
        
        float secondJumpGravity = (-2* (_maxJumpHeight + 2)) / Mathf.Pow((timeToApex*1.25f),2);
        float secondJumpInitialVelocity = (2 * (_maxJumpHeight * 2)) / (timeToApex * 1.5f);
        float thirdJumpGravity = (-2* (_maxJumpHeight + 4)) / Mathf.Pow((timeToApex*1.5f),2);
        float thirdJumpInitialVelocity = (2 * (_maxJumpHeight * 4)) / (timeToApex * 2f);
        
        _initialJumpVelocities.Add(1,_initialJumpVelocity);
        _initialJumpVelocities.Add(2,secondJumpInitialVelocity);
        _initialJumpVelocities.Add(3,thirdJumpInitialVelocity);
        
        _jumpGravities.Add(0,_gravity);
        _jumpGravities.Add(1,_gravity);
        _jumpGravities.Add(2,secondJumpGravity);
        _jumpGravities.Add(3,thirdJumpGravity);
    }
    
    // Update is called once per frame
    void Update()
    {
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
        HandleGravity();
        HandleJump();
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
        bool isFalling = _currentMovement.y <= 0.0f || !_isJumpPressed;
        float fallMultiplier = 2.0f;
        
        if (_characterController.isGrounded)
        {
            if (_isJumpAnimating)
            {
                _animator.SetBool(_isJumpingHash,false);
                _isJumpAnimating = false;
                _currentJumpResetRoutine = StartCoroutine(JumpResetRoutine());
                if (_jumpCount == 3)
                {
                    _jumpCount = 0;
                    _animator.SetInteger(_jumpCountHash,_jumpCount);
                }
            }
            _currentMovement.y = _groundedGravity;
            _currentRunMovement.y = _groundedGravity;
        }else if (isFalling)
        {
            float previousVelocityY = _currentMovement.y;
            float newVelocityY = _currentMovement.y + (_jumpGravities[_jumpCount] * fallMultiplier * Time.deltaTime);
            float nextVelocityY = Mathf.Max((previousVelocityY + newVelocityY) * .5f,-20.0f) ;
            _currentMovement.y = nextVelocityY;
            _currentRunMovement.y = nextVelocityY;
        }
        else
        {   // 항상 같은 높이뛰기 위함
            float previousVelocityY = _currentMovement.y;
            float newVelocityY = _currentMovement.y + (_jumpGravities[_jumpCount] * Time.deltaTime);
            float nextVelocityY = (previousVelocityY + newVelocityY) * .5f;
            _currentMovement.y = nextVelocityY;
            _currentRunMovement.y = nextVelocityY;
        }
    }

    private void HandleJump()
    {
        if (!_isJumping &&  _characterController.isGrounded && _isJumpPressed)
        {
            if (_jumpCount<3 && _currentJumpResetRoutine != null)
            {
                StopCoroutine(_currentJumpResetRoutine);   
            }
            _animator.SetBool(_isJumpingHash,true);
            _isJumpAnimating = true;
            _isJumping = true;
            _jumpCount += 1;
            _animator.SetInteger(_jumpCountHash,_jumpCount);
            float previousVelocityY = _currentMovement.y;
            float newVelocityY = (_currentMovement.y + _initialJumpVelocity);
            float nextVelocityY = (previousVelocityY + newVelocityY) * .5f;
            _currentMovement.y = _initialJumpVelocities[_jumpCount] * .5f;
            _currentRunMovement.y = _initialJumpVelocities[_jumpCount] * .5f;
        }else if (_isJumping && _characterController.isGrounded && !_isJumpPressed)
        {
            _isJumping = false;
        }
    }

    IEnumerator JumpResetRoutine()
    {
        yield return new WaitForSeconds(.5f);
        _jumpCount = 0;
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
