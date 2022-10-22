using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMouseProxy : MonoBehaviour
{
    [SerializeField] Character character;

    private void OnMouseDown()
    {
        character.MouseDown();
    }
}
