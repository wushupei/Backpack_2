using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothingItem : Item
{
    public int physicalDefense { get; set; } //物理防御
    public int magicDefense { get; set; } //魔法防御
    public ClothingItem(int _iD, string _name, string _description, int _physicalDefense, int _magicDefense, string _imageName)
    {
        type = GoodsType.Clothing; //衣服类型
        iD = _iD;
        name = _name;
        description = _description;
        physicalDefense = _physicalDefense;
        magicDefense = _magicDefense;
        imageName = _imageName;
    }
}
