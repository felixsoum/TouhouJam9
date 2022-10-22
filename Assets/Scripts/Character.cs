using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Actor
{
    public CharacterMouseProxy MouseProxy { get; set; }
    private bool isPicked;

    public static Character GetPlayerCharacter()
    {
        return GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
    }

    protected override void Update()
    {
        base.Update();

        if (isPicked)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                float moveDelta = transform.position.x - hitInfo.point.x;
                if (Mathf.Abs(moveDelta) > 0.01f)
                {
                    SetIsFacingRight(transform.position.x < hitInfo.point.x); 
                }
                transform.position = hitInfo.point;
            }
            

            if (Input.GetMouseButtonUp(0))
            {
                isPicked = false;
                material.SetFloat("_IsPicked", 0);
                MouseProxy.Reset();
                Time.timeScale = 1;
            }
        }
    }

    internal void MouseDown()
    {
        isPicked = true;
        material.SetFloat("_IsPicked", 1);
        Time.timeScale = 0;
    }
}
