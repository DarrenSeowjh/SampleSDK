using NUnit.Framework;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEngine.TestTools;
using POC;

public class SimpleThemeUITests
{
    private GameObject gameObject;
    private SimpleThemeUI comp;

    [SetUp]
    public void SetUp()
    {
        gameObject = new GameObject("ThemeUITest");
        gameObject.AddComponent<UIDocument>(); // Required for UI Toolkit root
        comp = gameObject.AddComponent<SimpleThemeUI>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(gameObject);
    }

    [Test]
    public void SetInfoText_UpdatesInfoFieldAndLabel()
    {
        string testText = "Testing Info Label";

        // Simulate UI binding
        var container = new VisualElement();
        var label = new Label();
        label.name = "info";
        container.Add(label);

        var root = gameObject.GetComponent<UIDocument>().rootVisualElement;
        container.name = "container";
       
        root.Add(container);

        comp.LoadThemeUI(); // Re-bind elements

        // Assign test references
        container = root.Q("container");
        var info = container.Q<Label>("info");
        comp.SetInfoText(testText);

        Assert.AreEqual(testText, comp.infoText);
        Assert.AreEqual(testText, info.text);
    }

    [Test]
    public void SetTheme_PokemonTheme_AppliesTexture()
    {
        // Setup a container and texture
        Texture2D dummyTex = new Texture2D(32, 32);
        comp.pokemonBg = dummyTex;

        var container = new VisualElement();
        container.name = "container";

        var root = gameObject.GetComponent<UIDocument>().rootVisualElement;
        container.style.backgroundImage = dummyTex;
        root.Add(container);

        comp.SetTheme(UITheme.POKEMON);

        comp.LoadThemeUI(); // Rebinds container
        Assert.AreEqual(dummyTex, container.style.backgroundImage.value.texture);
    }

    [Test]
    public void SetTheme_NoneTheme_ClearsBackgroundImage()
    {
        var container = new VisualElement();
        container.style.backgroundImage = null;
        container.name = "container";

        var root = gameObject.GetComponent<UIDocument>().rootVisualElement;
        
        root.Add(container);

        comp.SetTheme(UITheme.NONE);
        comp.LoadThemeUI();

        Assert.IsNull(container.style.backgroundImage.value.texture);
    }

    [Test]
    public void OnYesClicked_DisablesGameObject()
    {
        Assert.IsTrue(gameObject.activeSelf);
        comp.OnYesClicked(new ClickEvent());
        Assert.IsFalse(gameObject.activeSelf);
    }

    [Test]
    public void OnNoClicked_DisablesGameObject()
    {
        Assert.IsTrue(gameObject.activeSelf);
        comp.OnNoClicked(new ClickEvent());
        Assert.IsFalse(gameObject.activeSelf);
    }
}
