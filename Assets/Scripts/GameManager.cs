using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.AI;


public class GameManager : MonoBehaviour
{
    public GameObject Map;
    public GameObject PauseMenu;

    public PlayerController playerController;

    public GameObject playerTorch;
    public GameObject houseTorch;

    [Header("Spawn Point")]
    public Transform playerPos;
    public Transform outOfHousePoint;
    public Transform IntoHousePoint;
    public Transform outOfForestPoint;
    public Transform IntoForestPoint;

    [Header("Screen Fade")]
    public CanvasGroup canvasGroup;
    private Tween fadeTween;

    [Header("Door Animation")]
    [SerializeField] private Animator doorAnimator;
    //public bool doorOpenTrigger;

    [Header("Sound")]
    public AudioClip[] sfxlist;
    public AudioClip impressedBG;
    [HideInInspector]public AudioClip sfxSound;

    [Header("Puzzle")]
    public BoxCollider[] boxCollider_QuestDoors;
    public BoxCollider[] block_QuestDoors;

    [Header("Soldier Controller")]
    public NavMeshAgent soldierNav;
    public Animator soldierAnim;
    public List<Transform> SoldierWaypoint = new List<Transform>();
    int SoldierIndex;
    Coroutine coWaypoint;
    public GameObject Soldier_Faded;
    public GameObject Soldier_AtEntrance;

    [Header("07Watching Monster In the Mountain")]
    public GameObject Player;
    public GameObject Soldier_Ragdoll;
    public GameObject monster_InTheMountain;

    [Header("Monster Controller")]
    public NavMeshAgent monsterNav;
    public Animator monsterAnim;
    public List<Transform> monsterWayPoint = new List<Transform>();
    int monsterIndex;
    Coroutine coMonsterRun;
    public GameObject monster;

    [Header("Flash Scarecrow")]
    public GameObject flashed_Scarecrow;

    [Header("Death Scean")]
    public GameObject playerCamera;
    public GameObject deathSceneCamera;
    public GameObject deathSceanMonster;
    public GameObject blackBackground;

    [Header("Quest")]
    public GameObject Quest01;
    public GameObject Quest07;
    public GameObject Quest07Block;
    public GameObject Quest07DiedSoldier;
    public GameObject Quest08ConversationTrigger;





    private void Start()
    {
        StartCoroutine(Beginning());
    }

