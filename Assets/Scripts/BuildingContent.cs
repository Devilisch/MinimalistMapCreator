using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingContent : MonoBehaviour
{
    [SerializeField] private Text nameText;
    [SerializeField] private Text typeText;
    [SerializeField] private Text descriptionText;
    [SerializeField] private Button button;
    [SerializeField] private Image image;

    private Action<Sprite> onButtonClicked;
    private Action onClose;

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
        onButtonClicked?.Invoke( image.sprite );
        onClose?.Invoke();
    }

    public void Init( Building info, Sprite sprite, Action<Sprite> clickAction, Action closeAction )
    {
        nameText.text = "Name: " + info.name;
        typeText.text = "Type: " + info.type;
        descriptionText.text = "Description: " + info.description;
        image.sprite = sprite;
        onButtonClicked = clickAction;
        onClose = closeAction;
    }
}
