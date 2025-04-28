using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class UnitData
{
    public List<BaseUnit> defaultCharacterData = GetDefaultCharacterData();
    public List<BaseUnit> defaultEquipmentData = GetDefaultEquipmentData();

    //生成单位数据
    public static List<BaseUnit> GetDefaultCharacterData()
    {
        List<BaseUnit> units = new List<BaseUnit>();
        units.Add(new BaseUnit("100", "Youmi",BaseUnit.UnitType.Character, 1, 0.01f, 0.002f, 1, 10, 1, 3, 2, 20, 1));
        units.Add(new BaseUnit("101", "Utane",BaseUnit.UnitType.Character, 1, 0.012f, 0.002f, 1, 8, 1, 5, 2, 20, 1));
        return units;
    }

    public static List<BaseUnit> GetDefaultEquipmentData()
    {
        List<BaseUnit> units = new List<BaseUnit>();
        units.Add(new BaseUnit("200", "Gloves", BaseUnit.UnitType.Equipment, 1, 0,0, 0, 10, 0, 0, 0, 0));
        units.Add(new BaseUnit("201", "Shield", BaseUnit.UnitType.Equipment, 1, 0, 0, 0, 0, 0, 3, 0, 0));
        return units;
    }

    public BaseUnit GetCharacterById(int _id)
    {
        return defaultCharacterData.Find(o => o.id == _id);
    }

    public BaseUnit GetEquipmentById(int _id)
    {
        return defaultEquipmentData.Find(o => o.id == _id);
    }
}

