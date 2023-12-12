using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //public Animator cameraAnim; 

    public bool playerCanMove;
    public bool isGetLantern;
    public bool isWalk;
    public bool isPlayerMove;

    [Header("변수")]
    public float moveSpeed = 2.8f; // 이동 속도 조절 변수
    public float sensitivity = 1.8f; // 마우스 회전 감도
    [SerializeField] private float headbobSpeed = 4f;
    [SerializeField] private float headbobAmount = 0.05f;

    [HideInInspector] public Rigidbody rb;
    private Camera playerCamera;
    AudioSource audioSource;

    private float rotationX;

    //headbob

    private float defaultYPos;
    private float timer;

    //[Header("퍼즐")]
    //public List<Transform> doorTransforms;
    //public List<GameObject> bloodTexts;

    //[HideInInspector] public bool isStartPuzzle;
    //int puzzleIndex;

    //public GameObject TriggerPuzzleClear_21;

    [Header("Quest07")]
    [HideInInspector] public bool isRotateToDiedSoldier;
    [HideInInspector] public bool isRotateToMonster;
    public Transform DiedSoldier;
    public Transform Monster_Mountain;
    float rotationSpeed = 1.7f;

    [Header("Quest12")]
    [HideInInspector] public bool isRotateToMonster_Village;
    public Transform Monster_Village;

    [Header("Quest16")]
    [HideInInspector] public bool isRotateToStoneWall;
    public Transform StoneWall;


    [Header("Tuto02")]
    [HideInInspector] public bool isRotateToDiedPeople_Tuto02;
    public Transform DiedPeople;

    [Header("Ending")]
    [HideInInspector] public bool isRotateToSky;
    public Transform sky;



    void Start()
    {
        playerCanMove = true;
        rb = GetComponent<Rigidbody>();
        playerCamera = GetComponentInChildren<Camera>();
        audioSource = GetComponent<AudioSource>();

        //퍼즐
        //Shuffle();
        //SetBloodTexts();

    }


    private void Update()
    {
        if (playerCanMove)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // 이동 방향 계산
            Vector3 moveDirection = new Vector3(horizontalInput, 0.0f, verticalInput).normalized;

            // 움직임 적용
            Vector3 moveVelocity = transform.TransformDirection(moveDirection) * moveSpeed;
            rb.velocity = moveVelocity;

            //마우스 회전 처리
            float mouseX = Input.GetAxis("Mouse X") * sensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

            rotationX -= mouseY;
            rotationX = Mathf.Clamp(rotationX, -90, 90); // 상하 각도 제한

            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, mouseX, 0);

            //if (rb.velocity.x != 0 || rb.velocity.z != 0)
            //    isPlayerMove = true;
            //else
            //    isPlayerMove = false;

            if (horizontalInput != 0 || verticalInput != 0)
                isPlayerMove = true;
            else
                isPlayerMove = false;

            if (isPlayerMove) //&& gameObject.name != "Player_Ending")
            {
                if (!audioSource.isPlaying)
                    audioSource.Play();
            }
            else
                audioSource.Stop();
        }
        else
            audioSource.Stop();

        if (isRotateToDiedPeople_Tuto02)
        {
            Vector3 targetDirection = DiedPeople.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1f * Time.deltaTime);
        }

        if (isRotateToDiedSoldier)
        {
            Vector3 targetDirection = DiedSoldier.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        }

        if (isRotateToMonster)
        {
            Vector3 targetDirection = Monster_Mountain.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        }

        if (isRotateToMonster_Village)
        {
            Vector3 targetDirection = Monster_Village.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        if (isRotateToStoneWall)
        {
            Vector3 targetDirection = StoneWall.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        if (isRotateToSky)
        {
            Vector3 targetDirection = sky.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.3f * Time.deltaTime);
        }
    }

    public void StartRotateToDiedSoldier()
    {
        isRotateToDiedSoldier = true;
    }

    public void StopRotateToDiedSoldier()
    {
        isRotateToDiedSoldier = false;
    }
    public void StartRotateToMonster_Mountain()
    {
        isRotateToMonster = true;
    }
    public void StopRotateToMonster_Mountain()
    {
        isRotateToMonster = false;
    }

    public void StartRotateToDiedPeople()
    {
        isRotateToDiedPeople_Tuto02 = true;
    }
    public void StopRotateToDiedPeople()
    {
        isRotateToDiedPeople_Tuto02 = false;
    }
    public void StartRotateToMonster_Village()
    {
        isRotateToMonster_Village = true;
    }
    public void StopRotateToMonster_Village()
    {
        isRotateToMonster_Village = false;
    }

    public void StartRotateToStoneWall()
    {
        isRotateToStoneWall = true;
    }
    public void StopRotateToStoneWall()
    {
        isRotateToStoneWall = false;
    }

    public void StartRotateToSky()
    {
        isRotateToSky = true;
    }







    //퍼즐
    // 게임 오브젝트 리스트 배열 섞기 : 실패했을 경우 텍스트의 위치를 재배치하기 위함
    //public void Shuffle()
    //{
    //    int n = bloodTexts.Count;
    //    while (n > 1)
    //    {
    //        n--;
    //        int k = Random.Range(0, n + 1);
    //        GameObject transValue = bloodTexts[k];
    //        bloodTexts[k] = bloodTexts[n];
    //        bloodTexts[n] = transValue;
    //    }
    //}

    // shuffle에서 섞인 텍스트 배열을 지정해놓은 위치에 배치하여 임의성 부여
    //public void SetBloodTexts()
    //{
    //    for (int i = 0; i < bloodTexts.Count; i++)
    //    {
    //        bloodTexts[i].transform.position = doorTransforms[i].position;
    //        bloodTexts[i].transform.rotation = doorTransforms[i].rotation;
    //    }
    //}



}
