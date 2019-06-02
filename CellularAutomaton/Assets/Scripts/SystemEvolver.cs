using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;

public class SystemEvolver : MonoBehaviour
{
    [SerializeField] List<int> bRule = new List<int> { 3 };
    [SerializeField] List<int> sRule = new List<int> { 2, 3 };
    [SerializeField] float speed = 0.25f;

    private int generation = 0;
    private bool started = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !started)
        {
            started = true;
            EvolveAsync().WrapErrors();
        }
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
            await new WaitForSeconds(speed);
        }
    }
}
