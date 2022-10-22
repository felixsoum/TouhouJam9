using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMouseProxy : MonoBehaviour
{
    [SerializeField] Character character;
    [SerializeField] Collider characterColllider;

    private void Start()
    {
        character.MouseProxy = this;
    }

    private void OnMouseDown()
    {
        character.MouseDown();
        characterColllider.enabled = false;
    }

    public void Reset()
    {
        characterColllider.enabled = true;
    }
}
