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

        private UIDocument uiDocument;
        private Label info;
        public string infoText;

        public void SetInfoText(string text)
        {
            infoText = text;
            if (info == null)
                return;
            info.text = text;
        }
        private void Awake()
        {
            uiDocument = GetComponent<UIDocument>();
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
        public UITheme GetTheme()
        {
            return _theme;
        }
        public string GetInfoText()
        {
            return infoText;
        }
        public void LoadThemeUI()
        {
            // The UXML is already instantiated by the UIDocument component
            // var root = GetComponent<UIDocument>().rootVisualElement;
            UIDocument uiDocument = GetComponent<UIDocument>();

            container = GetContainer();
            info = GetInfoLabel();
           
            yesButton = GetYesButton();
            noButton = GetNoButton();

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
           

            SetInfoText(infoText);
        }      
        
        public VisualElement GetContainer()
        {
            container = uiDocument.rootVisualElement.Q("container");
            if (container == null)
            {
                container = new VisualElement();
                container.name = "container";
                uiDocument.rootVisualElement.Add(container);
            }
            return container;
        }
        public Label GetInfoLabel()
        {
            info = container.Q("info") as Label;
            if (info == null)
            {
                info = new Label();
                info.name = "info";
                GetContainer().Add(info);
            }
            return info;
        }
        public VisualElement GetOptionButtonsContainer()
        {
            VisualElement buttonContainer = container.Q("option-buttons");
            if(buttonContainer == null)
            {
                buttonContainer = new VisualElement();
                buttonContainer.name = "option-buttons";
                GetContainer().Add(buttonContainer);
            }
            return buttonContainer;
        }
        public Button GetYesButton()
        {
            VisualElement buttonContainer = GetOptionButtonsContainer();
            Button yesButton = buttonContainer.Q("yes-button") as Button;
            if (yesButton == null)
            {
                yesButton = new Button();
                yesButton.name = "yes-button";
                buttonContainer.Add(yesButton);
            }
            return yesButton;
        }
        public Button GetNoButton()
        {
            VisualElement buttonContainer = GetOptionButtonsContainer();
            Button noButton = buttonContainer.Q("no-button") as Button;
            if (noButton == null)
            {
                noButton = new Button();
                noButton.name = "no-button";
                buttonContainer.Add(noButton);
            }
            return noButton;
        }
        public void RegisterButtonCallbacks()
        {
            yesButton.RegisterCallback<ClickEvent>(OnYesClicked);
            noButton.RegisterCallback<ClickEvent>(OnNoClicked);
        }
        public void OnNoClicked(ClickEvent evt)
        {
            Debug.Log("No was clicked");
            gameObject.SetActive(false);
        }
        public void OnYesClicked(ClickEvent evt)
        {
            Debug.Log("Yes was clicked");
            gameObject.SetActive(false);
        }
    }
}
