using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropItem : Item
{
    public int addHP; //加血
    public int addMP; //加蓝
    public PropItem(int _iD, string _name, string _description, int _addHP, int _addMP, string _imageName)
    {
        type = GoodsType.Prop; //道具类型
        iD = _iD;
        name = _name;
        description = _description;
        addHP = _addHP;
        addMP = _addMP;
        imageName = _imageName;
    }
}
