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
    public string objectNumber;//��Ʒ��ţ���ʾ��Ʒ��ʲô��
    /// <summary>
    /// ��ƷID������������ͬ����Ʒ����possesseingObjects����
    /// </summary>
    public int id;
    /// <summary>
    /// ����
    /// </summary>
    public string name;
    public UnitType unitType;//���
    public int level;//�ȼ�
    public int health;//����
    public float moveSpeed;//�ٶ�
    public float maxMoveSpeed;
    public float accelerate;//���ٶ�
    public int attackK;//����������K
    public int attackB;//����������B
    public int defendK;//����������K
    public int defendB;//����������B
    public int maxHealthK;
    public int maxHealthB;
    public int Attack//�����Ĺ�����
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
    public int Defend//�����ķ�����
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
    public int coinLevel;//������һ���ʱ�ı���
    
    public string[] imagePathsIdle;//��ֹͼƬ״̬
    public string[] imagePathsRunning;//�˶�ͼƬ״̬
    public bool isSolid;//�Ƿ�ʵ��
    public Rect boundingBox;//��ײ��
    public Sprite iconSprite;
    public BaseUnit equipment1, equipment2;//װ����1,2

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
        iconSprite = null;//??? �Ӳ�����ȡ
    }

    public UnityEvent<int> onUseHoldingObject;//����Ϊʹ�õĵ�������
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
