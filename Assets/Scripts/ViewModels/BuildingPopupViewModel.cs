using System;
using UnityEngine;
using UnityEngine.UI;

public class BuildingPopupViewModel : MonoBehaviour
{
    [SerializeField] private Button deleteButton;
    [SerializeField] private Button moveButton;
    [SerializeField] private Button infoButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private GameObject window;
    [SerializeField] private GameObject targetRing;

    public Action DeleteAction { private get; set; }
    public Action MoveAction { private get; set; }
    public Action ShowInfoAction { private get; set; }
    public Action CloseAction { private get; set; }

    private void Start()
    {
        window.transform.localScale = Vector3.zero;
        targetRing.transform.localScale = Vector3.zero;

        LeanTween.scale( targetRing, Vector3.one, 0.2f ).setOnComplete( () => {
            LeanTween.scale( window, Vector3.one, 0.2f );
        });
    }

    private void OnEnable()
    {
        deleteButton.onClick.AddListener( DeleteButtonClicked );
        moveButton.onClick.AddListener( MoveButtonClicked );
        infoButton.onClick.AddListener( InfoButtonClicked );
        closeButton.onClick.AddListener( CloseButtonClicked );
    }

    private void OnDisable()
    {
        deleteButton.onClick.RemoveAllListeners();
        moveButton.onClick.RemoveAllListeners();
        infoButton.onClick.RemoveAllListeners();
        closeButton.onClick.RemoveAllListeners();
    }

    private void DeleteButtonClicked()
    {
        DeleteAction?.Invoke();
        CloseButtonClicked();
    }

    private void MoveButtonClicked()
    {
        MoveAction?.Invoke();
        CloseButtonClicked();
    }

    private void InfoButtonClicked()
    {
        ShowInfoAction?.Invoke();
        CloseButtonClicked();
    }

    private void CloseButtonClicked()
    {
        LeanTween.scale( window, new Vector3( 1.0f, 0.0f, 1.0f ), 0.2f ).setOnComplete( () => {
            LeanTween.scale( targetRing, Vector3.zero, 0.2f ).setOnComplete( Close );
        });
    }

    private void Close()
    {
        CloseAction?.Invoke();
        GameObject.Destroy( gameObject );
    }
}
