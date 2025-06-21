using UnityEngine;

public class NotDestroy : MonoBehaviour
{
    public static NotDestroy Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Nie usuwaj przy zmianie sceny
        }
        else
        {
            Destroy(gameObject); // Zapobiega duplikatom
        }
    }
}
