using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/// <summary>
/// A subclass of Building that produce resource at a constant rate.
/// </summary>
public class ResourcePile : Building
{
    public ResourceItem Item;

    [SerializeField]
    private GameObject[] resources;

    [SerializeField]
    private float maxValue = 2000;

    [SerializeField]
    private float productionSpeed = 1;

    [SerializeField]
    private int maxWorkers = 5;

    public int CurrentWorkers { get; private set; }

    private float currentProductValue;
    private float currentValue;
    private float lastValue = 0;

    protected override void Start()
    {
        base.Start();
        currentValue = maxValue;
        currentProductValue = 0;
        CurrentWorkers = 0;
        UpdateResources();
    }

    public void Produce()
    {
        float productions = productionSpeed * Time.deltaTime;

        if (!IsFullFilled && currentValue > 0 && productions > 0)
        {
            currentProductValue += productions;
            currentValue -= productions;
            int tryToAddValue = (int)Math.Floor(currentProductValue);
            currentProductValue -= tryToAddValue - AddItem(Item.Id, tryToAddValue);
            UpdateResources();
            UpdateProducts();
        }
    }

    protected override void Update()
    {
        base.Update();
        CurrentWorkers = (int)Math.Round((lastValue - currentValue) / (productionSpeed * Time.deltaTime));
        lastValue = currentValue;
    }

    public override string GetData()
    {
        var message = new StringBuilder();
        message.AppendLine($"Remain Value: {(int)Math.Floor(currentValue)}");
        message.AppendLine($"Preserved Product Value:  {m_CurrentAmount}");
        message.AppendLine($"Working Staffs: {CurrentWorkers}");
        message.AppendLine($"Production Speed: {(int)Math.Round(productionSpeed * CurrentWorkers)}/s");
        return message.ToString();
    }

    void UpdateResources()
    {
        var showResourcesIndex = resources.Length * currentValue / maxValue;
        for (int i = 0; i < resources.Length; i++)
        {
            resources[i].SetActive(i < showResourcesIndex);
        }
    }
}
