using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuildingButtonHandler : MonoBehaviour
{
    [SerializeField] BuildingObjectBase item;
    Button button;

    BuildingCreator buildingCreator;
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ButtonClicked);
        buildingCreator = BuildingCreator.GetInstance();
    }

    //обработка нажатия по текстуре, которую игрок хочет нарисовать
    private void ButtonClicked()
    {
        Debug.Log("Clicked to " + item.name);
        buildingCreator.ObjectSelected(item);
    }
}
