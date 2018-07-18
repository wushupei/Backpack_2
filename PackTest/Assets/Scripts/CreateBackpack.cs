using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
/// <summary>
/// 挂GoosPanel上
/// </summary>
public class CreateBackpack : MonoBehaviour
{
    ItemData itemData; //声明ItemData
    public List<Transform> slots; //声明一个集合存放所有小格子

    private void Start()
    {
        //创建ItemData的实例,才可以调用它的方法
        itemData = FindObjectOfType<ItemData>();
        GetAllSlot();
    }
    public GameObject CreateImage(int iD) //创建图片
    {
        //根据ID获取数据
        Item item = itemData.AccordingToIDGetData(iD);
        GameObject image = null;
        switch (item.type) //判断物品类型,去往不同路径加载预制件
        {
            case GoodsType.Weapon:
                image = Instantiate(Resources.Load("Prefabs/Weapons/" + item.imageName) as GameObject);
                break;
            case GoodsType.Clothing:
                image = Instantiate(Resources.Load("Prefabs/Clothings/" + item.imageName) as GameObject);
                break;
            case GoodsType.Prop:
                image = Instantiate(Resources.Load("Prefabs/Props/" + item.imageName) as GameObject);
                break;
        }
        image.AddComponent<EquipmentPicture>().selfItem = item;//添加脚本并储存自身信息 
        return image; //返回图片
    }
    //声明集合存放所有已经实例化的物品图片
    public List<GameObject> goodsList = new List<GameObject>();
    public void CreateImageInSlot(int iD) //将图片放进空格里
    {
        if (goodsList.Count < slots.Count) //当物品数量小于格子数量时
        {
            GameObject image = CreateImage(iD); //创建物品图片
            goodsList.Add(image); //将创建的物品图片放进物品集合中
            for (int i = 0; i < transform.childCount; i++) //遍历所有小方格
            {
                if (transform.GetChild(i).childCount == 0) //如果小方格没有子物体,表示该小方格为空
                {                  
                    image.transform.SetParent(transform.GetChild(i)); //将图片放进该小方格中
                    image.transform.localPosition = Vector3.zero; //居中
                    return; //放进去后退出整个方法
                }
            }         
        }
        print("物品栏已满"); 
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
            CreateImageInSlot(1001);
        if (Input.GetKeyDown(KeyCode.Alpha1))
            CreateImageInSlot(1002);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            CreateImageInSlot(1003);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            CreateImageInSlot(2001);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            CreateImageInSlot(2002);
        if (Input.GetKeyDown(KeyCode.Alpha5))
            CreateImageInSlot(2003);
        if (Input.GetKeyDown(KeyCode.Alpha6))
            CreateImageInSlot(3001);
        if (Input.GetKeyDown(KeyCode.Alpha7))
            CreateImageInSlot(3002);
        if (Input.GetKeyDown(KeyCode.Alpha8))
            CreateImageInSlot(3003);
        if (Input.GetKeyDown(KeyCode.Alpha9))
            CreateImageInSlot(3004);
    }
    
    void GetAllSlot() //获取所有的GoodsPanel下所有的小格子
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            slots.Add(transform.GetChild(i));
        }
    }
}
