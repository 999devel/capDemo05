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

    public Rigidbody rb;
    private Camera playerCamera;
    private float rotationX = 0.0f;

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

    //public void teleportHouse()
    //{
    //    player.transform.SetPositionAndRotation(outPoint_House.transform.position,
    //        outPoint_House.transform.rotation);
    //}

    void Start()
    { 
        rb = GetComponent<Rigidbody>();
        playerCamera = GetComponentInChildren<Camera>();
        //Cursor.lockState = CursorLockMode.Locked;
        
    }

    private void OnEnable()
    {
        monster_eyes = GameObject.Find("monster_eyes");
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

        if(monster_eyes_active_time < 1)
        {
            if (Time.timeScale != 0)
            {
                // ���콺 ȸ�� ó��
                float mouseX = Input.GetAxis("Mouse X") * sensitivity;
                float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

                rotationX -= mouseY;
                rotationX = Mathf.Clamp(rotationX, -90, 90); // ���� ���� ����

                playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
                transform.rotation *= Quaternion.Euler(0, mouseX, 0);
            }
        }
        else
        {
            monster_eyes_active_time += Time.deltaTime;
            if (monster_eyes_active_time > 3)
            {
                monster_eyes_active_time = 0;
                monster_eyes.SetActive(false);
                GameObject.Find("dialogue_caller").SetActive(true);
            }
        }

        // E Ű �Է� �ޱ�
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    // ��ȣ�ۿ��� �õ��� ���� ���
        //    Vector3 rayOrigin = playerCamera.transform.position;
        //    Vector3 rayDirection = playerCamera.transform.forward;

        //    RaycastHit hit;

        //    // ����ĳ��Ʈ�� ����Ͽ� ��ȣ�ۿ� ������ ������Ʈ �˻�
        //    if (Physics.Raycast(rayOrigin, rayDirection, out hit, interactionDistance))
        //    {
        //        // ��ȣ�ۿ� ������ ������Ʈ Ȯ��
        //        InteractableObject interactableObject = hit.collider.GetComponent<InteractableObject>();

        //        if (interactableObject != null)
        //        {
        //            // ��ȣ�ۿ� ������ ������Ʈ�� ��ȣ�ۿ�
        //            interactableObject.Interact();
        //        }
        //    }

        //}
    }

    //monster eyes�� �÷��̾� ȸ����Ű��
    private GameObject monster_eyes;
    private float monster_eyes_active_time = 0;

    public void look_monster_eyes()
    {
        monster_eyes_active_time = 1;
        Vector3 l_vector = monster_eyes.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(l_vector).normalized;
    }
}
