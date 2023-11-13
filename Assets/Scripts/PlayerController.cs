using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("변수")]
    public float moveSpeed = 5.0f; // 이동 속도 조절 변수
    public float sensitivity = 2.0f; // 마우스 회전 감도
    public float interactionDistance = 5.0f; // 상호작용 거리

    [HideInInspector] public int dialogueNum = 0;
    [HideInInspector] public bool isWalk = true;   // 플레이어가 움직일 때 true, 가만히 있으면 false // 이후에 false로 초기값 수정하고 주석 삭제
    [HideInInspector] public bool isGetLantern = true;

    public Rigidbody rb;
    private Camera playerCamera;
    private float rotationX = 0.0f;

    //private void Awake()
    //{
    //    // 씬 이동 시 player 오브젝트 유지(village <-> house 이동 시 위치 저장 위함)
    //    DontDestroyOnLoad(gameObject);

    //    // player 오브젝트 중복 생성 방지
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
        // WASD 키 입력 받기
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // 이동 방향 계산
        Vector3 moveDirection = new Vector3(horizontalInput, 0.0f, verticalInput).normalized;

        // 움직임 적용
        Vector3 moveVelocity = transform.TransformDirection(moveDirection) * moveSpeed;
        rb.velocity = moveVelocity;

        if(monster_eyes_active_time < 1)
        {
            if (Time.timeScale != 0)
            {
                // 마우스 회전 처리
                float mouseX = Input.GetAxis("Mouse X") * sensitivity;
                float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

                rotationX -= mouseY;
                rotationX = Mathf.Clamp(rotationX, -90, 90); // 상하 각도 제한

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

        // E 키 입력 받기
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    // 상호작용을 시도할 방향 계산
        //    Vector3 rayOrigin = playerCamera.transform.position;
        //    Vector3 rayDirection = playerCamera.transform.forward;

        //    RaycastHit hit;

        //    // 레이캐스트를 사용하여 상호작용 가능한 오브젝트 검색
        //    if (Physics.Raycast(rayOrigin, rayDirection, out hit, interactionDistance))
        //    {
        //        // 상호작용 가능한 오브젝트 확인
        //        InteractableObject interactableObject = hit.collider.GetComponent<InteractableObject>();

        //        if (interactableObject != null)
        //        {
        //            // 상호작용 가능한 오브젝트와 상호작용
        //            interactableObject.Interact();
        //        }
        //    }

        //}
    }

    //monster eyes로 플레이어 회전시키기
    private GameObject monster_eyes;
    private float monster_eyes_active_time = 0;

    public void look_monster_eyes()
    {
        monster_eyes_active_time = 1;
        Vector3 l_vector = monster_eyes.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(l_vector).normalized;
    }
}
