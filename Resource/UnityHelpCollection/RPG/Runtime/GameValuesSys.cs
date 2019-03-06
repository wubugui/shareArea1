﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameValuesSys : MonoBehaviour {

	public int healthLimit { get; set; }
    public int magicLimit { get; set; }
    /// <summary>
    /// 当前生命魔法值
    /// </summary>
    public int HP;
    public int MP;
    public float physicalResistance { get; set; }
    public float magicResistance { get; set; }
    public int attackValue { get; set; }
    public int attackSpeed { get; set; }
    /// <summary>
    /// 法术强度
    /// </summary>
    public float spellPower { get; set; }
    public float CritRate { get; set; }
    public float dodgeRate { get; set; }
    public int hpRestoreSpeed { get; set; }
    public int mpRestoreSpeed { get; set; }
    public int CritcalMult { get; set; }

    private WaitForSeconds restoreDelta = new WaitForSeconds(1);

    private IEnumerator Restore()
    {
        RestoreValue(ref HP, hpRestoreSpeed, healthLimit);
        RestoreValue(ref MP, mpRestoreSpeed, magicLimit);
        yield return restoreDelta;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value">当前值</param>
    /// <param name="restore">回复值</param>
    /// <param name="limit">上限值</param>
    private void RestoreValue(ref int value,int restore,int limit)
    {
        if (value < limit)
        {
            if (value + restore <= limit) value += restore;
            else value = limit;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <returns>返回false代表闪避成功</returns>
    public bool HurtByPhysical(int value)
    {
        if (dodgeRate >= Random.value) return false;
        HP -= Mathf.FloorToInt(value * physicalResistance);
        return true;
    }

    public void HurtByMagic(int value)
    {
        MP -= Mathf.FloorToInt(value * magicResistance);
    }

    public int givePhyDamage()
    {
        if(CritRate >= Random.value)
        {
            return CritcalMult * attackValue;
        }
        return attackValue;
    }

    public int giveMagicDamage(int baseValue) { return Mathf.FloorToInt(baseValue * spellPower); }
}
