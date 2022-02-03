using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductivityUnit : Unit
{
    protected override void BuildingInRange()
    {
        var currentResourcePileTarget = m_Target as ResourcePile;
        currentResourcePileTarget.Produce();
    }

    public override string GetName()
    {
        return "Worker";
    }

    public override string GetData()
    {
        return $"target: {m_Target?.name}";
    }

    public override void GetContent(ref List<Building.InventoryEntry> content)
    {

    }
}
