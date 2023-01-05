using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using DG.Tweening;
public class Controller : MonoBehaviour
{
    private CharacterController _characterController;
    private Animator _playerAnimator;
    private Vector3 _playerVelocity;
    [SerializeField] private bool _groundedPlayer;
    [SerializeField] private float _playerSpeed = 2.0f;
    private float _jumpHeight = 1.0f;
    private float _gravityValue = -9.81f;
    [SerializeField] private float strawberryDist= 3f;
    public Transform StrawberryTransform;
    public Transform RightHand;
    public bool Ray;
    private Ray _ray;
    public LayerMask LayerMask;
    public RaycastHit hit;

    
    private void Start()
    {
        _characterController = gameObject.GetComponent<CharacterController>();
        _playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        _groundedPlayer = _characterController.isGrounded;
        _ray = new Ray(transform.position + Vector3.up, transform.forward);
        Ray = Physics.Raycast(_ray, out hit, 5f, LayerMask);
        
        if (_groundedPlayer && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        _characterController.Move(move * Time.deltaTime * _playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && _groundedPlayer)
        {
            _playerVelocity.y += Mathf.Sqrt(_jumpHeight * -3.0f * _gravityValue);
        }

        _playerVelocity.y += _gravityValue * Time.deltaTime;
        _characterController.Move(_playerVelocity * Time.deltaTime);
        if (Input.GetAxis("Horizontal") != 0 || (Input.GetAxis("Vertical"))!= 0 )
        {
            _playerAnimator.SetBool("walk",true);
        }
        else
        {
            _playerAnimator.SetBool("walk",false);
        }

        if (Input.GetKey("space") && (transform.position - StrawberryTransform.position).sqrMagnitude < strawberryDist
            && Ray) // 조건 추가 : 일정 거리안에 있어야하고 player가 딸기를 보고 있어야 한다. 
        {
            GetStrawberry();
        }

        DrawRayLine();
    }
    [ContextMenu("getStrawberry")]
    private void GetStrawberry()
    {
        _playerAnimator.SetTrigger("getStrawberry");
        StrawberryTransform.DOScale(Vector3.one * 0.1f, 4.7f);
        StrawberryTransform.SetParent(RightHand);
        StrawberryTransform.transform.localPosition =
            Vector3.Lerp(StrawberryTransform.localPosition, Vector3.zero, 2.7f);
    }
    public void DrawRayLine()
    {
        if (hit.collider != null)
        {
            Debug.DrawRay(transform.position +Vector3.up,transform.forward * hit.distance,Color.red);
        }
        else
        {
            Debug.DrawRay(transform.position +Vector3.up,transform.forward * 5, Color.red);
        }
    }
}
