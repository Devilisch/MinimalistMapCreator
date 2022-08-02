using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingWindowViewModel : MonoBehaviour
{
    [SerializeField] private Button deleteButton;
    [SerializeField] private Button moveButton;
    [SerializeField] private Button infoButton;
    [SerializeField] private Button closeButton;

    private Action onDelete;
    private Action onMove;
    private Action onInfo;
    private Action onClose;

    private void OnEnable() {
        deleteButton.onClick.AddListener( DeleteButtonClicked );
        moveButton.onClick.AddListener( MoveButtonClicked );
        infoButton.onClick.AddListener( InfoButtonClicked );
        closeButton.onClick.AddListener( CloseButtonClicked );
    }

    private void OnDisable() {
        deleteButton.onClick.RemoveAllListeners();
        moveButton.onClick.RemoveAllListeners();
        infoButton.onClick.RemoveAllListeners();
        closeButton.onClick.RemoveAllListeners();
    }

    private void DeleteButtonClicked()
    {
        onDelete?.Invoke();
        CloseButtonClicked();
    }

    private void MoveButtonClicked()
    {
        onMove?.Invoke();
        CloseButtonClicked();
    }

    private void InfoButtonClicked()
    {
        onInfo?.Invoke();
        CloseButtonClicked();
    }

    private void CloseButtonClicked()
    {
        onClose?.Invoke();
        GameObject.Destroy( gameObject );
    }

    public void Init( Action onResult, Action deleteAction, Action moveAction, Action infoAction )
    {
        onClose = onResult;
        onDelete = deleteAction;
        onMove = moveAction;
        onInfo = infoAction;
    }
}