    private void Update()
    {
        if(Map.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                Map.SetActive(false);
                UnBindPlayerMoving();
            }
        }
        else if(Map.activeSelf == false)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                Map.SetActive(true);
                BindPlayerMoving();
            }
        }

        if (PauseMenu.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseMenu.SetActive(false);
                UnBindPlayerMoving();
            }
        }
        else if (PauseMenu.activeSelf == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseMenu.SetActive(true);
                BindPlayerMoving();
            }
        }
    }

    private void Fade(float endValue, float duration)
    {
        if (fadeTween != null)
        {
            fadeTween.Kill(false);
        }

        fadeTween = canvasGroup.DOFade(endValue, duration);
    }

    public void FadeIn(float duration)
    {
        Fade(1f, duration);
    }

    public void FadeOut(float duration)
    {
        Fade(0f, duration);
    }

    IEnumerator Beginning()
    {
        playerController.playerCanMove = false;
        FadeOut(2.5f);
        yield return new WaitForSeconds(2.5f);
        playerController.playerCanMove = true;
    }

    IEnumerator VillageToHouse()
    {
        playerController.playerCanMove = false;
        FadeIn(2.5f);
        yield return new WaitForSeconds(2.5f);

        playerPos.position = IntoHousePoint.position;
        playerPos.rotation = IntoHousePoint.rotation;
        FadeOut(2.5f);
        yield return new WaitForSeconds(2.5f);
        playerController.playerCanMove = true;

    }

    IEnumerator HouseToVillage()
    {
        playerController.playerCanMove = false;
        FadeIn(2.5f);
        yield return new WaitForSeconds(2.5f);
        Time.timeScale = 1f;

        playerPos.position = outOfHousePoint.position;
        playerPos.rotation = outOfHousePoint.rotation;
        FadeOut(2.5f);
        yield return new WaitForSeconds(2.5f);
        playerController.playerCanMove = true;
    }

    IEnumerator VillageToForest()
    {
        playerController.playerCanMove = false;

        FadeIn(2.5f);
        yield return new WaitForSeconds(2.5f);
        Time.timeScale = 1f;

        playerPos.position = IntoForestPoint.position;
        playerPos.rotation = IntoForestPoint.rotation;
        FadeOut(2.5f);
        yield return new WaitForSeconds(2.5f);
        playerController.playerCanMove = true;
    }

    IEnumerator ForestToVillage()
    {
        playerController.playerCanMove = false;

        FadeIn(2.5f);
        yield return new WaitForSeconds(2.5f);

        playerPos.position = outOfForestPoint.position;
        playerPos.rotation = outOfForestPoint.rotation;
        FadeOut(2.5f);
        yield return new WaitForSeconds(2.5f);
        playerController.playerCanMove = true;
    }

    public void MovePlayerVillageToHouse()
    {
        playerTorch.SetActive(false);
        houseTorch.SetActive(true);
        StartCoroutine(VillageToHouse());
    }

    public void MovePlayerHouseToVillage()
    {
        playerTorch.SetActive(true);
        houseTorch.SetActive(false);
        StartCoroutine(HouseToVillage());
    }
    public void MovePlayerVillageToForest()
    {
        StartCoroutine(VillageToForest());
    }

    public void MovePlayerForestToVillage()
    {
        StartCoroutine(ForestToVillage());
    }

    public void test_playerToHousePoint()
    {
        playerPos.position = outOfHousePoint.position;
        playerPos.rotation = outOfHousePoint.rotation;
    }

    public void DoorOpening()
    {
        doorAnimator.SetTrigger("DoorOpening");
    }

    //Sound
    public void FindCorrectSFXSound(string sfxName)
    {
        for (int i = 0; i < sfxlist.Length; i++)
        {
            if (sfxlist[i].name == sfxName)
            {
                sfxSound = sfxlist[i];
            }
        }
    }

    public void SFXSound_CheckingDoor()
    {
        FindCorrectSFXSound("CheckingDoor");
        SoundManager.instance.SFXPlayer("CheckingDoor", sfxSound);
    }

    public void SFXSound_OpeningDoor()
    {
        FindCorrectSFXSound("OpeningDoor");
        SoundManager.instance.SFXPlayer("OpeningDoor", sfxSound);
    }

    public void SFXSound_ScarecrowWalking()
    {
        FindCorrectSFXSound("ScarecrowWalking");
        SoundManager.instance.SFXPlayer("ScarecrowWalking", sfxSound);
    }

    public void SFXSound_Scream()
    {
        FindCorrectSFXSound("Scream");
        SoundManager.instance.SFXPlayer("Scream", sfxSound);
    }

    public void SFXSound_FallingSoldier()
    {
        FindCorrectSFXSound("FallingSoldier");
        SoundManager.instance.SFXPlayer("FallingSoldier", sfxSound);
    }

    public void SFXSound_RunningMonster()
    {
        FindCorrectSFXSound("RunningMonster");
        SoundManager.instance.SFXPlayer("RunningMonster", sfxSound);
    }

    public void SFXSound_Knock()
    {
        FindCorrectSFXSound("Knock");
        SoundManager.instance.SFXPlayer("Knock", sfxSound);
    }

    public void SFXSound_FlashScarecrow()
    {
        FindCorrectSFXSound("FlashScarecrow");
        SoundManager.instance.SFXPlayer("FlashScarecrow", sfxSound);
    }


    public void BGSound_ImpressedBG()
    {
        SoundManager.instance.BgSoundPlay(impressedBG);
    }

    public void BindPlayerMoving()
    {
        playerController.playerCanMove = false;     // ui 조작 중 플레이어의 화면 회전을 막기 위함
    }

    public void UnBindPlayerMoving()
    {
        playerController.playerCanMove = true;
    }

    public void TurnOnCollider_QuestDoors()
    {
        for (int i = 0; i < boxCollider_QuestDoors.Length; i++)
        {
            boxCollider_QuestDoors[i].enabled = true;
        }
    }

    public void TurnOffCollider_QuestDoors()
    {
        for (int i = 0; i < boxCollider_QuestDoors.Length; i++)
        {
            boxCollider_QuestDoors[i].enabled = false;
        }
    }

    public void TurnOnBlock_QuestDoors()
    {
        for (int i = 0; i < block_QuestDoors.Length; i++)
        {
            block_QuestDoors[i].enabled = true;
        }
    }
    public void TurnOffBlock_QuestDoors()
    {
        for (int i = 0; i < block_QuestDoors.Length; i++)
        {
            block_QuestDoors[i].enabled = false;
        }
    }


    //01대화 트리거
    public void TriggerConversation01ntoHouse()
    {
        StartCoroutine(coTriggerConveersation01IntoHouse());
    }

    IEnumerator coTriggerConveersation01IntoHouse()
    {
        yield return new WaitForSeconds(2.1f);
        Quest01.SetActive(true);
    }

    // 03포졸 > 03포졸입구 까지의 움직임
    #region
    public void SoliderGoEntranceOfVillage()
    {
        coWaypoint = StartCoroutine(CoroutineSoliderGoEntranceOfVillage());
    }


    IEnumerator CoroutineSoliderGoEntranceOfVillage()
    {
        yield return new WaitForSeconds(0.7f);
        while (true)
        {
            yield return new WaitForSeconds(0.3f);

            soldierAnim.SetTrigger("isWalk");
            soldierNav.destination = SoldierWaypoint[SoldierIndex].position;

            if (!soldierNav.pathPending && soldierNav.remainingDistance < 1f)
                Soldier_GotoNext(); //목적지까지의 거리가 1이하거나 도착했으면 함수실행
            if (SoldierIndex == SoldierWaypoint.Count)
            {
                Soldier_Faded.SetActive(false);
                Soldier_AtEntrance.SetActive(true);
                StopCoroutine(coWaypoint);
            }
        }
    }
    void Soldier_GotoNext()
    {
        soldierNav.destination = SoldierWaypoint[SoldierIndex].position;
        SoldierIndex = (SoldierIndex + 1);
    }
    #endregion

    // 06몬스터
    #region

    public void Action06()
    {
        StartCoroutine(coAction06());
    }

    

    IEnumerator coAction06()
    {
        yield return new WaitForSeconds(0.7f);
        BindPlayerMoving();
        yield return new WaitForSeconds(1.2f);
        playerController.StartRotateToDiedSoldier();
        yield return new WaitForSeconds(3f);
        playerController.StopRotateToDiedSoldier();
        monster_InTheMountain.SetActive(true);
        playerController.StartRotateToMonster_Mountain();
        yield return new WaitForSeconds(6f);
        playerController.StopRotateToMonster_Mountain();
        Player.transform.DOLocalRotate(new Vector3(0, 0, 0), 2f);
        UnBindPlayerMoving();
        Quest07.SetActive(true);
        Quest07Block.SetActive(true);
    }

    #endregion

    // 07시신정리
    public void Action07()
    {
        StartCoroutine(coAcition07());
    }

    IEnumerator coAcition07()
    {
        BindPlayerMoving();
        FadeIn(2f);
        yield return new WaitForSeconds(2.1f);
        Soldier_Ragdoll.SetActive(false);
        Quest07DiedSoldier.SetActive(true);
        FadeOut(2f);
        UnBindPlayerMoving();
    }

    //08 옷 태우기
    public void Action08_FireClothes()
    {
        StartCoroutine(coAction08_FireClothes());
    }

    IEnumerator coAction08_FireClothes()
    {
        BindPlayerMoving();
        FadeIn(2f);
        yield return new WaitForSeconds(2.1f);
        FadeOut(2f);
        yield return new WaitForSeconds(2.1f);
        UnBindPlayerMoving();
        Quest08ConversationTrigger.SetActive(true);
    }


    // 18몬스터 삼거리에 잠깐 비춰지는 움직임
    #region
    public void MonsterSetting()
    {
        coMonsterRun = StartCoroutine(MonsterRun());
    }


    IEnumerator MonsterRun()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.3f);

            //monsterAnim.SetTrigger("isRun");
            monsterNav.destination = monsterWayPoint[monsterIndex].position;

            if (!monsterNav.pathPending && monsterNav.remainingDistance < 1f)
                Monster_GotoNext(); //목적지까지의 거리가 1이하거나 도착했으면 함수실행
            if (monsterIndex == monsterWayPoint.Count)
            {
                monster.SetActive(false);
                StopCoroutine(coMonsterRun);
            }
        }
    }
    void Monster_GotoNext()
    {
        monsterNav.destination = monsterWayPoint[monsterIndex].position;
        monsterIndex = (monsterIndex + 1);
    }
    #endregion

    public void ClosePauseMenu()
    {
        PauseMenu.SetActive(false);
        UnBindPlayerMoving();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // 15플래쉬 허수아비
    #region
    public void FlashScarecrow()
    {
        StartCoroutine(coFlashScarecrow());
    }

    IEnumerator coFlashScarecrow()
    {
        yield return new WaitForSeconds(1.7f);
        flashed_Scarecrow.SetActive(true);
        yield return new WaitForSeconds(1.2f);
        flashed_Scarecrow.SetActive(false);
    }
    #endregion

    //퍼즐 클리어 이후 공격받는 신
    #region
    public void LoadDeathScene()
    {
        StartCoroutine(coLoadDeathScene());
    }

    IEnumerator coLoadDeathScene()
    {
        yield return new WaitForSeconds(3.5f);
        playerCamera.SetActive(false);
        deathSceneCamera.SetActive(true);
        deathSceanMonster.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        blackBackground.SetActive(true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Ending");
    }
    #endregion

}
