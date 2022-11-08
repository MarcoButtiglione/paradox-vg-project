using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BlinkManager : MonoBehaviour
{
    private Renderer rendererGhost;
    //public int timeToBlink = 3;
    //private bool fadingDown = true;

    private void Start()
    {
        rendererGhost = gameObject.GetComponent<Renderer>();
    }
    private void FixedUpdate()
    {
        var tempColor = rendererGhost.material.color;

        if (GameManager.Instance.State == GameState.OldPlayerTurn)
        {
            tempColor.a = Mathf.Clamp(Time.unscaledDeltaTime, 0.5f , 1f);
            rendererGhost.material.color = tempColor;
        }

    }
}
