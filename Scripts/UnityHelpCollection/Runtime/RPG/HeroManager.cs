﻿using System;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameValuesSys), typeof(AttributeSys))]
public class HeroManager : MonoBehaviour, ModelInterface
{
    public event Action<float> HpChanged;
    public event Action<float> MpChanged;
    public event Action<int, string> SkillChanged;
    public event Action<int> LevelChanged;
    public event Action<float> ExperienceChanged;
    public event Action<EquipmentType, string> EquipChanged;
    public event Action<int, int, string> PackageChanged;
    public event Action AttributeChanged;

    public GameValuesSys valuesSys;
    public AttributeSys attrSys;

    private Equipment[,] packages = new Equipment[8, 8];
    private Dictionary<EquipmentType, Equipment> equips = new Dictionary<EquipmentType, Equipment>();

    private float expPoint = 0.0f;
    private int leftAttrPoint = 10; //人物属性点
    public int LeftAttrPoint { get { return leftAttrPoint; } }

    void Awake()
    {
        valuesSys = GetComponent<GameValuesSys>();
        attrSys = GetComponent<AttributeSys>();

        //初始化字典
        foreach (EquipmentType v in Enum.GetValues(typeof(EquipmentType)))
        {
            equips[v] = null;
        }
    }

    void Start()
    {
        valuesSys.HpChanged.AddListener(delegate (float value) { HpChanged(value); });
        valuesSys.MpChanged.AddListener(delegate (float value) { MpChanged(value); });
    }

    public void SetEquip(EquipmentType EquipType, string equipID)
    {
        if (equipID == "-1"&&equips[EquipType]!=null)
        {
            equips[EquipType].UnEquip(attrSys,valuesSys);
            equips[EquipType] = null;
            EquipChanged(EquipType, equipID);
        }
        var s = Resources.Load<Equipment>(Path.respDataEquip + equipID);
        if (s != null)
        {
            equips[EquipType] = s;
            equips[EquipType].Equip(attrSys, valuesSys);
            EquipChanged(EquipType, equipID);
        }
    }

    public void NextFreeSlot(ref int row,ref int line)
    {
        for(int i = 0; i < packages.GetLength(0); i++)
        {
            for(int j = 0; j < packages.GetLength(1); j++)
            {
                if (packages[i, j] == null) { row = i; line = j; return; }
            }
        }
    }

    public void SetPackage(int row, int line, string equipID)
    {
        if (equipID == "-1")
        {
            packages[row, line] = null;
            PackageChanged(row, line, equipID);
        }

        var s = Resources.Load<Equipment>(Path.respDataEquip + equipID);

        if (s != null)
        {
            s.AddEquips();
            packages[row, line] = s;
            PackageChanged(row, line, equipID);
        }
    }

    public void SetSkill(int slotNumber, string skillID)
    {
        if (slotNumber < 0 || slotNumber > 5) { Debug.LogError("index error"); return; }
        if (skillID == "-1") { }
    }

    public bool AddAttribtePoint(ValuesType valuesType)
    {
        if (leftAttrPoint <= 0) return false;
        switch (valuesType)
        {
            case ValuesType.strength:
                leftAttrPoint -= 1;
                attrSys.AddStrength(1);
                return true;
            case ValuesType.agile:
                leftAttrPoint -= 1;
                attrSys.AddAgile(1);
                return true;
            case ValuesType.intellgence:
                leftAttrPoint -= 1;
                attrSys.AddIntelligence(1);
                return true;
            case ValuesType.physique:
                leftAttrPoint -= 1;
                Debug.Log("happen");
                attrSys.AddPhysique(1);
                return true;
            case ValuesType.lucky:
                leftAttrPoint -= 1;
                attrSys.AddLucky(1);
                return true;
            default:
                Debug.LogError("error");
                return false;
        }
    }
}
