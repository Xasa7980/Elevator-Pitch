using DamageNumbersPro;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


public class UI_Animations : MonoBehaviour
{
    [SerializeField] private bool enableAnimation = true;
    [SerializeField] private float animationSpeed = 1f;
    [SerializeField] private float startTime;

    public Vector3 originalPosition;

    [SerializeField] private AnimationCurve scaleCurve;
    [SerializeField] private AnimationCurve alphaCurve;
    [SerializeField] private AnimationCurve shrinkCurve; // Curva para el efecto de encogimiento
    [SerializeField] private float fadeDuration = 1f; // Duraci�n del efecto fade in y fade out
    [SerializeField] private float jumpHeight = 2f; // Altura m�xima del salto
    [SerializeField] private float lifeTime = 5f; // Tiempo total de vida del objeto

    [SerializeField] private bool enableLateralJump = false; // Controla si el salto incluir� un movimiento lateral
    [SerializeField] private float lateralJumpIntensity = 2f; // Intensidad del salto lateral
    [SerializeField] private bool enableSpiralMovement = false; // Controla si el objeto realizar� un movimiento en espiral
    [SerializeField] private float spiralIntensity = 5f; // Intensidad del movimiento espiral

    [SerializeField] private TextMeshProUGUI textMesh;

    public void SetTargetValue ( Vector3 target ) => targetPosition = target;
    private Vector3 targetPosition;

    private float currentLifeTime = 0f; // Contador de tiempo de vida actual
    private void OnEnable ( )
    {
        ResetValues();
        originalPosition = transform.position;
    }

    void Start ( )
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        InitFadeIn();
    }

    void Update ( )
    {
        transform.forward = Camera.main.transform.forward;
        if (!enableAnimation) return;

        currentLifeTime += Time.deltaTime;
        float timeSinceStart = Time.time - startTime;
        AnimatePosition(timeSinceStart);
        AnimateScaleAndShrink(timeSinceStart);
        AnimateFade(currentLifeTime);

        if (currentLifeTime >= lifeTime)
        {
            gameObject.SetActive(false);
        }
    }

    void ResetValues ( )
    {
        startTime = Time.time;
        transform.position = originalPosition;
        currentLifeTime = 0f;
        InitFadeIn();
        enableAnimation = true;
    }

    void AnimatePosition ( float time )
    {
        float normalizedTime = time / lifeTime;
        float verticalMovement = jumpHeight * Mathf.Sin(Mathf.PI * normalizedTime);

        // Calcula la posici�n horizontal solo si el salto lateral est� habilitado
        Vector3 spiralMovement = Vector3.zero;
        float horizontalMovement = 0f;
        if (enableLateralJump)
        {
            // Mapea el tiempo normalizado a la posici�n horizontal para hacer un arco
            horizontalMovement = lateralJumpIntensity * Mathf.Sin(Mathf.PI * normalizedTime);
        }

        float spiralMovementX = 0f;
        float spiralMovementY = 0f;
        if (enableSpiralMovement)
        {
            float spiralProgress = normalizedTime * 2 * Mathf.PI * spiralIntensity;
            spiralMovementX = Mathf.Sin(spiralProgress) * spiralIntensity;
            spiralMovementY = Mathf.Cos(spiralProgress) * spiralIntensity * (normalizedTime <= 0.5f ? 1 : -1);
        }

        Vector3 newPosition = originalPosition + new Vector3(horizontalMovement + spiralMovementX, verticalMovement + spiralMovementY, 0);
        transform.position = newPosition;
        if (normalizedTime >= 1f)
        {
            gameObject.SetActive(false);
        }
    }

    void AnimateScaleAndShrink ( float time )
    {
        // Usa la curva de encogimiento para ajustar la escala del objeto durante su tiempo de vida
        float scaleMultiplier = shrinkCurve.Evaluate(currentLifeTime / lifeTime);
        float scale = scaleCurve.Evaluate(time) * scaleMultiplier;
        transform.localScale = new Vector3(scale, scale, scale);
    }

    void AnimateFade ( float time )
    {
        float alpha = time < fadeDuration ? time / fadeDuration : time > (lifeTime - fadeDuration) ? (lifeTime - time) / fadeDuration : 1f;
        Color color = textMesh.color;
        color.a = alphaCurve.Evaluate(alpha);
        textMesh.color = color;
    }

    public void UpdateTarget ( Vector3 newTargetPosition, float newAnimationSpeed )
    {
        targetPosition = newTargetPosition;
        animationSpeed = newAnimationSpeed;
        ResetValues();
    }

    public void SetScaleCurve ( AnimationCurve newScaleCurve )
    {
        scaleCurve = newScaleCurve;
    }

    public void SetAlphaCurve ( AnimationCurve newAlphaCurve )
    {
        alphaCurve = newAlphaCurve;
    }

    public void SetJumpCurve ( AnimationCurve newJumpCurve )
    {
        // Este m�todo se mantiene por compatibilidad, aunque no se use directamente en AnimatePosition
    }

    public void SetShrinkCurve ( AnimationCurve newShrinkCurve )
    {
        shrinkCurve = newShrinkCurve;
    }

    void InitFadeIn ( )
    {
        Color color = textMesh.color;
        color.a = 0f;
        textMesh.color = color;
    }
}