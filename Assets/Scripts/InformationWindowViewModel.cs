using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationWindowViewModel : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private Image image;
    [SerializeField] private Text nameText;
    [SerializeField] private Text typeText;
    [SerializeField] private Text descriptionText;

    private Action onClose;

    private void OnEnable()
    {
        closeButton.onClick.AddListener( Close );
    }

    private void OnDisable() {
        closeButton.onClick.RemoveAllListeners();
    }

    private void Close()
    {
        onClose?.Invoke();
        GameObject.Destroy( gameObject );
    }

    public void Init( Sprite sprite, Building building, Action onResult )
    {
        image.sprite = sprite;
        nameText.text = "Name: " + building.name;
        typeText.text = "Type: " + building.type;
        descriptionText.text = "Description:\n" + building.description;
        onClose = onResult;
    }
}
