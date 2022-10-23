using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] RectTransform staminaBar;
    [SerializeField] Image staminaForeground;
    [SerializeField] Image hpForeground;
    [SerializeField] Image deathBackground;
    [SerializeField] Image yuyukoPortrait;
    [SerializeField] Image expBarForeground;
    [SerializeField] TMP_Text deathText;
    [SerializeField] GameObject levelUpMenu;

    Character player;
    private Camera mainCamera;

    private void Awake()
    {
        deathBackground.enabled = false;
        yuyukoPortrait.enabled = false;
        deathText.enabled = false;
        levelUpMenu.SetActive(false);
    }

    internal void CompleteGame()
    {
        StartCoroutine(CompleteGameCoroutine());
    }

    private void Start()
    {
        player = Character.GetPlayerCharacter();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        hpForeground.transform.localScale = new Vector3(player.GetHPRatio(), 1, 1);

        staminaBar.gameObject.SetActive(player.Stamina < 1f && player.IsAlive);
        staminaForeground.fillAmount = player.Stamina;

        float expRatio = (float)player.currentExp / player.maxExp;
        expBarForeground.transform.localScale = new Vector3(expRatio, 1, 1);

        var screenPoint = mainCamera.WorldToScreenPoint(player.GetHeadPosition());
        screenPoint.x -= 50;
        screenPoint.y += 50;
        staminaBar.position = screenPoint;
    }

    IEnumerator CompleteGameCoroutine()
    {
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 0;
        deathBackground.enabled = true;
        yield return new WaitForSecondsRealtime(1f);
        yuyukoPortrait.enabled = true;
        yield return new WaitForSecondsRealtime(1f);
        deathText.enabled = true;
        yield return new WaitForSecondsRealtime(5f);
        Time.timeScale = 1;
        SceneManager.LoadScene("Start");
    }

    internal void LevelUp()
    {
        levelUpMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void OnUpgradeChoice(int index)
    {
        if (index == 0)
        {
            player.UpgradeFireRate();

        }
        else
        {
            player.UpgradeBeam();
        }

        levelUpMenu.SetActive(false);
        Time.timeScale = 1;
    }
}
