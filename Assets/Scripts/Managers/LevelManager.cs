using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
    public ArtManager artManager;

    [Header("Animation")]
    public float scaleDuration = .1f;
    public float scaleTimeBetweenSlices = .1f;
    public Ease ease = Ease.OutBack;


    [SerializeField] private int _index;
    private GameObject _currentLevel;
    private List<LevelSliceBase> _spawnedPieces = new List<LevelSliceBase>();

    private void Start()
    {
        // SpawnNextLevel();
        //StartCoroutine(CreateLevelPieceCoroutine());
        GenerateLevel();
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

        var artType = RandomColor();
        ColorManager.Instance.ChangeColorByType(artType);
        StartCoroutine(ScaleSliceByTime());
    }

    private ArtManager.ArtType RandomColor()
    {
        var levelSlice = artManager.artSetup[Random.Range(0, artManager.artSetup.Count)];

        return levelSlice.artType;
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

    IEnumerator ScaleSliceByTime()
    {
        foreach(var slice in _spawnedPieces)
        {
            slice.transform.localScale = Vector3.zero;
        }

        yield return null;

        for(int i = 0;i < _spawnedPieces.Count; i++)
        {
            _spawnedPieces[i].transform.DOScale(1, scaleDuration).SetEase(ease);
            yield return new WaitForSeconds(scaleTimeBetweenSlices);
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
