using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ClickAction : MonoBehaviour
{
    public bool snowOnTrack = false;
    public bool bigHearthOnTrack = false;
    public bool manyHearthsOnTrack = false;
    
    public Animator[] animatorsToSwitchStateSnow;
    public Animator[] animatorsToSwitchStateBigHearth;
    public Animator[] animatorsToSwitchStateManyHearths;
    public ParticleSystem particleSystemSnow;
    public ParticleSystem particleSystemManyHearths;

    public void ChangeSnow(bool newStatus)
    {
        snowOnTrack = newStatus;
    }
    public void Change1H(bool newStatus)
    {
        bigHearthOnTrack = newStatus;
    }
    public void ChangeMH(bool newStatus)
    {
        manyHearthsOnTrack = newStatus;
    }
    
    public void MouseDown()
    {
        if (snowOnTrack)
        {
            foreach (var curr in animatorsToSwitchStateSnow) curr.enabled = !curr.enabled;
            if(!animatorsToSwitchStateSnow[0].enabled)
                particleSystemSnow?.Pause();
            else
                particleSystemSnow?.Play();
        }
        /*else
        {
            foreach (var curr in animatorsToSwitchStateSnow) curr.enabled = !curr.enabled;
            particleSystemSnow?.Pause();
        }*/
        if (bigHearthOnTrack)
        {
            foreach (var curr in animatorsToSwitchStateBigHearth) curr.enabled = !curr.enabled;
        }
        /*else
        {
            foreach (var curr in animatorsToSwitchStateBigHearth) curr.enabled = !curr.enabled;
        }*/
        if (manyHearthsOnTrack)
        {
            foreach (var curr in animatorsToSwitchStateManyHearths) curr.enabled = !curr.enabled;
            if(!animatorsToSwitchStateManyHearths[0].enabled)
                particleSystemManyHearths?.Pause();
            else
                particleSystemManyHearths?.Play();
        }/*
        else
        {
            foreach (var curr in animatorsToSwitchStateSnow) curr.enabled = !curr.enabled;
            particleSystemManyHearths?.Pause();
        }*/
        
    }
}
