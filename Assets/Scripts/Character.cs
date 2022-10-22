using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] MeshRenderer meshRenderer;

    Material material;

    Camera mainCamera;
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

        if (isPicked && Input.GetMouseButtonUp(0))
        {
            isPicked = false;
            material.SetFloat("_IsPicked", 0);
        }
    }

    internal void MouseDown()
    {
        isPicked = true;
        material.SetFloat("_IsPicked", 1);
    }
}
