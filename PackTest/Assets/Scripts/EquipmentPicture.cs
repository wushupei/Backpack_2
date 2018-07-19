using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; //调用拖拽接口需要引入的命名空间
using UnityEngine.UI;
/// <summary>
///图片被加载创建时挂在图片上
/// </summary>
public class EquipmentPicture : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Item selfItem; //用来存储属于自身的信息
    Transform original; //用于记录原位置
    GameObject dataPanel; //声明信息栏
    private void OnEnable()
    {
        transform.localPosition = Vector3.zero;
    }
    public void OnBeginDrag(PointerEventData eventData) //拖拽开始时调用一次
    {
        original = transform.parent; //记录原位置
        //直接成为Canvas的子物体,排列在所有其它其物体之下
        transform.SetParent(GameObject.Find("Canvas").transform);
    }
    public void OnDrag(PointerEventData eventData) //拖拽时持续调用
    {
        transform.position = eventData.position; //将鼠标的位置实时赋给图片
    }
    public void OnEndDrag(PointerEventData eventData) //拖拽结束时调用一次
    {
        GetIntoSlot();
    }
    void GetIntoSlot() //放入小格子
    {
        GameObject[] slots = GameObject.FindGameObjectsWithTag("Slot"); //根据标签获取所有小格子
        RectTransform slot = null;
        bool GetInto = false; //判断是否可以进入小格子
        foreach (var item in slots)
        {
            slot = item.GetComponent<RectTransform>(); //获取该小格子的RectTransform组件     
            if (slot.rect.Contains(Input.mousePosition - slot.position)) //判断鼠标是否进入该小格子             
            {
                if (slot.GetComponent<SlotType>()) //判断小格子是不是装备栏的
                {
                    if (selfItem.type == slot.GetComponent<SlotType>().slotType)//判断物品类型和格子类型是否相同
                        GetInto = true;
                }
                else //否则就是物品栏的
                    GetInto = true;
                break;
            }
        }
        if (GetInto) //如果确定能进入小格子
        {
            if (slot.transform.childCount > 0)//判断格子里面是否已有物品
            {
                Transform oriP = slot.transform.GetChild(0); //获取原物品
                oriP.SetParent(original); //将原物品放进拖拽图片的原格子
                oriP.localPosition = Vector3.zero; //居中
            }
            transform.SetParent(slot); //物品进入小格子并居中
            transform.localPosition = Vector3.zero;
            return; //退出方法
        }
        transform.SetParent(original); //返回原位置        
        transform.localPosition = Vector3.zero; //居中
    }
    public void OnPointerEnter(PointerEventData eventData) //光标进入时调用一次
    {
        //显示信息栏
        if (!dataPanel) //如果dataPanel引用为空,就去获取它
                        //DataPanel开始时隐藏状态,一般方法找不到,所以先找到父物体,通过父物体的路径去找即可
            dataPanel = GameObject.Find("Canvas").transform.Find("DataPanel").gameObject;
        dataPanel.SetActive(true); //显示
                                   //显示信息
        ShowData();
    }
    public void OnPointerExit(PointerEventData eventData) //光标移出时调用一次
    {
        dataPanel.SetActive(false); //关闭信息栏
    }
    void ShowData() //显示信息
    {
        //名字和描述显示当前物品的信息
        GameObject.Find("NameText").GetComponent<Text>().text = selfItem.name;
        GameObject.Find("DescriptionText").GetComponent<Text>().text = selfItem.description;
        switch (selfItem.type) //根据物品类型显示自身独有的信息
        {
            case GoodsType.Weapon:
                WeaponsItem weaponData = selfItem as WeaponsItem; //selfItem是Item类型,没有攻击力的字段,所以要进行转换
                ShowSelfData("攻击力:", weaponData.attack.ToString(), "暴击率:", string.Format("{0:P0}", weaponData.violent));
                break;
            case GoodsType.Clothing:
                ClothingItem clothingData = selfItem as ClothingItem;
                ShowSelfData("物理防御:", clothingData.physicalDefense.ToString(), "魔法防御:", clothingData.magicDefense.ToString());
                break;
            case GoodsType.Prop:
                PropItem propData = selfItem as PropItem;
                ShowSelfData("血量回复:", propData.addHP.ToString(), "法力回复:", propData.addMP.ToString());
                break;
        }
    }
    void ShowSelfData(string t1, string t2, string t3, string t4) //显示特有信息
    {
        GameObject.Find("Attack").GetComponent<Text>().text = t1;
        GameObject.Find("AttackText").GetComponent<Text>().text = t2;
        GameObject.Find("Violent").GetComponent<Text>().text = t3;
        GameObject.Find("ViolentText").GetComponent<Text>().text = t4;
    }
}