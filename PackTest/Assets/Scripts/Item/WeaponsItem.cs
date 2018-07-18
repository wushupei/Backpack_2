using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsItem : Item
{
    public int attack { get; set; } //攻击力
    public float violent { get; set; } //暴击率
    public WeaponsItem(int _iD, string _name, string _description, int _attack, float _violent, string _imageName)
    {
        type = GoodsType.Weapon; //武器类型
        iD = _iD;
        name = _name;
        description = _description;
        attack = _attack;
        violent = _violent;
        imageName = _imageName;
    }
}
