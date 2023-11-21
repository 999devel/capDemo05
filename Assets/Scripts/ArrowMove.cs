using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMove : MonoBehaviour
{
    [Header("������Ʈ")]
    [SerializeField] private GameObject player;
    private Camera playerCamera;
    public GameObject arrowGroup;

    [Header("��ġ ����")]
    public Transform playerPos;
    public Transform lookAtPlayerHousePos;
    public Transform lookAtStartPointPos;
    public Transform lookAtFieldPos;

    [HideInInspector] public bool isMoveDirectionToPlayerHouse;
    [HideInInspector] public bool isMoveDirectionToStartPoint;
    [HideInInspector] public bool isMoveDirectionToField;
    [HideInInspector] public bool isPlayerMove;

    [Header("��ġ ����")]
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
        // ��� ������ ������������ ������ �ٶ� ���� �������� ��
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

    // playerHouse �������� �̵� ��
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
    
    // startPoint �������� �̵� ��
    private void DirectionToStartPoint()
    {
        playerPos.position = Vector3.MoveTowards(playerPos.position, lookAtStartPointPos.position, moveSpeed);

        if (playerPos.position == lookAtStartPointPos.position)
        {
            isMoveDirectionToStartPoint = false;
            isPlayerMove = false;
        }
    }

    // ��� �������� �̵� ��
    private void DirectionToField()
    {
        playerPos.position = Vector3.MoveTowards(playerPos.position, lookAtFieldPos.position, moveSpeed);

        if(playerPos.position == lookAtFieldPos.position)
        {
            isMoveDirectionToField = false;
            isPlayerMove = false;
        }
    }


    // �Ʒ� üũ�� ȭ��ǥ�� Ŭ������ �� �̺�Ʈ�� �ۼ�
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

    // ȭ��ǥ ��ȣ�ۿ� �� ����ȭ
    public void ArrowClear()
    {
        gameObject.layer = LayerMask.NameToLayer("Invisible");
    }

    public void ArrowOpaque()
    {
        gameObject.layer = LayerMask.NameToLayer("DirectionArrow");
    }
}
