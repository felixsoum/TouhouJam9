using UnityEngine;

public class Actor : MonoBehaviour
{
    [SerializeField] GameObject visualContainer;
    [SerializeField] MeshRenderer meshRenderer;

    protected Material material;
    protected Camera mainCamera;

    public bool IsFacingRight => isFacingRight;
    bool isFacingRight = true;
    float hitValue;

    public bool IsAlive => isAlive;
    protected bool isAlive = true;

    protected virtual void Awake()
    {
        material = meshRenderer.material;
    }

    protected virtual void Start()
    {
        mainCamera = Camera.main;
        SetIsFacingRight(true);
    }

    private void OnEnable()
    {
        isAlive = true;
    }

    protected virtual void Update()
    {
        meshRenderer.transform.forward = mainCamera.transform.forward;
        hitValue -= 5f * Time.deltaTime;
        hitValue = Mathf.Clamp01(hitValue);
        material.SetFloat("_Hit", hitValue);
    }

    protected void OnHit()
    {
        hitValue = 1f;
        material.SetFloat("_Hit", hitValue);
    }

    protected void SetIsFacingRight(bool isFacingRight)
    {
        if (this.isFacingRight == isFacingRight)
        {
            return;
        }

        this.isFacingRight = isFacingRight;

        visualContainer.transform.localScale = new Vector3(isFacingRight ? -1 : 1, 1, 1);
    }

    protected virtual void OnDeath() { }
}
