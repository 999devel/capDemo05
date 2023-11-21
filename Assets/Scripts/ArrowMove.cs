using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMove : MonoBehaviour
{
    [Header("오브젝트")]
    [SerializeField] private GameObject player;
    private Camera playerCamera;
    public GameObject arrowGroup;

    [Header("위치 정보")]
    public Transform playerPos;
    public Transform lookAtPlayerHousePos;
    public Transform lookAtStartPointPos;
    public Transform lookAtFieldPos;

    [HideInInspector] public bool isMoveDirectionToPlayerHouse;
    [HideInInspector] public bool isMoveDirectionToStartPoint;
    [HideInInspector] public bool isMoveDirectionToField;
    [HideInInspector] public bool isPlayerMove;

    [Header("수치 변수")]
    [SerializeField] private float moveSpeed = 0.01f;
    [SerializeField] private float headbobSpeed = 4f;
    [SerializeField] private float headbobAmount = 0.05f;

    private float defaultYPos;
    private float timer;


    private void Awake()
    {
        playerCamera = player.GetComponentInChildren<Camera>();
        defaultYPos = playerCamera.transform.localPosition.y;

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // 모든 방향은 시작지점에서 마을을 바라본 것을 기준으로 함
        if (isPlayerMove)
        {
            HandleHeadbob();
        }

        if (isMoveDirectionToPlayerHouse)
        {
            DirectionToPlayerHouse();
        }

        if (isMoveDirectionToStartPoint)
        {
            DirectionToStartPoint();
        }

        
    }

    private void HandleHeadbob()
    {
        timer += Time.deltaTime * headbobSpeed;

        playerCamera.transform.localPosition = new Vector3(
            playerCamera.transform.localPosition.x,
            defaultYPos + Mathf.Sin(timer) * headbobAmount,
            playerCamera.transform.localPosition.z);
    }

    // playerHouse 방향으로 이동 시
    private void DirectionToPlayerHouse()
    {
        playerPos.position = Vector3.MoveTowards(playerPos.position, lookAtPlayerHousePos.position, moveSpeed);

        if (playerPos.position == lookAtPlayerHousePos.position)
        {
            isMoveDirectionToPlayerHouse = false;
            isPlayerMove = false;
            ArrowOpaque();
            arrowGroup.SetActive(false);
        }
    }
    
    // startPoint 방향으로 이동 시
    private void DirectionToStartPoint()
    {
        playerPos.position = Vector3.MoveTowards(playerPos.position, lookAtStartPointPos.position, moveSpeed);

        if (playerPos.position == lookAtStartPointPos.position)
        {
            isMoveDirectionToStartPoint = false;
            isPlayerMove = false;
        }
    }

    // 논밭 방향으로 이동 시
    private void DirectionToField()
    {
        playerPos.position = Vector3.MoveTowards(playerPos.position, lookAtFieldPos.position, moveSpeed);

        if(playerPos.position == lookAtFieldPos.position)
        {
            isMoveDirectionToField = false;
            isPlayerMove = false;
        }
    }


    // 아래 체크는 화살표를 클릭했을 때 이벤트로 작성
    public void CheckPlayerMove()
    {
        isPlayerMove = true;
    }

    public void LookAtPlayerHouseOnMove()
    {
        isMoveDirectionToPlayerHouse = true;
    }

    public void LookAtStartPointOnMove()
    {
        isMoveDirectionToStartPoint = true;
    }

    public void LookAtFieldOnMove()
    {
        isMoveDirectionToField = true;
    }

    // 화살표 상호작용 시 투명화
    public void ArrowClear()
    {
        gameObject.layer = LayerMask.NameToLayer("Invisible");
    }

    public void ArrowOpaque()
    {
        gameObject.layer = LayerMask.NameToLayer("DirectionArrow");
    }
}
