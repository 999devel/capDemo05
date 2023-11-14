using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;


public class GameManager : MonoBehaviour
{
    public PlayerController playerController;

    public GameObject enterVillageScenePanel;
    public GameObject House_Collider_Door;
    public GameObject House_Collider2;
    public GameObject Village_PatrolGuideUIPanel;
    public GameObject[] Village_Walls;
    public GameObject[] Village_Doors;
    public GameObject entryWall;
    public static int village_Count = 0;

    public GameObject playerTorch;
    public GameObject houseTorch;

    [Header("Spawn Point")]
    public Transform playerPos;
    public Transform outOfHousePoint;
    public Transform IntoHousePoint;
    public Transform outOfForestPoint;
    public Transform IntoForestPoint;
    public Transform test_InFrontOfWhiteMonsterPoint;

    [Header("Screen Fade")]
    public CanvasGroup canvasGroup;
    private Tween fadeTween;

    [Header("Door Animation")]
    [SerializeField] private Animator doorAnimator;
    //public bool doorOpenTrigger;

    [Header("Sound")]
    public AudioClip[] sfxlist;
    [HideInInspector]public AudioClip sfxSound;



    private void Start()
    {
        StartCoroutine(Beginning());
    }

    public void CloseEnterVillageSceneButtonPanel()
    {
        enterVillageScenePanel.SetActive(false);
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
        FadeOut(2f);
        yield return null;
        playerController.playerCanMove = true;
    }

    IEnumerator VillageToHouse()
    {
        playerController.playerCanMove = false;
        FadeIn(2f);
        yield return fadeTween.WaitForCompletion();
        playerPos.position = IntoHousePoint.position;
        playerPos.rotation = IntoHousePoint.rotation;
        FadeOut(2f);
        playerController.playerCanMove = true;
    }

    IEnumerator HouseToVillage()
    {
        playerController.playerCanMove = false;
        FadeIn(2f);
        yield return fadeTween.WaitForCompletion();
        playerPos.position = outOfHousePoint.position;
        playerPos.rotation = outOfHousePoint.rotation;
        FadeOut(2f);
        playerController.playerCanMove = true;
    }

    IEnumerator VillageToForest()
    {
        playerController.playerCanMove = false;
        FadeIn(2f);
        yield return fadeTween.WaitForCompletion();
        playerPos.position = IntoForestPoint.position;
        playerPos.rotation = IntoForestPoint.rotation;
        FadeOut(2f);
        playerController.playerCanMove = true;
    }

    IEnumerator ForestToVillage()
    {
        playerController.playerCanMove = false;
        FadeIn(2f);
        yield return fadeTween.WaitForCompletion();
        playerPos.position = outOfForestPoint.position;
        playerPos.rotation = outOfForestPoint.rotation;
        FadeOut(2f);
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

    public void test_playerToInFrontOfWhiteMonster()
    {
        playerPos.position = test_InFrontOfWhiteMonsterPoint.position;
        playerPos.rotation = test_InFrontOfWhiteMonsterPoint.rotation;
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

    // 문 점검 시 두 번 사운드
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
}
