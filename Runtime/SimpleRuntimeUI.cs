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
    public class SimpleRuntimeUI : MonoBehaviour
    {
        private Button _button;
        private Toggle _toggle;

        private int _clickCount;

        public UITheme _theme;
        private string inputStr;

        //Add logic that interacts with the UI controls in the `OnEnable` methods
        private void OnEnable()
        {
            LoadThemeUI();
        }

        private void OnDisable()
        {
            _button.UnregisterCallback<ClickEvent>(PrintClickMessage);
        }

        private void PrintClickMessage(ClickEvent evt)
        {
            Debug.Log(inputStr);
            SampleAPI.PrintAPIMessage();
        }

        public void InputMessage(ChangeEvent<string> evt)
        {
            inputStr = evt.newValue;
        }

        public void LoadThemeUI()
        {
            // The UXML is already instantiated by the UIDocument component
            var root = GetComponent<UIDocument>().rootVisualElement;

            switch (_theme)
            {
                case UITheme.NONE:
                    break;
                case UITheme.POKEMON:                    
                    root.style.backgroundImage = AssetDatabase.LoadAssetAtPath<Texture2D>("Packages/com.poc.samplesdk/Assets/Pikachu.jpg");
                    break;
                default:
                    break;
            }

            _toggle = new Toggle("Display Counter?");
            root.Add(_toggle);

           
            
            var _inputFields = new TextField();
            _inputFields.RegisterCallback<ChangeEvent<string>>(InputMessage);
            root.Add(_inputFields);

            _button = CreateButton(root);
            _button.RegisterCallback<ClickEvent>(PrintClickMessage);
        }
        private Button CreateButton(VisualElement root)
        {
            Button button = new Button();

            button.text = "This is a button";
            button.style.width = new Length(50, LengthUnit.Percent);
            button.style.alignSelf = Align.Center;
            root.Add(button);

            return button;
        }
    }
}
