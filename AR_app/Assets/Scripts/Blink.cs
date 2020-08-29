using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class Blink : MonoBehaviour
{
    private AnimationCurve _inOutCurve;

    public void CreateBlink(float time)
    {
        FadeUITransition fadeOut = new FadeUITransition(
            GetComponent<CanvasGroup>(),
            .5f, 0f, _inOutCurve, time / 2f);


        FadeUITransition fadeIn = new FadeUITransition(
            GetComponent<CanvasGroup>(),
            0f, .5f, _inOutCurve, time / 2f,
            () => fadeOut.Play()
        );

        fadeIn.Play();
    }
}