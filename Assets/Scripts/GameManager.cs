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
    public AudioClip VillageBG;
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

    [Header("05 Box")]
    public GameObject getBox;
    public GameObject Quest05Conversation;
    public GameObject Quest05Conversation02;
    public GameObject Quest06Box_Completed;

    [Header("07Watching Monster In the Mountain")]
    public GameObject Player;
    public GameObject Soldier_Ragdoll;
    public GameObject monster_InTheMountain;

    [Header("11 Scream")]
    public GameObject Scream_Normal;
    public GameObject Scream_FlipNeck;
    public GameObject Scream_Run;
    public GameObject Scream_Face;
    public GameObject After_Scream_Conversation;
    public NavMeshAgent screamNav_Run;
    public List<Transform> screamWayPoint = new List<Transform>();
    int screamIndex;
    Coroutine coScreamRun;

    [Header("12 Monster")]
    public NavMeshAgent monsterNav;
    public List<Transform> monsterWayPoint = new List<Transform>();
    int monsterIndex;
    Coroutine coMonsterRun;
    public GameObject monster;
    public GameObject Conversation12;

    [Header("16 Monster")]
    public GameObject playerBody;
    public NavMeshAgent ChaseMonsterNav;
    public List<Transform> ChaseMonsterWayPoint = new List<Transform>();
    int ChaseMonsterIndex;
    Coroutine coMonsterChase;
    public GameObject ChaseMonster;


    [Header("Flash Scarecrow")]
    public GameObject flashed_Scarecrow;
    public GameObject Quest11afterFlashScarecrowBox;

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
    public GameObject Quest08BigFire;
    public GameObject Quest08ConversationTrigger;
    public GameObject Quest09Conversation;
    //public GameObject Quest15MidiumFire01;
    //public GameObject Quest15MidiumFire02;
    //public GameObject Quest15MidiumFire03;
    //public GameObject Quest15MidiumFire04;
    //public GameObject Quest15Conversation01;
    //public GameObject Quest15Conversation02;
    //public GameObject Quest15Conversation03;
    //public GameObject Quest15Conversation04;
    public GameObject Quest14Conversation;
    public GameObject Quest15ConversationGoLastBox;
    public GameObject Quest16conversation;

    [Header("Ending")]
    public GameObject EndingUITrigger;
    public GameObject FireGroup;
    



    private void Start()
    {
        StartCoroutine(Beginning());
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().name == "Village")
        {
            if (Map.activeSelf == true)
            {
                if (Input.GetKeyDown(KeyCode.M))
                {
                    Map.SetActive(false);
                    UnBindPlayerMoving();
                }
            }
            else if (Map.activeSelf == false)
            {
                if (Input.GetKeyDown(KeyCode.M))
                {
                    Map.SetActive(true);
                    BindPlayerMoving();
                }
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

    public void SFXSound_Impact()
    {
        FindCorrectSFXSound("Impact");
        SoundManager.instance.SFXPlayer("Impact", sfxSound);
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

    public void SFXSound_BigFire()
    {
        FindCorrectSFXSound("BigFire");
        SoundManager.instance.SFXPlayer("BigFire", sfxSound);
    }
    public void SFXSound_MediumFire()
    {
        FindCorrectSFXSound("MediumFire");
        SoundManager.instance.SFXPlayer("MediumFire", sfxSound);
    }
    public void SFXSound_SmallFire()
    {
        FindCorrectSFXSound("SmallFire");
        SoundManager.instance.SFXPlayer("SmallFire", sfxSound);
    }


    public void BGSound_ImpressedBG()
    {
        SoundManager.instance.BgSoundPlay(impressedBG);
    }

    public void BGSound_Village()
    {
        SoundManager.instance.BgSoundPlay(VillageBG);
    }

    public void BindPlayerMoving()
    {
        playerController.playerCanMove = false;     // ui 조작 중 플레이어의 화면 회전을 막기 위함
        playerController.isPlayerMove = false;
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

    //05 상자
    #region
    public void Action05_GetBox()
    {
        StartCoroutine(coAction05_GetBox());
    }

    IEnumerator coAction05_GetBox()
    {
        BindPlayerMoving();
        FadeIn(2f);
        yield return new WaitForSeconds(2.1f);
        getBox.SetActive(true);
        FadeOut(2f);
        yield return new WaitForSeconds(2f);
        playerController.moveSpeed = 2.2f;
        Quest05Conversation.SetActive(true);
        UnBindPlayerMoving();
    }

    public void Action05_DropBox()
    {
        StartCoroutine(coAction05_DropBox());
    }

    IEnumerator coAction05_DropBox()
    {
        BindPlayerMoving();
        FadeIn(2f);
        yield return new WaitForSeconds(2.1f);
        getBox.SetActive(false);
        Quest06Box_Completed.SetActive(true);
        FadeOut(2f);
        UnBindPlayerMoving();
        yield return new WaitForSeconds(2f);
        playerController.moveSpeed = 2.8f;
        Quest05Conversation02.SetActive(true);
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
        SFXSound_FallingSoldier();
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
    #region
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
    #endregion

    //08 옷 태우기
    #region

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
        yield return new WaitForSeconds(2f);
        UnBindPlayerMoving();
        Quest08BigFire.SetActive(true);
        Quest08ConversationTrigger.SetActive(true);
    }
    #endregion

    //11 스크림
    #region
    public void Action11()
    {
        StartCoroutine(coAction11());
    }

    IEnumerator coAction11()
    {
        //BindPlayerMoving();
        //yield return new WaitForSeconds(0.5f);
        //Scream_Normal.SetActive(false);
        //Scream_FlipNeck.SetActive(true);
        //SFXSound_RunningMonster();
        //yield return new WaitForSeconds(3f);
        //Scream_FlipNeck.SetActive(false);
        //Scream_Run.SetActive(true);
        yield return new WaitForSeconds(1f);
        SFXSound_Scream();
        Scream_Face.SetActive(true);
        yield return new WaitForSeconds(0.6f);
        Scream_Face.SetActive(false);
        yield return new WaitForSeconds(1f);
        After_Scream_Conversation.SetActive(true);
        //coScreamRun = StartCoroutine(ScreamRun());
    }

    IEnumerator ScreamRun()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);

            screamNav_Run.destination = screamWayPoint[screamIndex].position;

            if (!screamNav_Run.pathPending && screamNav_Run.remainingDistance < 0.5f)
                Scream_GotoNext();
            if(screamIndex == screamWayPoint.Count)
            {
                Scream_Run.SetActive(false);
                Scream_Face.SetActive(true);
                yield return new WaitForSeconds(0.6f);
                Scream_Face.SetActive(false);
                yield return new WaitForSeconds(1f);
                After_Scream_Conversation.SetActive(true);
                StopCoroutine(coAction11());
            }
        }
    }

    void Scream_GotoNext()
    {
        screamNav_Run.destination = screamWayPoint[screamIndex].position;
        screamIndex = (screamIndex + 1);
    }
#endregion

    // 12몬스터 삼거리에 잠깐 비춰지는 움직임
    #region
    public void MonsterSetting()
    {
        coMonsterRun = StartCoroutine(MonsterRun());
    }


    IEnumerator MonsterRun()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            BindPlayerMoving();
            monster.SetActive(true);
            playerController.StartRotateToMonster_Village();
            monsterNav.destination = monsterWayPoint[monsterIndex].position;

            if (!monsterNav.pathPending && monsterNav.remainingDistance < 0.5f)
                Monster_GotoNext(); //목적지까지의 거리가 1이하거나 도착했으면 함수실행
            if (monsterIndex == monsterWayPoint.Count)
            {
                playerController.StopRotateToMonster_Village();
                monster.SetActive(false);
                UnBindPlayerMoving();
                StartCoroutine(TriggerConversation12());
                StopCoroutine(coMonsterRun);
            }
        }
    }

    IEnumerator TriggerConversation12()
    {
        yield return new WaitForSeconds(1f);
        Conversation12.SetActive(true);
    }

    void Monster_GotoNext()
    {
        monsterNav.destination = monsterWayPoint[monsterIndex].position;
        monsterIndex = (monsterIndex + 1);
    }
    #endregion

    // 14
    public void Action14()
    {
        StartCoroutine(coAction14());
    }

    IEnumerator coAction14()
    {
        yield return new WaitForSeconds(1f);
        Quest14Conversation.SetActive(true);
    }

    // 15 상자에 상호작용 순서대로

    public void Action15()
    {
        StartCoroutine(coAction15());
    }

    IEnumerator coAction15()
    {
        yield return new WaitForSeconds(1.5f);
        SFXSound_Impact();
    }

    public void Action15_1()
    {
        StartCoroutine(coAction15_1());
    }

    IEnumerator coAction15_1()
    {
        yield return new WaitForSeconds(1.5f);
        SFXSound_Impact();
        yield return new WaitForSeconds(1f);
        Quest15ConversationGoLastBox.SetActive(true);
    }

    //IEnumerator coAction15_1()
    //{
    //    BindPlayerMoving();
    //    FadeIn(2f);
    //    yield return new WaitForSeconds(2.1f);
    //    FadeOut(2f);
    //    UnBindPlayerMoving();
    //    Quest15MidiumFire01.SetActive(true);
    //    Quest15Conversation01.SetActive(true);
    //    yield return new WaitForSeconds(1.5f);
    //    SFXSound_Impact();
    //}
    //public void Action15_2()
    //{
    //    StartCoroutine(coAction15_2());
    //}

    //IEnumerator coAction15_2()
    //{
    //    BindPlayerMoving();
    //    FadeIn(2f);
    //    yield return new WaitForSeconds(2.1f);
    //    FadeOut(2f);
    //    UnBindPlayerMoving();
    //    Quest15MidiumFire02.SetActive(true);
    //    Quest15Conversation02.SetActive(true);

    //}
    //public void Action15_3()
    //{
    //    StartCoroutine(coAction15_3());
    //}

    //IEnumerator coAction15_3()
    //{
    //    BindPlayerMoving();
    //    FadeIn(2f);
    //    yield return new WaitForSeconds(2.1f);
    //    FadeOut(2f);
    //    UnBindPlayerMoving();
    //    Quest15MidiumFire03.SetActive(true);
    //    Quest15Conversation03.SetActive(true);
    //}

    //public void Action15_4()
    //{
    //    StartCoroutine(coAction15_4());
    //}


    //IEnumerator coAction15_4()
    //{
    //    BindPlayerMoving();
    //    FadeIn(2f);
    //    yield return new WaitForSeconds(2.1f);
    //    FadeOut(2f);
    //    UnBindPlayerMoving();
    //    Quest15MidiumFire04.SetActive(true);
    //    Quest15Conversation04.SetActive(true);
    //}

    public void Action16()
    {
        StartCoroutine(coAction16());
    }

    IEnumerator coAction16()
    {
        yield return new WaitForSeconds(1.5f);
        BindPlayerMoving();
        playerController.StartRotateToStoneWall();
        yield return new WaitForSeconds(1.5f);
        playerController.StopRotateToStoneWall();
        Quest16conversation.SetActive(true);
        UnBindPlayerMoving();
    }

    public void MonsterChaseSetting()
    {
        coMonsterChase = StartCoroutine(MonsterChase());
    }


    IEnumerator MonsterChase()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            ChaseMonster.SetActive(true);
            ChaseMonsterNav.destination = ChaseMonsterWayPoint[ChaseMonsterIndex].position;

            if (!ChaseMonsterNav.pathPending && ChaseMonsterNav.remainingDistance < 0.5f)
                ChaseMonster_GotoNext(); //목적지까지의 거리가 1이하거나 도착했으면 함수실행
            if (ChaseMonsterIndex == ChaseMonsterWayPoint.Count)
            {
                ChaseMonster.SetActive(false);
                StartCoroutine(coLoadDeathScene());
                StopCoroutine(coMonsterChase);
            }
        }
    }

    void ChaseMonster_GotoNext()
    {
        ChaseMonsterNav.destination = ChaseMonsterWayPoint[ChaseMonsterIndex].position;
        ChaseMonsterIndex = (ChaseMonsterIndex + 1);
    }


    public void ClosePauseMenu()
    {
        PauseMenu.SetActive(false);
        UnBindPlayerMoving();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // 09플래쉬 허수아비
    #region
    public void FlashScarecrow()
    {
        StartCoroutine(coFlashScarecrow());
    }

    IEnumerator coFlashScarecrow()
    {
        yield return new WaitForSeconds(1.2f);
        SFXSound_FlashScarecrow();
        flashed_Scarecrow.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        flashed_Scarecrow.SetActive(false);
        Quest11afterFlashScarecrowBox.SetActive(true);
    }

    public void Action09Conversation()
    {
        StartCoroutine(coAction09Conversation());
    }

    IEnumerator coAction09Conversation()
    {
        yield return new WaitForSeconds(1);
        Quest09Conversation.SetActive(true);
    }
    #endregion

    // 16 공격 받는 씬
    #region

    IEnumerator coLoadDeathScene()
    {
        BindPlayerMoving();
        //playerCamera.SetActive(false);
        playerBody.SetActive(false);
        //Player.transform.LookAt(ChaseMonster.transform.localPosition);
        Player.transform.DOLocalRotate(new Vector3(-80, 0, 0), 2f);
        //deathSceneCamera.SetActive(true);
        //deathSceanMonster.SetActive(true);
        yield return new WaitForSeconds(3f);
        blackBackground.SetActive(true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Ending");
    }
    #endregion

    public void OpenSoundControlPanel()
    {
        SoundManager.instance.OpenSoundControlPanel();
    }

    public void ClosdSoundControlPanel()
    {
        SoundManager.instance.CloseSoundControllPanel();

    }

}
