using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public Level[] levels;

    public Level currentLevel;

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        else Instance = this;
    }

    public void SwitchLevel(Level lv)
    {
        currentLevel = lv;

        OnSwitchLevel(lv);
    }

    public event Action<Level> onSwitchLevel;
    public void OnSwitchLevel(Level lv) { onSwitchLevel?.Invoke(lv); }
}
