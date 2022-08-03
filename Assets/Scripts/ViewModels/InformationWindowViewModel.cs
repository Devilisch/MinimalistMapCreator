using System;
using UnityEngine;
using UnityEngine.UI;

public class InformationWindowViewModel : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private Image image;
    [SerializeField] private Text nameText;
    [SerializeField] private Text typeText;
    [SerializeField] private Text descriptionText;
    [SerializeField] private GameObject window;

    private void Start()
    {
        window.transform.localScale = Vector3.zero;
        LeanTween.scale( window, Vector3.one, 0.2f );
    }

    public Action CloseAction { private get; set; }

    private void OnEnable()
    {
        closeButton.onClick.AddListener( CloseButtonClicked );
    }

    private void OnDisable()
    {
        closeButton.onClick.RemoveAllListeners();
    }

    private void CloseButtonClicked()
    {
        LeanTween.scale( window, Vector3.zero, 0.2f ).setOnComplete( Close );
    }

    private void Close()
    {
        CloseAction?.Invoke();
        GameObject.Destroy( gameObject );
    }

    public void Init( Sprite sprite, Building building )
    {
        image.sprite         = sprite;
        nameText.text        = "Name: " + building.name;
        typeText.text        = "Type: " + building.type;
        descriptionText.text = "Description:\n" + building.description;
    }
}
