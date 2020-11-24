using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndTurnButton : MonoBehaviour
{
    public bool activated;
    bool inCoroutine = false;

    private void Update()
    {
        if (activated)
        {
            if (!inCoroutine)
                StartCoroutine(ColorLerp());
        }
        else
        {
            inCoroutine = false;
            StopAllCoroutines();
            GetComponent<Image>().color = Color.white;
        }
    }

    public IEnumerator ColorLerp()
    {
        inCoroutine = true;
        while (true)
        {
            int elapsedFrames = 0;

            while (GetComponent<Image>().color != Color.green)
            {
                float t = (float)elapsedFrames / 180;
                GetComponent<Image>().color = Color.Lerp(Color.white, Color.green, t);
                elapsedFrames++;
                yield return null;
            }

            elapsedFrames = 0;

            while (GetComponent<Image>().color != Color.white)
            {
                float t = (float)elapsedFrames / 180;
                GetComponent<Image>().color = Color.Lerp(Color.green, Color.white, t);
                elapsedFrames++;
                yield return null;
            }
        }
    }
}
