using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rack: Building
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override string GetData()
    {
        return $"Products({m_CurrentAmount}/{InventorySpace})";
    }
}
