using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Profiling.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    public Slider creamSlider;
    
    private CharacterController controller;
    private Vector3 playerVelocity;
    public Vector3 jumpingForce;
    private bool isGrounded;
    public float playerSpeed = 2.0f;
    public float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;

    public int creamRemain = 1; // 남은 생크림 수
    public int creamCapacity = 5; // 생크림 용량
    public bool isCreamAvailable = false;

    public int fillCreamIndexI;
    public int fillCreamIndexJ;

    private void OnEnable()
    {
        creamRemain = 0;
        creamSlider.value = creamRemain;
    }

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        /*
        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Debug.Log("try to Jump");
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            isGrounded = false;
        }*/
        // destroy 된 케이크 리스트 내에 존재하면
        if (MapManager.Instance.destroyedAreaList != null)
            isCreamAvailable = getAvalableCream();
        // if (isCreamAvailable) 공격 가능 표시 UI? 추가  
        if (Input.GetButtonDown("Jump") && isCreamAvailable)
            tryFillEmptyCake();
        

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if (transform.position.y < -5)
            GameManager.Instance.GameOver();
    }

    public void tryFillEmptyCake()
    {
        Debug.Log("tryFill");
        Debug.Log(creamRemain);
        if (creamRemain <= 0) return;
        if (fillCreamIndexI == -1) return;
        // 케이크 채우기! I값이 -1이면 리턴, 멀어져서 fillCream불가능
        // cakeState가 destroyed일 때 채워넣을 수 있음
        if (MapManager.Instance.mapInfo[fillCreamIndexI, fillCreamIndexJ].cube.cubeState 
            == Define.CubeState.Destroyed)
        {
            Debug.Log("tryFill Destroyed일때만");
            MapManager.Instance.RefillCake(fillCreamIndexI, fillCreamIndexJ);
            creamRemain -= 1;
            creamSlider.value = creamRemain;
        }
    }

    
    public bool getAvalableCream()
    {
        // 캐릭터와 케이크 사이 길이 측정, 생크림 채우기 가능한 거리 계산,
        for (int i = 0; i < MapManager.Instance.destroyedAreaList.Count; i++)
        {
            Vector3 cubePosition = MapManager.Instance.destroyedAreaList[i].cube.transform.position;
            float dist = 3f;
            if ((cubePosition - this.transform.position).sqrMagnitude < dist * dist)
            {
                fillCreamIndexI = MapManager.Instance.destroyedAreaList[i].rowIndex;
                fillCreamIndexJ = MapManager.Instance.destroyedAreaList[i].columnIndex;

                //Debug.Log("row : " +MapManager.Instance.destrotyedAreaList[i].rowIndex +"col : " +
                  //  MapManager.Instance.destrotyedAreaList[i].columnIndex);
                return true;
            }
        }
        fillCreamIndexI = -1;
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            Destroy(other.gameObject);
            GetCream();
            Debug.Log(creamRemain);
        }

        if (other.gameObject.layer == 11)
        {
            Debug.Log("포크에 맞음 사망..");
            GameManager.Instance.GameOver();
        }
    }

    public void GetCream()
    {
        // 생크림 아이템 접촉 시 creamRemain 증가, creamRemain은 용량을 넘길 수 없음.
        if (creamRemain >= 0 && creamRemain <creamCapacity)
        {
            // 접촉한 오브젝트 Destroy
            creamRemain += 1;
            creamSlider.value = creamRemain;
        }
    }

}
