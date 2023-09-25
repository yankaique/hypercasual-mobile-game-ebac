using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Transform container;

    [Header("Simple Level")]
    public List<GameObject> levels;

    [Header("Random Level")]
    public List<LevelSliceBase> levelStartPieces;
    public List<LevelSliceBase> levelPieces;
    public List<LevelSliceBase> levelEndPieces;
    public int pieceStartNumber = 5;
    public int pieceNumber = 5;
    public int pieceEndNumber = 5;
    public float timeBetweenSlices = .3f;

    [Header("Art")]
    public ArtManager.ArtType arType;

    [SerializeField] private int _index;
    private GameObject _currentLevel;
    private List<LevelSliceBase> _spawnedPieces = new List<LevelSliceBase>();

    private void Awake()
    {
        // SpawnNextLevel();
        GenerateLevel();
        //StartCoroutine(CreateLevelPieceCoroutine());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            SpawnNextLevel();
        }
    }

    #region Pieces
    private void CleanSpawnedPiece()
    {
        for(int i = _spawnedPieces.Count - 1; i >= 0; i--)
        {
            Destroy(_spawnedPieces[i]);
        }

        _spawnedPieces.Clear();
    }

    private void GenerateLevel()
    {
        CleanSpawnedPiece();

        for (int i = 0; i < pieceStartNumber; i++) {
            CreateLevelPiece(levelStartPieces);
        }
        
        for(int i = 0; i < pieceNumber; i++) {
            CreateLevelPiece(levelPieces);
        }
        
        for(int i = 0; i < pieceEndNumber; i++) {
            CreateLevelPiece(levelEndPieces);
        }
    }
    private void CreateLevelPiece(List<LevelSliceBase> levelList)
    {
        var levelSlice = levelList[Random.Range(0, levelList.Count)];
        var newSliceOfMap = Instantiate(levelSlice, container);

        if (_spawnedPieces.Count > 0)
        {
            var lastPiece = _spawnedPieces[_spawnedPieces.Count - 1];
            newSliceOfMap.transform.position = lastPiece.finalSlice.position;
        }else
        {
            newSliceOfMap.transform.localPosition = Vector3.zero;
        }

        /*
        foreach (var p in newSliceOfMap.GetComponentInChildren<ArtPiece>()) 
        { 
            p.ChangePiece(ArtManager.Instance.GetSetupByType(p.type).gameObject);
        }
        */

        _spawnedPieces.Add(newSliceOfMap);
    }

    IEnumerator CreateLevelPieceCoroutine()
    {
        _spawnedPieces = new List<LevelSliceBase>();
        for(int i = 0; i < pieceNumber;i++)
        {
            CreateLevelPiece(levelStartPieces);
            yield return new WaitForSeconds(timeBetweenSlices);
        }
    }

    #endregion

    #region Create Level
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
    #endregion


}
