using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoaderPanel : PanelBase

{
    [SerializeField] private Slider progressBar;
    protected override void Awake()
    {
        base.Awake();
        if (progressBar == null)
        {
            progressBar = GetComponentInChildren<Slider>();
        }
        if (progressBar != null)
        {
            GameManager.Instance.progressBar = progressBar;
        }
    }
}
