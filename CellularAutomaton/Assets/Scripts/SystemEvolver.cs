using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;

public class SystemEvolver : MonoBehaviour
{
    public Rules.rules rule = Rules.rules.Life;
    [SerializeField] List<int> bRule = new List<int> { 3 };
    [SerializeField] List<int> sRule = new List<int> { 2, 3 };
    [SerializeField] float simulationSpeed = 0.25f;


    private int generation = 0;
    private bool started = false;

    private void Awake()
    {
        bRule = Rules.bRules[(int)rule];
        sRule = Rules.sRules[(int)rule];
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !started)
        {
            started = true;
            EvolveAsync().WrapErrors();
        }

        if (Input.GetKeyDown(KeyCode.S) && !started)
        {
            SaveToFile("test");
        }

        if (Input.GetKeyDown(KeyCode.L) && !started)
        {
            LoadFromFile("test");
        }
    }

    private void LoadFromFile(string fileName)
    {
        Debug.Log("Loading...");
        var path = $"{Application.dataPath}/{fileName}.txt";
        var json = File.ReadAllText(path);

        var sd = JsonConvert.DeserializeObject<SaveData>(json);
        var dictHelper = JsonConvert.DeserializeObject<Dictionary<int, bool>>(sd.cellsJson);

        var size = GridGenerator.Instance.size;

        foreach (var cell in dictHelper)
        {
            int y = cell.Key / sd.xSize;
            int x = cell.Key - sd.xSize * y;

            if (x < size.x && y < size.y)
            {
                GridGenerator.Instance.grid[new int2(x, y)].SetAlive(cell.Value);
                GridGenerator.Instance.grid[new int2(x, y)].Update();
            }
        }
        Debug.Log("Loading complete!");
    }

    private void SaveToFile(string fileName)
    {
        Debug.Log("Saving...");
        var path = $"{Application.dataPath}/{fileName}.txt";
        Dictionary<int, bool> dictHelper = new Dictionary<int, bool>();

        var size = GridGenerator.Instance.size;

        foreach (var cell in GridGenerator.Instance.grid)
        {
            var index = cell.Key.x + cell.Key.y * size.x;
            dictHelper.Add(index, cell.Value.IsAlive);
        }

        string cellData = JsonConvert.SerializeObject(dictHelper);
        string saveData = JsonConvert.SerializeObject(new SaveData(size.x, size.y, cellData));
        File.WriteAllText(path, saveData);
        Debug.Log("Save complete!");
    }

    private async Task EvolveAsync()
    {
        while (true)
        {
            //Debug.Log($"Generation: {++generation}");
            foreach (var cell in GridGenerator.Instance.grid.Values)
            {
                cell.Next(bRule, sRule);
            }

            foreach (var cell in GridGenerator.Instance.grid.Values)
            {
                cell.Update();
            }
            await new WaitForSeconds(simulationSpeed);
        }
    }

    private class SaveData
    {
        public int xSize;
        public int ySize;
        public string cellsJson;

        public SaveData(int xSize, int ySize, string cellsJson)
        {
            this.xSize = xSize;
            this.ySize = ySize;
            this.cellsJson = cellsJson;
        }
    }
}

