using System;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] TMP_Text deathText;
    Character player;
    private Camera mainCamera;

    private void Awake()
    {
        deathBackground.enabled = false;
        yuyukoPortrait.enabled = false;
        deathText.enabled = false;
    }
    internal void ShowDeathScreen()
    {
        StartCoroutine(DeathScreenCoroutine());
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

        var screenPoint = mainCamera.WorldToScreenPoint(player.GetHeadPosition());
        screenPoint.x -= 50;
        screenPoint.y += 50;
        staminaBar.position = screenPoint;
    }

    IEnumerator DeathScreenCoroutine()
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
}
