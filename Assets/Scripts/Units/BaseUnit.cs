using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class BaseUnit
{
    public static int nextId = 1;
    public enum UnitType
    {
        Character,Equipment,Magic,Accessory
    }
    public string objectNumber;//物品编号（表示物品是什么）
    /// <summary>
    /// 物品ID，用于区分相同的物品，从possesseingObjects查找
    /// </summary>
    public int id;
    /// <summary>
    /// 名称
    /// </summary>
    public string name;
    public UnitType unitType;//类别
    public int level;//等级
    public int health;//体力
    public float moveSpeed;//速度
    public float maxMoveSpeed;
    public float accelerate;//加速度
    public int attackK;//攻击力参数K
    public int attackB;//攻击力参数B
    public int defendK;//防御力参数K
    public int defendB;//防御力参数B
    public int maxHealthK;
    public int maxHealthB;
    public int Attack//计算后的攻击力
    {
        get
        {
            int a = attackK * level + attackB;
            if (equipment1 != null)
                a += equipment1.Attack;
            if (equipment2 != null)
                a += equipment2.Attack;
            return a;
        }
    }
    public int Defend//计算后的防御力
    {
        get
        {
            int a = defendK * level + defendB;
            if (equipment1 != null)
                a += equipment1.Defend;
            if (equipment2 != null)
                a += equipment2.Defend;
            return a;
        }
    }
    public int MaxHealth
    {
        get
        {
            int a = maxHealthK * level + maxHealthB;
            if (equipment1 != null)
                a += equipment1.MaxHealth;
            if (equipment2 != null)
                a += equipment2.MaxHealth;
            return a;
        }
    }
    public int coinLevel;//被爆金币或经验时的倍率
    
    public string[] imagePathsIdle;//静止图片状态
    public string[] imagePathsRunning;//运动图片状态
    public bool isSolid;//是否实体
    public Rect boundingBox;//碰撞盒
    public Sprite iconSprite;
    public BaseUnit equipment1, equipment2;//装备物1,2

    public BaseUnit(string _objectNumber="",string _name="",UnitType _type=UnitType.Character,int _level=1,float _maxMoveSpeed=0.01f,float _accelerate=0.005f,
        int _attackK=1,int _attackB=10,int _defendK=1,int _defendB=3,int _maxHealthK=1,int _maxHealthB=20,int _coinLevel=1)
    {
        id = nextId;
        nextId++;
        objectNumber = _objectNumber;
        name = _name;
        level = _level;
        maxMoveSpeed = _maxMoveSpeed;
        accelerate = _accelerate;
        attackK = _attackK;
        attackB = _attackB;
        defendK = _defendK;
        defendB = _defendB;
        maxHealthK = _maxHealthK;
        maxHealthB = _maxHealthB;
        coinLevel = _coinLevel;
        health = MaxHealth;
        moveSpeed = 0;
        isSolid = true;
        boundingBox = new Rect();
        equipment1 = null;
        equipment2 = null;
        //TODO
        iconSprite = null;//??? 从参数获取
    }

    public UnityEvent<int> onUseHoldingObject;//参数为使用的道具索引
    public UnityEvent<GameObject> onCollisionObject;

    public void UseHoldingObject(int index)
    {
        //
    }

    public void CosumeSelf()
    {
        //
    }

    public void AttackTo(BaseUnit other)
    {
        //
    }

    public void ReceiveAttackFrom(BaseUnit from)
    {
        //
    }
}
