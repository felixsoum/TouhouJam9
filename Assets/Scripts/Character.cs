using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] MeshRenderer meshRenderer;

    Material material;

    Camera mainCamera;

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
    }
}
