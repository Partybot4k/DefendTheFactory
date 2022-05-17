using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePickerUI : MonoBehaviour
{
    // Start is called before the first frame update
    public UIManager uiManager;
    public Vector3 pickerOffset;
    public float pickerPadding;
    public MousePickerSlot pickerPrefab;
    public List<MousePickerSlot> pickerSlots = new List<MousePickerSlot>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         transform.localPosition = Input.mousePosition + pickerOffset;
    }

    public void UpdatePickerUI(List<ItemStack> pickerCollectiblesList)
    {
        ClearSlotList();
        if (pickerCollectiblesList.Count > 0)
        {
            foreach (ItemStack item in pickerCollectiblesList)
            {
                MousePickerSlot newSlot = Instantiate(pickerPrefab, pickerOffset, Quaternion.identity);
                newSlot.transform.position = new Vector3(0.0f, 0.0f, transform.position.z);
                newSlot.transform.SetParent(transform);
                newSlot.UpdateGraphic(item);
                pickerSlots.Add(newSlot);
            }
        }
    }

    void ClearSlotList()
    {
        foreach (MousePickerSlot slot in pickerSlots){
            Destroy(slot.gameObject);
        }
        pickerSlots.Clear();
    }
}
