using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("����")]
    public float moveSpeed = 5.0f; // �̵� �ӵ� ���� ����
    public float sensitivity = 2.0f; // ���콺 ȸ�� ����
    public float interactionDistance = 5.0f; // ��ȣ�ۿ� �Ÿ�

    [HideInInspector] public int dialogueNum = 0;
    [HideInInspector] public bool isWalk = true;   // �÷��̾ ������ �� true, ������ ������ false // ���Ŀ� false�� �ʱⰪ �����ϰ� �ּ� ����
    [HideInInspector] public bool isGetLantern = true;

    private Rigidbody rb;
    private Camera playerCamera;
    private float rotationX = 0.0f;
    private GameObject player;
    private GameObject outPoint_House;

    //private void Awake()
    //{
    //    // �� �̵� �� player ������Ʈ ����(village <-> house �̵� �� ��ġ ���� ����)
    //    DontDestroyOnLoad(gameObject);

    //    // player ������Ʈ �ߺ� ���� ����
    //    var obj = FindObjectsOfType<PlayerController>();
    //    if (obj.Length == 1)
    //    {
    //        DontDestroyOnLoad(gameObject);
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    public void teleportHouse()
    {
        player.transform.SetPositionAndRotation(outPoint_House.transform.position,
            outPoint_House.transform.rotation);
    }

    void Start()
    { 
        rb = GetComponent<Rigidbody>();
        playerCamera = GetComponentInChildren<Camera>();
        player = GameObject.FindWithTag("Player");
        outPoint_House = GameObject.Find("outPoint_House");
        //Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // WASD Ű �Է� �ޱ�
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // �̵� ���� ���
        Vector3 moveDirection = new Vector3(horizontalInput, 0.0f, verticalInput).normalized;

        // ������ ����
        Vector3 moveVelocity = transform.TransformDirection(moveDirection) * moveSpeed;
        rb.velocity = moveVelocity;

        
        if(Time.timeScale != 0)
        {
            // ���콺 ȸ�� ó��
            float mouseX = Input.GetAxis("Mouse X") * sensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

            rotationX -= mouseY;
            rotationX = Mathf.Clamp(rotationX, -90, 90); // ���� ���� ����

            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, mouseX, 0);
        }

        // E Ű �Է� �ޱ�
        if (Input.GetKeyDown(KeyCode.E))
        {
            // ��ȣ�ۿ��� �õ��� ���� ���
            Vector3 rayOrigin = playerCamera.transform.position;
            Vector3 rayDirection = playerCamera.transform.forward;

            RaycastHit hit;

            // ����ĳ��Ʈ�� ����Ͽ� ��ȣ�ۿ� ������ ������Ʈ �˻�
            if (Physics.Raycast(rayOrigin, rayDirection, out hit, interactionDistance))
            {
                // ��ȣ�ۿ� ������ ������Ʈ Ȯ��
                InteractableObject interactableObject = hit.collider.GetComponent<InteractableObject>();

                if (interactableObject != null)
                {
                    // ��ȣ�ۿ� ������ ������Ʈ�� ��ȣ�ۿ�
                    interactableObject.Interact();
                }

                if (interactableObject.gameObject.CompareTag("located_Lantern"))    // �÷��̾ ������ �����鼭 ����� ��� Light�� ������ ���� �޼�
                {
                    isGetLantern = true;
                    //located_Lantern.setActive(false); //���� ������Ʈ ��Ȱ��ȭ
                    //player_get_Lantern.setActive(true); // �÷��̾ �� ���� ������Ʈ Ȱ��ȭ
                    //located_area_Lantern.setActive(true); // ������ �ٽ� ���� �� �ִ� �ڸ� Ȱ��ȭ
                }

                if (interactableObject.gameObject.CompareTag("located_area_Lantern"))    
                {
                    isGetLantern = false;
                    //located_Lantern.setActive(true); //���� ������Ʈ Ȱ��ȭ
                    //player_get_Lantern.setActive(false); // �÷��̾ �� ���� ������Ʈ ��Ȱ��ȭ
                    //located_area_Lantern.setActive(false); // ������ �ٽ� ���� �� �ִ� �ڸ� ��Ȱ��ȭ
                }
            }

        }
    }


}
