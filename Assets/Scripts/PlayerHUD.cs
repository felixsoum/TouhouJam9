using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] Image hpForeground;
    Character player;
    private void Start()
    {
        player = Character.GetPlayerCharacter();
    }

    private void Update()
    {
        hpForeground.transform.localScale = new Vector3(player.GetHPRatio(), 1, 1);
    }
}
