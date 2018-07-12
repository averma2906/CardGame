using System;
using System.Collections;
using UnityEngine;

public class MonoBehaviourExtended : MonoBehaviour
{
    public void InvokeRepeating(Action action, float time, float repeatTime)
    {
        InvokeRepeating(action.Method.Name, time, repeatTime);
    }

    public void Invoke(Action action, float time)
    {
        Invoke(action.Method.Name, time);
    }

    public void CancelInvoke(Action action)
    {
        CancelInvoke(action.Method.Name);
    }

    public GameObject Instantiate(UnityEngine.Object original, Vector3 position)
    {
        return Instantiate(original, position, Quaternion.identity) as GameObject;
    }

    public bool IsInvoking(Action action)
    {
        return IsInvoking(action.Method.Name);
    }



    public Coroutine WaitForRealSeconds(float seconds)
    {
        return StartCoroutine(WaitCoroutine(seconds));
    }

    private IEnumerator WaitCoroutine(float seconds)
    {
        var initialTime = Time.realtimeSinceStartup;

        while (Time.realtimeSinceStartup < (initialTime + seconds))
        {
            yield return null;
        }
    }
}