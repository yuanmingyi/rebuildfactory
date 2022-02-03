using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Subclass of Unit that will transport resource from a Resource Pile back to Base.
/// </summary>
public class TransporterUnit : Unit
{
    public int MaxAmountTransported = 100;

    private Building m_CurrentTransportTarget;
    private Building.InventoryEntry m_Transporting = new Building.InventoryEntry();

    // We override the GoTo function to remove the current transport target, as any go to order will cancel the transport
    public override void GoTo(Vector3 position)
    {
        base.GoTo(position);
        m_CurrentTransportTarget = null;
    }

    public override void GoTo(Building target)
    {
        base.GoTo(target);
        m_CurrentTransportTarget = target;
    }

    protected override void BuildingInRange()
    {
        if (m_Target != m_CurrentTransportTarget)
        {
            //we arrive at the base, unload!
            if (m_Transporting.Count > 0)
            {
                m_Transporting.Count = m_Target.AddItem(m_Transporting.ResourceId, m_Transporting.Count);
            }

            //we go back to the building we came from
            if (m_Transporting.Count == 0)
            {
                base.GoTo(m_CurrentTransportTarget);
                m_Transporting.ResourceId = "";
            }
        }
        else
        {
            if (m_Transporting.Count == 0 && m_Target.Inventory.Count > 0)
            {
                m_Transporting.ResourceId = m_Target.Inventory[0].ResourceId;
                m_Transporting.Count = m_Target.GetItem(m_Transporting.ResourceId, MaxAmountTransported);
            }

            if (m_Transporting.Count > 0)
            {
                var target = FindNeareastInventory();
                if (target != null)
                {
                    base.GoTo(target);
                }
            }
        }
    }

    //Override all the UI function to give a new name and display what it is currently transporting
    public override string GetName()
    {
        return "Transporter";
    }

    public override string GetData()
    {
        return $"Can transport up to {MaxAmountTransported}";
    }

    public override void GetContent(ref List<Building.InventoryEntry> content)
    {
        if (m_Transporting.Count > 0)
            content.Add(m_Transporting);
    }

    private Building FindNeareastInventory()
    {
        var minDist = float.MaxValue;
        Building nearestInventory = null;
        foreach (var inventory in GameManager.Instance.InventoryBuildings)
        {
            if (inventory.IsFullFilled)
            {
                continue;
            }

            var dist = inventory.Distance(transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearestInventory = inventory;
            }
        }

        return nearestInventory;
    }
}
