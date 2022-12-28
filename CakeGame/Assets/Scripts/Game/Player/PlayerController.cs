using System.Collections;
using System.Collections.Generic;
using Unity.Profiling.LowLevel.Unsafe;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
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
        if (MapManager.Instance.destrotyedAreaList != null)
            isCreamAvailable = getAvalableCream();
        // if (isCreamAvailable) 공격 가능 표시 UI? 추가  
        if (Input.GetButtonDown("Jump") && isCreamAvailable)
            tryFillEmptyCake();
        

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void tryFillEmptyCake()
    {
        Debug.Log("tryFill");
        if (creamRemain <= 0) return;
        if (fillCreamIndexI == -1) return;
        // 케이크 채우기! I값이 -1이면 리턴, 멀어져서 fillCream불가능

        MapManager.Instance.mapInfo[fillCreamIndexI, fillCreamIndexJ].cube.parent.SetActive(true);

        
    }


    public void GetCream()
    {
        
    }

    public bool getAvalableCream()
    {
        // 캐릭터와 케이크 사이 길이 측정, 생크림 채우기 가능한 거리 계산,
        for (int i = 0; i < MapManager.Instance.destrotyedAreaList.Count; i++)
        {
            Vector3 cubePosition = MapManager.Instance.destrotyedAreaList[i].cube.transform.position;
            float dist = 3f;
            if ((cubePosition - this.transform.position).sqrMagnitude < dist * dist)
            {
                fillCreamIndexI = MapManager.Instance.destrotyedAreaList[i].rowIndex;
                fillCreamIndexJ = MapManager.Instance.destrotyedAreaList[i].columnIndex;

                //Debug.Log("row : " +MapManager.Instance.destrotyedAreaList[i].rowIndex +"col : " +
                  //  MapManager.Instance.destrotyedAreaList[i].columnIndex);
                return true;
            }
        }
        fillCreamIndexI = -1;
        return false;
    }
}
