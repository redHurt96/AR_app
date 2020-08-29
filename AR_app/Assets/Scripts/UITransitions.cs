using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITransitions : SimpleSingleton<UITransitions>
{
    public GameObject ev;
    public static GameObject EventSystem;

    public static void SlideTo(SlideUITransition uITransition) =>
        Instance.StartCoroutine(SlideToRoutine(uITransition));

    public static void Fade(FadeUITransition uITransition) =>
        Instance.StartCoroutine(FadeRoutine(uITransition));

    private void Awake()
    {
        EventSystem = ev;
    }

    private static IEnumerator SlideToRoutine(SlideUITransition uITransition)
    {
        float currentTime = 0f;
        Vector3 startPosition = uITransition.RectTransform.localPosition;

        if (uITransition.withDisableTouch)
            EventSystem.SetActive(false);

        while (currentTime < 1f)
        {
            uITransition.RectTransform.localPosition = Vector3.Lerp(startPosition, uITransition.EndPosition, uITransition.Curve.Evaluate(currentTime));
            currentTime += Time.deltaTime / uITransition.TransitionTime;
            yield return null;
        }

        uITransition.RectTransform.localPosition = uITransition.EndPosition;

        if (uITransition.withDisableTouch)
            EventSystem.SetActive(true);

        uITransition.OnDone?.Invoke();
    }

    private static IEnumerator FadeRoutine(FadeUITransition uITransition)
    {
        float currentTime = 0f;

        if (uITransition.withDisableTouch)
            EventSystem.SetActive(false);

        while (currentTime < 1f)
        {
            uITransition.CanvasGroup.alpha = Mathf.Lerp(uITransition.StartAlpha, uITransition.EndAlpha, uITransition.Curve.Evaluate(currentTime));
            currentTime += Time.deltaTime / uITransition.TransitionTime;
            yield return null;
        }

        uITransition.CanvasGroup.alpha = 0f;

        if (uITransition.withDisableTouch)
            EventSystem.SetActive(true);

        uITransition.OnDone?.Invoke();
    }
}

public class UITransition
{
    public bool withDisableTouch = false;
    public float TransitionTime;
    public AnimationCurve Curve;
    public Action OnDone;

    public virtual void Play() { }
}

public class SlideUITransition : UITransition
{
    public RectTransform RectTransform;
    public Vector3 EndPosition;

    public SlideUITransition(RectTransform rectTransform, Vector3 endPosition, AnimationCurve curve, float transitionTime = 1f, Action onDone = null)
    {
        RectTransform = rectTransform;
        EndPosition = endPosition;
        Curve = curve;
        TransitionTime = transitionTime;
        OnDone = onDone;
    }

    public override void Play() => UITransitions.SlideTo(this);
}

public class FadeUITransition : UITransition
{
    public CanvasGroup CanvasGroup;
    public float StartAlpha;
    public float EndAlpha;

    public FadeUITransition(CanvasGroup canvasGroup, float startAlpha, float endAlpha, AnimationCurve curve, float transitionTime = 1f, Action onDone = null)
    {
        CanvasGroup = canvasGroup;
        StartAlpha = startAlpha;
        EndAlpha = endAlpha;
        Curve = curve;
        TransitionTime = transitionTime;
        OnDone = onDone;
    }

    public override void Play() => UITransitions.Fade(this);
}