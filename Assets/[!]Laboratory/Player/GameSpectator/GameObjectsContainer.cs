using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectsContainer : MonoBehaviour

{
    public static GameObjectsContainer instance = null; // ��������� �������

    
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
    // �����, ����������� ��� ������ ����
    void Awake()
    {
        // ������, ��������� ������������� ����������
        if (instance == null)
        { // ��������� ��������� ��� ������
            instance = this; // ������ ������ �� ��������� �������
        }
        else if (instance == this)
        { // ��������� ������� ��� ���������� �� �����
            Destroy(gameObject); // ������� ������
        }

        // ������ ��� ����� �������, ����� ������ �� �����������
        // ��� �������� �� ������ ����� ����
        DontDestroyOnLoad(gameObject);

        // � ��������� ���������� �������������
        InitializeManager();
    }

    // ����� ������������� ���������
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
