using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Transform container;
    public List<GameObject> levels;

    [SerializeField] private int _index;
    private GameObject _currentLevel;

    private void Awake()
    {
        SpawnNextLevel();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("Level criado");
            SpawnNextLevel();
        }
    }

    private void SpawnNextLevel()
    {
        if(_currentLevel != null)
        {
            Destroy(_currentLevel);
            _index++;

            if( _index >= levels.Count )
            {
                ResetLevelIndex();
            }
        }

        _currentLevel = Instantiate(levels[_index].gameObject);
        _currentLevel.transform.localPosition = Vector3.zero;
    }

    private void ResetLevelIndex()
    {
            _index = 0;
    }

    

}
