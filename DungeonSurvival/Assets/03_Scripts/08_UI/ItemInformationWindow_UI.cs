using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemInformationWindow_UI : MonoBehaviour
{

    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemDescriptionText;
    [SerializeField] RectTransform itemInfoUI_Container;
    [SerializeField] private ItemInformationTextContainer textContainer;

    public void CheckItemInformation ( Item item )
    {
        //if(item.TryGetAction(out ItemAction action))
        //{
        //    if(action is HealingItemAction)
        //    {
        //        HealingItemAction healingItemAction = (HealingItemAction)action;
        //        ItemInformationTextContainer healPointsValue = Instantiate(textContainer, itemInfoUI_Container.transform);
        //        ItemInformationTextContainer healSpeedValue = Instantiate(textContainer, itemInfoUI_Container.transform);
        //        healPointsValue.textValue.text = healingItemAction.healPoints.ToString();
        //        healSpeedValue.textValue.text = healingItemAction.healingFillTime.ToString();
        //    }
        //    else if (action is FoodItemAction)
        //    {
        //        FoodItemAction foodItemAction = (FoodItemAction)action;
        //        ItemInformationTextContainer hungerRecoveringValue = Instantiate(textContainer, itemInfoUI_Container.transform);
        //        ItemInformationTextContainer feedSpeedValue = Instantiate(textContainer, itemInfoUI_Container.transform);
        //        hungerRecoveringValue.textValue.text = foodItemAction.hungerPoints.ToString();
        //        feedSpeedValue.textValue.text = foodItemAction.eatFillTime.ToString();
        //    }
        //    else if (action is StatBoostItemAction)
        //    {
        //        StatBoostItemAction boostItemAction = (StatBoostItemAction)action;

        //        ItemInformationTextContainer statPoints = Instantiate(textContainer, itemInfoUI_Container.transform);

        //        statPoints.textValue.text = boostItemAction.statPoints.ToString();
        //    }
        //    else if (action is RepairItemAction)
        //    {
        //        RepairItemAction repairItemAction = (RepairItemAction)action;
        //        ItemInformationTextContainer repairingQuantityValue = Instantiate(textContainer, itemInfoUI_Container.transform);
        //        ItemInformationTextContainer repairingSpeed = Instantiate(textContainer, itemInfoUI_Container.transform);

        //        repairingQuantityValue.textValue.text = repairItemAction.repairingPoints.ToString();
        //        repairingSpeed.textValue.text = repairItemAction.repairingFillTime.ToString();
        //    }
            //else if(action is HealingItemAction)
            //{
            //MagicItemAction TENGO QUE COMPLETAR ESA CLASE
            //}
        //}
    }
    private void SetItemName ( Item item )
    {
        itemNameText.text = item.displayName;
    }
    private void SetItemDescription ( Item item )
    {
        itemDescriptionText.text = item.description;
    }
    private void SetItemIcon ( Item item )
    {
        itemIcon.transform.rotation = Quaternion.Euler(0, 0, item._iconRotationZ);
        itemIcon.transform.localScale = new Vector3(item._iconScale, item._iconScale, item._iconScale);
        itemIcon.sprite = item.icon;
    }
    public void SetItemInformation ( Item item )
    {
        SetItemName(item);
        SetItemDescription(item);
        SetItemIcon(item);
        CheckItemInformation(item);
    }
}
