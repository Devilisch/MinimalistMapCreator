using System;
using UnityEngine;
using UnityEngine.UI;

public class BuildWindowViewModel : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private VerticalLayoutGroup content;
    [SerializeField] private BuildWindowContentViewModel buildWindowContent;
    [SerializeField] private GameObject window;

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

    public void Init( Buildings buildings, Action<Sprite> ButtonClickAction )
    {
        window.transform.localScale = Vector3.zero;
        LeanTween.scale( window, Vector3.one, 0.2f ).setOnComplete( () => {
            foreach ( var building in buildings.BuildingsData )
            {
                BuildWindowContentViewModel newBuildingContent = Instantiate( buildWindowContent, content.transform );

                newBuildingContent.Init( building, buildings.GetSprite( building.name ) );
                newBuildingContent.ButtonClickAction = ButtonClickAction;
                newBuildingContent.CloseAction       = CloseButtonClicked;
            }
            }
        );
    }
}
