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
    public float moveSpeed = 5.0f; // 이동 속도 조절 변수
    public float sensitivity = 2.0f; // 마우스 회전 감도
    [SerializeField] private float headbobSpeed = 4f;
    [SerializeField] private float headbobAmount = 0.05f;

    private Rigidbody rb;
    private Camera playerCamera;
    AudioSource audioSource;

    private float rotationX;

    //headbob

    private float defaultYPos;
    private float timer;

    [Header("퍼즐")]
    public List<Transform> doorTransforms;
    public List<GameObject> bloodTexts;

    [HideInInspector] public bool isStartPuzzle;
    int puzzleIndex;

    public GameObject TriggerPuzzleClear_21;




    void Start()
    {
        playerCanMove = true;
        rb = GetComponent<Rigidbody>();
        playerCamera = GetComponentInChildren<Camera>();
        audioSource = GetComponent<AudioSource>();

        //퍼즐
        Shuffle();
        SetBloodTexts();

    }


    private void Update()
    {
        if (playerCanMove)
        {
            // WASD 키 입력 받기
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

            if (rb.velocity.x != 0 || rb.velocity.z != 0)
                isPlayerMove = true;
            else
                isPlayerMove = false;

            if (isPlayerMove)
            {
                if (!audioSource.isPlaying)
                    audioSource.Play();
            }
            else
                audioSource.Stop();
        }


        //Ray ray = new Ray(transform.position, transform.forward);
        //RaycastHit hitdata;

        //if(Physics.Raycast(ray, out hitdata, 50, layerMask))
        //{
        //    // 1
        //    if (puzzleIndex == 1)
        //    {
        //        if (hitdata.collider.gameObject.CompareTag("1"))
        //        {
        //            if (Input.GetKeyDown(KeyCode.Space))
        //            {
        //                puzzleIndex++;
        //                Debug.Log("성공");
        //            }

        //        }
        //        else
        //        {
        //            if (Input.GetKeyDown(KeyCode.Space))
        //            {
        //                Debug.Log("실패");

        //                puzzleIndex = 1;
        //                Shuffle();
        //                SetBloodTexts();
        //            }
        //        }
        //    }

        //    // 2
        //    if (puzzleIndex == 2)
        //    {
        //        if (hitdata.collider.gameObject.CompareTag("2"))
        //        {
        //            if (Input.GetKeyDown(KeyCode.Space))
        //            {
        //                puzzleIndex++;
        //                Debug.Log("성공");

        //            }

        //        }
        //        else
        //        {
        //            if (Input.GetKeyDown(KeyCode.Space))
        //            {
        //                Debug.Log("실패");

        //                puzzleIndex = 1;
        //                Shuffle();
        //                SetBloodTexts();
        //            }
        //        }
        //    }

        //    // 2
        //    if (puzzleIndex == 3)
        //    {
        //        if (hitdata.collider.gameObject.CompareTag("3"))
        //        {
        //            if (Input.GetKeyDown(KeyCode.Space))
        //            {
        //                puzzleIndex++;
        //                Debug.Log("성공");

        //            }

        //        }
        //        else
        //        {
        //            if (Input.GetKeyDown(KeyCode.Space))
        //            {
        //                Debug.Log("실패");

        //                puzzleIndex = 1;
        //                Shuffle();
        //                SetBloodTexts();
        //            }
        //        }
        //    }

        //    // 4
        //    if (puzzleIndex == 4)
        //    {
        //        if (hitdata.collider.gameObject.CompareTag("4"))
        //        {
        //            if (Input.GetKeyDown(KeyCode.Space))
        //            {
        //                puzzleIndex++;
        //                Debug.Log("성공");

        //            }

        //        }
        //        else
        //        {
        //            if (Input.GetKeyDown(KeyCode.Space))
        //            {
        //                Debug.Log("실패");
        //                puzzleIndex = 1;
        //                Shuffle();
        //                SetBloodTexts();
        //            }
        //        }
        //    }

        //    if (puzzleIndex == 5)
        //    {
        //        for (int i = 0; i < bloodTexts.Count; i++)
        //        {
        //            bloodTexts[i].SetActive(false);
        //        }

        //        TriggerPuzzleClear_21.SetActive(true);
        //        puzzleIndex++;

        //    }
        //}

            
    }

    //퍼즐
    // 게임 오브젝트 리스트 배열 섞기 : 실패했을 경우 텍스트의 위치를 재배치하기 위함
    public void Shuffle()
    {
        int n = bloodTexts.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            GameObject transValue = bloodTexts[k];
            bloodTexts[k] = bloodTexts[n];
            bloodTexts[n] = transValue;
        }
    }

    // shuffle에서 섞인 텍스트 배열을 지정해놓은 위치에 배치하여 임의성 부여
    public void SetBloodTexts()
    {
        for (int i = 0; i < bloodTexts.Count; i++)
        {
            bloodTexts[i].transform.position = doorTransforms[i].position;
            bloodTexts[i].transform.rotation = doorTransforms[i].rotation;
        }
    }



}
