using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Required if fading a UI Image

public class Coccus : BaseLife
{
    [SerializeField] public float energyToReproduce = 120f;
    public float energy = 100f;

    [SerializeField] private float minSpeed = 0f;
    [SerializeField] private float maxSpeed = 4f;

    [SerializeField] private float minScale = 0.2f;
    [SerializeField] private float maxScale = 0.6f;
    private float scale = 1f;
    private Vector3 newScale = new Vector3(2f, 2f, 2f);

    public bool isDead = false;
    public float fadeDuration = 2f; // Duration of the fade in seconds

    private SpriteRenderer spriteRenderer; // For 2D Sprites
    private Image uiImage; // For UI Images

    public void CoccusAwake()
    {
        // Get the appropriate component based on your object type
        spriteRenderer = GetComponent<SpriteRenderer>();
        uiImage = GetComponent<Image>();

        // Set initial alpha to 0 (fully transparent)
        if (spriteRenderer != null)
        {
            Color color = spriteRenderer.color;
            color.a = 0f;
            spriteRenderer.color = color;
        }
        else if (uiImage != null)
        {
            Color color = uiImage.color;
            color.a = 0f;
            uiImage.color = color;
        }
    }
    public void CoccusStart()
    {
        direction = UnityEngine.Random.insideUnitCircle.normalized;
        speed = UnityEngine.Random.Range(minSpeed, maxSpeed);
        GetComponent<Rigidbody2D>().linearVelocity = direction * speed;

        scale = UnityEngine.Random.Range(minScale, maxScale);
        newScale = new Vector3(scale, scale, 1f);
        transform.localScale = newScale;
        StartCoroutine(FadeIn());
    }

    public void CoccusUpdate()
    {
        BaseLifeUpdate();

        if (energy <= 0 || isDead)
            StartCoroutine(FadeOutAndDie());
    }

    public void CreateCoccus(GameObject CoccusPrefab, Vector3 position)
    {
        if (GlobalVariables.cellsCount < 150)
        {
            Instantiate(CoccusPrefab, position, Quaternion.identity);
            GlobalVariables.cellsCount += 1;
        }
    }

    private IEnumerator FadeOutAndDie()
    {
        Renderer objectRenderer = GetComponent<Renderer>();
        if (objectRenderer == null)
            yield break;

        Material material = objectRenderer.material; // Get the material instance
        Color startColor = material.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f); // Target alpha 0

        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float progress = timer / fadeDuration;
            material.color = Color.Lerp(startColor, endColor, progress);
            yield return null; // Wait for the next frame
        }

        // Ensure the object is fully transparent at the end
        material.color = endColor;

        UnityEngine.Object.Destroy(this.gameObject);
        GlobalVariables.cellsCount -= 1;
    }
    IEnumerator FadeIn()
    {
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float newAlpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);

            if (spriteRenderer != null)
            {
                Color color = spriteRenderer.color;
                color.a = newAlpha;
                spriteRenderer.color = color;
            }
            else if (uiImage != null)
            {
                Color color = uiImage.color;
                color.a = newAlpha;
                uiImage.color = color;
            }
            yield return null; // Wait for the next frame
        }
    }
}