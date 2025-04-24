using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace POC
{
    public enum UITheme
    {
        NONE,
        POKEMON
    }
    public class SimpleThemeUI : MonoBehaviour
    {
        public UITheme _theme; 
       
        public Texture2D pokemonBg;

        private VisualElement container;

        private Button yesButton;
        private Button noButton;

        private Label info;
        public string infoText;

        public void SetInfoText(string text)
        {
            if (info == null)
                return;
            infoText = text;
            info.text = text;
        }
        //Add logic that interacts with the UI controls in the `OnEnable` methods
        private void OnEnable()
        {
            LoadThemeUI();
        }

        private void OnDisable()
        {
            yesButton.UnregisterCallback<ClickEvent>(OnYesClicked);
            noButton.UnregisterCallback<ClickEvent>(OnNoClicked);

        }
        public void SetTheme(UITheme theme)
        {
            _theme = theme;
            if (container == null)
                return;
            switch (_theme)
            {
                case UITheme.NONE:
                    container.style.backgroundImage = null;
                    break;
                case UITheme.POKEMON:
                    container.style.backgroundImage = pokemonBg;
                    break;
                default:
                    break;
            }
        }
        public void LoadThemeUI()
        {
            // The UXML is already instantiated by the UIDocument component
            // var root = GetComponent<UIDocument>().rootVisualElement;
            UIDocument uiDocument = GetComponent<UIDocument>();

            container = uiDocument.rootVisualElement.Q("container");
            info = container.Q("info") as Label;
            VisualElement buttonContainer = container.Q("option-buttons");
            yesButton = buttonContainer.Q("yes-button") as Button;
            noButton = buttonContainer.Q("no-button") as Button;

            switch (_theme)
            {
                case UITheme.NONE:
                    container.style.backgroundImage = null;
                    break;
                case UITheme.POKEMON:                    
                    container.style.backgroundImage = pokemonBg;
                    break;
                default:
                    break;
            }
            yesButton.RegisterCallback<ClickEvent>(OnYesClicked);
            noButton.RegisterCallback<ClickEvent>(OnNoClicked);
        }       
        private void OnNoClicked(ClickEvent evt)
        {
            Debug.Log("No was clicked");
            gameObject.SetActive(false);
        }
        private void OnYesClicked(ClickEvent evt)
        {
            Debug.Log("Yes was clicked");
            gameObject.SetActive(false);
        }
    }
}
