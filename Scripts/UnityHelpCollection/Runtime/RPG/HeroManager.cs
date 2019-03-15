﻿using System;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine;

public class HeroManager : MonoBehaviour,ModelInterface
{
    public event Action<float> HpChanged;
    public event Action<float> MpChanged;
    public event Action<int, string> SkillChanged;
    public event Action<int> LevelChanged;
    public event Action<float> ExperienceChanged;
    public event Action<EquipmentType, string> EquipChanged;
    public event Action<int, int, string> PackageChanged;
    public GameValuesSys valuesSys;

    private Equipment[,] package = new Equipment[6, 8];

    void Awake()
    {
        valuesSys.HpChanged.AddListener(delegate (float value) { HpChanged(value); });
        valuesSys.MpChanged.AddListener(delegate (float value) { MpChanged(value); });
    }

    public void SetEquip(int EquipType, string equipID)
    {
        throw new NotImplementedException();
    }

    public void SetPackage(int row, int line, string equipID)
    {
        var s = Resources.Load<Equipment>(m_Path.respDataEquip+equipID+".asset");
        if (s != null)
        {
            package[row, line] = s;
            PackageChanged(row, line, equipID);
        }
    }

    public void SetSkill(int slotNumber, string skillID)
    {
        throw new NotImplementedException();
    }

    
}