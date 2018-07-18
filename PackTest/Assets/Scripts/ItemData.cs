using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
/// <summary>
/// 随便挂(挂摄像机上了)
/// </summary>
public class ItemData : MonoBehaviour
{
    JsonData jsonData; //引用了LitJson才能使用这个类
    List<Item> itemList = new List<Item>(); //声明一个集合来保存所有json数据
    private void Awake()
    {
        readJson(); //读取
        SaveItemData(); //存储
    }
    public void readJson()
    {
        //读取本地Json文件内容,储存进jsonData中
        jsonData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/物品数据.json"));
    }
    void SaveItemData()
    {
        for (int i = 0; i < jsonData.Count; i++)
        {
            int iD = (int)jsonData[i]["ID"]; //获取当前数据的ID
            switch (iD / 1000) //判断ID的第一位数字
            {
                case 1: //如果是1,将所有武器数据放进数组中
                    itemList.Add(new WeaponsItem((int)jsonData[i]["ID"], jsonData[i]["Name"].ToString(),
                    jsonData[i]["Description"].ToString(), (int)jsonData[i]["Attack"],
                    float.Parse(jsonData[i]["Violent"].ToString()), jsonData[i]["ImageName"].ToString()));
                    break;
                case 2: //如果是2,将所有衣服数据放进数组中
                    itemList.Add(new ClothingItem ((int)jsonData[i]["ID"], jsonData[i]["Name"].ToString(),
                    jsonData[i]["Description"].ToString(), (int)jsonData[i]["PhysicalDefense"],
                    (int)jsonData[i]["MagicDefense"], jsonData[i]["ImageName"].ToString()));
                    break;
                case 3: //如果是3,将所有道具数据放进数组中
                    itemList.Add(new PropItem((int)jsonData[i]["ID"], jsonData[i]["Name"].ToString(),
                    jsonData[i]["Description"].ToString(), (int)jsonData[i]["AddHP"],
                    (int)jsonData[i]["AddMP"], jsonData[i]["ImageName"].ToString()));
                    break;
            }
        }
    }
    public Item AccordingToIDGetData(int _iD) //根据ID拿取数据
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            if (_iD == itemList[i].iD) //如果有这个ID
            {
                return itemList[i]; //返回该ID的数据
            }
        }
        return null; //没有则返回空
    }
}
