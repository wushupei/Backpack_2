using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowGoods : MonoBehaviour //此脚本挂在BackpackPanel
{
    List<GameObject> allGoods; //存放当前所有物品图片
    List<Transform> allSlot; //存放当前所有小格子   
    public void GetAllGoods() //拿到当前所有物品和小格子
    {
        allGoods = FindObjectOfType<CreateBackpack>().goodsList;
        allSlot = FindObjectOfType<CreateBackpack>().slots;
    }
    //分类显示(参数直接将下拉菜单Dropdown拖进去)
    public void Show(Dropdown dropdown) 
    {
        GetAllGoods(); //拿到当前全部物品和小格子
        switch (dropdown.value) //根据value判断当前选项
        {
            case 0: //显示全部
                AdaptationSlot(allGoods); 
                break;
            case 1: //只显示武器
                ClassifyShow(GoodsType.Weapon);
                break;
            case 2: //只显示衣服
                ClassifyShow(GoodsType.Clothing);
                break;
            case 3: //只显示道具
                ClassifyShow(GoodsType.Prop);
                break;
        }
    }

    void ClassifyShow(GoodsType type) //显示某种类型物品
    {
        List<GameObject> goods = new List<GameObject>(); //保存物品
        foreach (var item in allGoods)
        {
            //将满足类型条件的物品保存
            if (item.GetComponent<EquipmentPicture>().selfItem.type == type)
                goods.Add(item);
            else //不满足的就移出小格子并隐藏
            {
                item.transform.SetParent(transform);
                item.SetActive(false); 
            }
        }
        AdaptationSlot(goods); //将保存的物品依次放入小格子
    }
    void AdaptationSlot(List<GameObject> goods) //将要显示的物品依次放入小格子
    {
        for (int i = 0; i < goods.Count; i++)
        {
            goods[i].SetActive(true); //显示
            goods[i].transform.SetParent(allSlot[i]); //依次放进小格子
            goods[i].transform.localPosition = Vector3.zero; //居中
        }
    }
}
