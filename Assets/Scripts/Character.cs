using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] MeshRenderer meshRenderer;

    Material material;

    Camera mainCamera;

    public CharacterMouseProxy MouseProxy { get; set; }
    private bool isPicked;

    private void Awake()
    {
        material = meshRenderer.material;
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        meshRenderer.transform.forward = mainCamera.transform.forward;

        if (isPicked)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                Debug.Log($"{hitInfo.collider.gameObject.name}, {hitInfo.point}");
                transform.position = hitInfo.point;
            }
            

            if (Input.GetMouseButtonUp(0))
            {
                isPicked = false;
                material.SetFloat("_IsPicked", 0);
                MouseProxy.Reset();
            }
        }
    }

    internal void MouseDown()
    {
        isPicked = true;
        material.SetFloat("_IsPicked", 1);
    }
}
