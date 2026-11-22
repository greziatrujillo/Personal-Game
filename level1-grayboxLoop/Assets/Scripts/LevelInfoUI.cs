using UnityEngine;
using TMPro;

/// <summary>
/// Managing level info that pops up at the beginning of a level
/// </summary>
public class LevelInfoUI : MonoBehaviour
{
    [SerializeField] private GameObject levelIntro;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    [SerializeField] private string levelTitle = "Level 1 - Cin";

    [TextArea(2, 5)]
    [SerializeField] private string levelDescription = "This character has slow falling. When jumping, you will automatically fall slower, allowing you to glide and land safely.";

    private static string lastShownScene;

    void Start()
    {

        string current = gameObject.scene.name;
        if (lastShownScene == current)
        {
            levelIntro.SetActive(false);
            return;
        }
        lastShownScene = current;

        titleText.text = levelTitle;
        descriptionText.text = levelDescription;

        levelIntro.SetActive(true);
        Time.timeScale = 0f;
    }

    public void PressContinue()
    {
        levelIntro.SetActive(false);
        Time.timeScale = 1f;
    }
}
