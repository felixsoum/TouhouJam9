using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] Image hpForeground;
    [SerializeField] Image deathBackground;
    [SerializeField] Image yuyukoPortrait;
    [SerializeField] TMP_Text deathText;
    Character player;

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
    }

    private void Update()
    {
        hpForeground.transform.localScale = new Vector3(player.GetHPRatio(), 1, 1);
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
        SceneManager.LoadScene("Start");
    }
}
