using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : IHeapItem<Node> {
    // gCost = distància al node inicial
    // hCost = distància al node final
    // fCost = gCost + hCost

    public bool walkable = true;
    public bool easyWalk = false;
    public Vector3 worldPosition;
    public int gridX;
    public int gridY;
    public int movementPenalty;

    public float noderadius = 0.2f;
    private LayerMask ObjectUnwakable = LayerMask.GetMask("ObjectUnwalkable");
    private LayerMask ItemUnwakable = LayerMask.GetMask("ItemUnwalkable");
    private LayerMask ItemWakable = LayerMask.GetMask("ItemUnwalkable");
    private LayerMask Door = LayerMask.GetMask("Door");
    private LayerMask EasyWalk = LayerMask.GetMask("EasyWalk");
    private LayerMask[] layers = new LayerMask[5];


    private enum objectType
    {
        ObjectUnwakableType = 0,
        ItemUnwakableType = 1,
        ItemWakableType = 2,
        DoorType = 3,
        EasyWalk = 4
    }
    private objectType type;
    
    public int gCost;
    public int hCost;
    public Node parent;
    int heapIndex;

    public Node( Vector3 _worldPos, int _gridX, int _gridY, int _penalty)
    {       
       
        worldPosition = _worldPos;
        gridX = _gridX;
        gridY = _gridY;
        movementPenalty = _penalty;

        layers[0] = ObjectUnwakable;
        layers[1] = ItemUnwakable;
        layers[2] = ItemWakable;
        layers[3] = Door;
        layers[4] = EasyWalk;

        for(int i = 0; i < layers.Length; i++)
        {
            
            if (Physics2D.OverlapCircle(_worldPos, noderadius, layers[i]))
            {
                type = (objectType)i;
				if(type == objectType.ObjectUnwakableType || 
				type == objectType.ItemUnwakableType)
                {
                    walkable = false;
                }
                else
                {
                    walkable = true;
                }
                if (type == objectType.EasyWalk)
                {
                    easyWalk = false;
                }
                else
                {
                    easyWalk = true;
                }
                break;
            }      

        }
        

    }

    public int fCost
    {
        get{
            return gCost + hCost;
        }
    }

    public int HeapIndex
    {
        get
        {
            return heapIndex;
        }
        set
        {
            heapIndex = value;
        }
    }

    public int CompareTo(Node nodeToCompare)
    {
        int compare = fCost.CompareTo(nodeToCompare.fCost);
        if(compare == 0)
        {
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }
        return -compare;
    }
    public int layerType
    {
        get
        {
            return (int)type;
        }
        set
        {
            type = (objectType)value;
        }
    }
}
