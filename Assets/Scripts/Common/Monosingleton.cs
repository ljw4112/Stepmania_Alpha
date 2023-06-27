using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{

    private static T _instance = null;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                //널이면 최초로 실행된 인스턴스 있나 확인
                _instance = FindObjectOfType(typeof(T)) as T;
                if (_instance == null)
                {
                    //없으면 생성 
                    _instance = new GameObject("@" + typeof(T).ToString(),
                        typeof(T)).GetComponent<T>();
                    DontDestroyOnLoad(_instance);
                }
            }

            return _instance;
        }
    }

    private void OnDestroy()
    {
        _instance = null;
    }
}