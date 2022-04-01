using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectsContainer : MonoBehaviour

{
    public static GameObjectsContainer instance = null; // Ёкземпл€р объекта

    
    public GameObject Me;    

    [SerializeField] private List<GameObject> _allObjects;
	public List<GameObject> AllObjects
	{
		private set
		{
			_allObjects = value;
		}

		get
		{
			return _allObjects;
		}
	}

	[SerializeField] private AbillityExecuter _abillityExecuter;
    public AbillityExecuter AbillityExecuter => _abillityExecuter;
    // ћетод, выполн€емый при старте игры
    void Awake()
    {
        // “еперь, провер€ем существование экземпл€ра
        if (instance == null)
        { // Ёкземпл€р менеджера был найден
            instance = this; // «адаем ссылку на экземпл€р объекта
        }
        else if (instance == this)
        { // Ёкземпл€р объекта уже существует на сцене
            Destroy(gameObject); // ”дал€ем объект
        }

        // “еперь нам нужно указать, чтобы объект не уничтожалс€
        // при переходе на другую сцену игры
        DontDestroyOnLoad(gameObject);

        // » запускаем собственно инициализатор
        InitializeManager();
    }

    // ћетод инициализации менеджера
    private void InitializeManager()
    {
        _allObjects = new List<GameObject>();
        AllObjects = new List<GameObject>();

        var i = transform.childCount;
        for (int j = 0; j < i; j++)
		{
            _allObjects.Add(this.gameObject.transform.GetChild(j).gameObject);            
        }
        
    }
}
