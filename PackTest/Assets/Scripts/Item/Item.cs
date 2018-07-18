using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GoodsType
{
    Weapon, //武器
    Clothing, //衣服
    Prop, //道具
}
public class Item
{
    public GoodsType type; //枚举
    public int iD { get; set; } //ID
    public string name { get; set; } //名字
    public string description { get; set; } //描述
    public string imageName { get; set; } //图片名字          
}
