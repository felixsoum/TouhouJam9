using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    [SerializeField] GameObject visualContainer;
    [SerializeField] MeshRenderer meshRenderer;

    protected Material material;
    protected Camera mainCamera;

    bool isFacingRight = true;

    private void Awake()
    {
        material = meshRenderer.material;
    }

    protected virtual void Start()
    {
        mainCamera = Camera.main;
    }

    protected virtual void Update()
    {
        meshRenderer.transform.forward = mainCamera.transform.forward;
    }

    protected void SetIsFacingRight(bool isFacingRight)
    {
        if (this.isFacingRight == isFacingRight)
        {
            return;
        }

        this.isFacingRight = isFacingRight;

        visualContainer.transform.localScale = new Vector3(isFacingRight ? 1 : -1, 1, 1);
    }
}
