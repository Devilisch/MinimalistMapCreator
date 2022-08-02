using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BuildWindowViewModel : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private VerticalLayoutGroup content;
    [SerializeField] private BuildingContent buildingContent;

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

    public void Init( Buildings buildings, Action onResult, Action<Sprite> action )
    {
        foreach ( var building in buildings.BuildingsArray )
        {
            BuildingContent newContent = Instantiate( buildingContent, content.transform );
            newContent.Init( building, buildings.GetSprite( building.name ), action, Close );
        }

        onClose = onResult;
    }
}
