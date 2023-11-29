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

    public void SoliderGoEntranceOfVillage()
    {
        coWaypoint = StartCoroutine(CoroutineSoliderGoEntranceOfVillage());
    }


    IEnumerator CoroutineSoliderGoEntranceOfVillage()
    {
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


    public void ClosePauseMenu()
    {
        PauseMenu.SetActive(false);
        UnBindPlayerMoving();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

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
}
