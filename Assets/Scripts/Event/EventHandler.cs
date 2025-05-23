using System;
using UnityEngine;

public static class EventHandler
{
    public static event Action SceneTransitionEvent;
    public static void CallSceneTransitionEvent(){
        SceneTransitionEvent?.Invoke();
    }

    public static event Action CloseEvent;
    public static void CallCloseEvent(){
        CloseEvent?.Invoke();
    }

    public static event Action StartShakeEvent;
    public static void CallStartShakeEvent(){
        StartShakeEvent?.Invoke();
    }

    public static event Action StartDragEvent;
    public static void CallStartDragEvent(){
        StartDragEvent?.Invoke();
    }

    public static event Action StartClickEvent;
    public static void CallStartClickEvent(){
        StartClickEvent?.Invoke();
    }

    public static event Action CloseClickEvent;
    public static void CallCloseClickEvent(){
        CloseClickEvent?.Invoke();
    }

    public static event Action FinishGameEvent;
    public static void CallFinishGameEvent(){
        FinishGameEvent?.Invoke();
    }

    public static event Action StartScrollEvent;
    public static void CallStartScrollEvent(){
        StartScrollEvent?.Invoke();
    }

    public static event Action EndScrollEvent;
    public static void CallEndScrollEvent(){
        EndScrollEvent?.Invoke();
    }

    
}
