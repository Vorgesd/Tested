using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TestTools;

public class OffMusicTests
{
    private GameObject testGameObject;
    private offmusic offMusicScript;
    private Button testButton;
    private GameObject testImageObject;

    [SetUp]
    public void SetUp()
    {
        // Создаем объект для тестирования и добавляем компоненты
        testGameObject = new GameObject();

        // Добавляем кнопку и объект изображения
        testButton = testGameObject.AddComponent<Button>();
        testImageObject = new GameObject();
        testImageObject.AddComponent<Image>();

        // Добавляем скрипт offmusic
        offMusicScript = testGameObject.AddComponent<offmusic>();

        // Устанавливаем связи с объектами
        offMusicScript.toggleButton = testButton;
        offMusicScript.imageObject = testImageObject;

        // Проверяем, что объект изображения изначально скрыт
        Assert.IsFalse(testImageObject.activeSelf, "Image should be inactive at the start.");
    }

    [TearDown]
    public void TearDown()
    {
        // Очищаем созданные объекты после каждого теста
        GameObject.Destroy(testGameObject);
        GameObject.Destroy(testImageObject);
    }

    [Test]
    public void ToggleImageVisibility_TogglesVisibility_WhenButtonIsClicked()
    {
        // Добавляем слушателя на кнопку
        testButton.onClick.AddListener(() => offMusicScript.ToggleImageVisibility());

        // Имитируем клик по кнопке
        testButton.onClick.Invoke();

        // Проверяем, что изображение стало активным
        Assert.IsTrue(testImageObject.activeSelf, "Image should be active after button click.");

        // Имитируем еще один клик по кнопке
        testButton.onClick.Invoke();

        // Проверяем, что изображение снова скрыто
        Assert.IsFalse(testImageObject.activeSelf, "Image should be inactive after second button click.");
    }

    [Test]
    public void ToggleImageVisibility_InitialVisibilityIsFalse()
    {
        // Проверяем, что изображение по умолчанию не видно
        Assert.IsFalse(testImageObject.activeSelf, "Image should be initially inactive.");
    }

    [Test]
    public void ToggleImageVisibility_ChangesStateOnMultipleClicks()
    {
        // Имитируем несколько кликов по кнопке
        testButton.onClick.Invoke();
        Assert.IsTrue(testImageObject.activeSelf, "Image should be visible after first click.");

        testButton.onClick.Invoke();
        Assert.IsFalse(testImageObject.activeSelf, "Image should be hidden after second click.");

        testButton.onClick.Invoke();
        Assert.IsTrue(testImageObject.activeSelf, "Image should be visible after third click.");
    }
}

