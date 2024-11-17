using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Random = UnityEngine.Random;

public class LargeAmountOfUpdates
{
    private GameObject _gameObjectWithUpdate;
    private GameObject _gameObjectWithoutUpdate;

    private List<GameObject> _gameObjectsWithUpdate = new ();
    private List<GameObject> _gameObjectsWithoutUpdate = new ();

    private const int ObjectsAmount = 5000;
    private const int FramesToMeasure = 500; // 1000 is ~3 seconds

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _gameObjectWithUpdate = Resources.Load<GameObject>("SamplePrefabWithUpdateLoop");
        _gameObjectWithoutUpdate = Resources.Load<GameObject>("SamplePrefabWithoutUpdateLoop");
    }
    

    [UnityTest]
    public IEnumerator Sample_FPS_GameObjects_With_Update_loop()
    {
        for (int i = 0; i < ObjectsAmount; i++)
        {
            var go = GameObject.Instantiate(_gameObjectWithUpdate);
            go.transform.position = new Vector3(Random.Range(-2.0f, 2.0f), Random.Range(-2.0f, 2.0f), Random.Range(-2.0f, 2.0f));
            _gameObjectsWithUpdate.Add(go);
        }
        yield return new WaitForSeconds(1.0f);
        float total = 0;
        for (int i = 0; i < FramesToMeasure; i++)
        {
            yield return new WaitForEndOfFrame();
            total += 1 / Time.deltaTime;
        }
        float averageFPS = total / FramesToMeasure;
        Debug.Log($"{ObjectsAmount} GameObjects with Update loop: {averageFPS} FPS");
        foreach (var go in _gameObjectsWithUpdate)
            GameObject.Destroy(go);
        _gameObjectsWithUpdate.Clear();
    }
    
    [UnityTest]
    public IEnumerator Sample_FPS_GameObjects_Without_Update_loop()
    {
        for (int i = 0; i < ObjectsAmount; i++)
        {
            var go = GameObject.Instantiate(_gameObjectWithoutUpdate);
            go.transform.position = new Vector3(Random.Range(-2.0f, 2.0f), Random.Range(-2.0f, 2.0f), Random.Range(-2.0f, 2.0f));
            _gameObjectsWithoutUpdate.Add(go);
        }
        yield return new WaitForSeconds(1.0f);
        float total = 0;
        for (int i = 0; i < FramesToMeasure; i++)
        {
            RotateGameObjectsWithUpdateLoop();
            yield return new WaitForEndOfFrame();
            total += 1 / Time.deltaTime;
        }
        float averageFPS = total / FramesToMeasure;
        Debug.Log($"{ObjectsAmount} GameObjects without Update loop: {averageFPS} FPS");
        foreach (var go in _gameObjectsWithoutUpdate)
            GameObject.Destroy(go);
        _gameObjectsWithoutUpdate.Clear();
    }

    private void RotateGameObjectsWithUpdateLoop()
    {
        foreach (var go in _gameObjectsWithoutUpdate)
        {
            go.transform.Rotate(new Vector3(10,10,10) * Time.deltaTime);
        }
    }
}

