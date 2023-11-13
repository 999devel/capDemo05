using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool playerCanMove;

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


    void Start()
    {
        playerCanMove = true;
        rb = GetComponent<Rigidbody>();
        playerCamera = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    private void OnEnable()
    {
        monster_eyes = GameObject.Find("monster_eyes");
    }

    void Update()
    {
        if (playerCanMove)
        {
            // WASD Ű �Է� �ޱ�
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // �̵� ���� ���
            Vector3 moveDirection = new Vector3(horizontalInput, 0.0f, verticalInput).normalized;

            // ������ ����
            Vector3 moveVelocity = transform.TransformDirection(moveDirection) * moveSpeed;
            rb.velocity = moveVelocity;

            if (monster_eyes_active_time < 1)
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
        }
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
