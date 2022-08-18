using UnityEngine;
using UnityEngine.UI;

public class DebugHelper : MonoBehaviour
{
    public static DebugHelper Instance;
    public Text TextMessage;

    private int messageCount;
    private bool clearFlag;
    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            return;
        }

        if (TextMessage == null)
        {
            TextMessage = transform.GetComponentInChildren<Text>();
        }
    }

    public void Log(string message)
    {
        if (clearFlag)
        {
            TextMessage.text = "--";
            messageCount = 0;
            clearFlag = false;
        }

        Debug.Log(message);
        TextMessage.text += System.Environment.NewLine + message;
        messageCount++;
        if (messageCount > 6)
            clearFlag = true;
    }
}
