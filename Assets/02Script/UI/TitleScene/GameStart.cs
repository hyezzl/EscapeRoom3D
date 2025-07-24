using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStart : MonoBehaviour
{
    [SerializeField] private Button startBTN;
    private Texture2D cursor2D;

    private void Awake()
    {
        if (startBTN == null) {
            Debug.Log("TitleScene - GameStart - Failed to Load Button");
            return;
        }
        cursor2D = Resources.Load<Texture2D>($"Cursor/Cursor1");
        if (cursor2D == null) Debug.Log("Failed to Load Cursor Image");
        startBTN.onClick.RemoveAllListeners();
        startBTN.onClick.AddListener(ConvertTitleToCorrider);
    }

    private void Start()
    {
        Cursor.SetCursor(cursor2D, Vector2.zero, CursorMode.Auto);
    }

    public void ConvertTitleToCorrider() {
        PlayerPrefs.SetString("NextScene", "Corrider");
        SceneManager.LoadScene("Loading");
    }
}
