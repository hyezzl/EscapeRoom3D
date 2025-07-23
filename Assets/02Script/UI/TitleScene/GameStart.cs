using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStart : MonoBehaviour
{
    [SerializeField] private Button startBTN;

    private void Awake()
    {
        if (startBTN == null) {
            Debug.Log("TitleScene - GameStart - Failed to Load Button");
            return;
        }
        startBTN.onClick.RemoveAllListeners();
        startBTN.onClick.AddListener(ConvertTitleToCorrider);
    }

    public void ConvertTitleToCorrider() {
        PlayerPrefs.SetString("NextScene", "Corrider");
        SceneManager.LoadScene("Loading");
    }
}
