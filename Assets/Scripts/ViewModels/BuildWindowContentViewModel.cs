using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildWindowContentViewModel : MonoBehaviour
{
    [SerializeField] private Text nameText;
    [SerializeField] private Text typeText;
    [SerializeField] private Text descriptionText;
    [SerializeField] private Button button;
    [SerializeField] private Image image;

    public Action<Sprite> ButtonClickAction { private get; set; }
    public Action CloseAction { private get; set; }

    private void OnEnable()
    {
        button.onClick.AddListener( ButtonClicked );
    }

    private void OnDisable()
    {
        button.onClick.RemoveAllListeners();
    }

    private void ButtonClicked()
    {
        ButtonClickAction?.Invoke( image.sprite );
        CloseAction?.Invoke();
    }

    public void Init( Building building, Sprite sprite )
    {
        image.sprite         = sprite;
        nameText.text        = "Name: " + building.name;
        typeText.text        = "Type: " + building.type;
        descriptionText.text = "Description: " + building.description;
    }
}
