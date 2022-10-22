using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    [SerializeField] MeshRenderer meshRenderer;

    protected Material material;
    protected Camera mainCamera;

    private void Awake()
    {
        material = meshRenderer.material;
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    protected virtual void Update()
    {
        meshRenderer.transform.forward = mainCamera.transform.forward;
    }
}
