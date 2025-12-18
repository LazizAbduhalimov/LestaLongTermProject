using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("References")]
    private HealthComponent HealthComponent;
    public Image FillImage;
    public Canvas Canvas;
    
    [Header("Settings")]
    public bool HideOnFullHealth = true;
    public bool SmoothTransition = true;
    public float TransitionSpeed = 5f;
    public bool BillboardToCamera = true;
    
    [Header("Colors")]
    public bool UseColorGradient = true;
    public Color HighHealthColor = Color.green;
    public Color MidHealthColor = Color.yellow;
    public Color LowHealthColor = Color.red;
    
    private Camera _mainCamera;
    private float _targetFillAmount = 1f;
    private CanvasGroup _canvasGroup;

    private void Start()
    {
        _mainCamera = Camera.main;
        
        _canvasGroup = Canvas.GetComponent<CanvasGroup>();
        HealthComponent = GetComponentInParent<HealthComponent>();
        ForceUpdateBar();
    }

    public void OnEnable()
    {
        ForceUpdateBar();
    }

    private void ForceUpdateBar()
    {
        if (HealthComponent == null) return;
        HealthComponent.OnHealthChanged += UpdateHealthBar;
        UpdateHealthBar(HealthComponent.MaxHealth, HealthComponent.CurrentHealth, HealthComponent.MaxHealth);
    }

    public void OnDisable()
    {
        if (HealthComponent == null) return;
        HealthComponent.OnHealthChanged -= UpdateHealthBar;
    }

    private void Update()
    {
        // Billboard эффект - поворачиваем к камере
        if (BillboardToCamera && _mainCamera != null && Canvas != null)
        {
            Canvas.transform.LookAt(Canvas.transform.position + _mainCamera.transform.rotation * Vector3.forward,
                _mainCamera.transform.rotation * Vector3.up);
        }

        // Плавное изменение fill amount
        if (SmoothTransition && FillImage != null)
        {
            FillImage.fillAmount = Mathf.Lerp(FillImage.fillAmount, _targetFillAmount, Time.deltaTime * TransitionSpeed);
        }
    }

    public void UpdateHealthBar(int oldHealth, int currentHealth, int maxHealth)
    {
        if (FillImage == null) return;

        float healthNormalized = (float)currentHealth / maxHealth;
        
        if (SmoothTransition)
        {
            _targetFillAmount = healthNormalized;
        }
        else
        {
            FillImage.fillAmount = healthNormalized;
        }

        // Обновляем цвет
        if (UseColorGradient)
        {
            UpdateHealthColor(healthNormalized);
        }

        // Скрываем/показываем бар
        if (HideOnFullHealth && _canvasGroup != null)
        {
            _canvasGroup.alpha = currentHealth >= maxHealth ? 0f : 1f;
        }
    }

    private void UpdateHealthColor(float healthPercent)
    {
        if (FillImage == null) return;

        if (healthPercent > 0.5f)
        {
            // От 50% до 100%: переход от желтого к зеленому
            float t = (healthPercent - 0.5f) * 2f;
            FillImage.color = Color.Lerp(MidHealthColor, HighHealthColor, t);
        }
        else
        {
            // От 0% до 50%: переход от красного к желтому
            float t = healthPercent * 2f;
            FillImage.color = Color.Lerp(LowHealthColor, MidHealthColor, t);
        }
    }
}