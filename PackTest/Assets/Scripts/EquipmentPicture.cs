using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; //调用拖拽接口需要引入的命名空间
/// <summary>
///图片被加载创建时挂在图片上
/// </summary>
public class EquipmentPicture : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private void OnEnable()
    {
        transform.localPosition = Vector3.zero;
    }
    public Item selfItem; //用来存储属于自身的信息
    Transform original; //用于记录原位置
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
        //获取所有小格子
        List<Transform> slots = FindObjectOfType<CreateBackpack>().slots;
        for (int i = 0; i < slots.Count; i++)
        {
            //获取当前小格子 
            RectTransform slot = slots[i].GetComponent<RectTransform>();
            //如果鼠标进入该小格子的范围
            if (slot.rect.Contains(Input.mousePosition - slot.position))
            {
                //如果小格子里已经有图片,则该图片放入被拖图片的原位置并居中
                if (slots[i].childCount > 0)
                {
                    Transform oriP = slots[i].GetChild(0); //获取原图片
                    oriP.SetParent(original);
                    oriP.localPosition = Vector3.zero;
                }
                //将被拖图片放入该小格子并居中
                transform.SetParent(slots[i]);
                transform.localPosition = Vector3.zero;
                return; //结束该方法的调用
            }
        }
        transform.SetParent(original); //返回原位置        
        transform.localPosition = Vector3.zero; //居中
    }
    
}