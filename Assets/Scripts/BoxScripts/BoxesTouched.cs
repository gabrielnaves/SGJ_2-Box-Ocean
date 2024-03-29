﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxesTouched : MonoBehaviour {

    static public BoxesTouched instance { get; private set; }

    List<Box> boxList = new List<Box>();

    void Awake() {
        instance = this;
    }

    public void Add(Box box) {
        if (!boxList.Contains(box)) {
            boxList.Add(box);
            box.transform.parent = transform;
            GameManager.instance.data.touchedBoxes++;
        }
    }

    public int Amount() {
        return boxList.Count;
    }

    public Box FirstBox() {
        if (Amount() == 0)
            return null;
        var first = boxList[0];
        RemoveBox(0);
        return first;
    }

    public Box LastBox() {
        if (Amount() == 0)
            return null;
        var last = boxList[Amount()-1];
        RemoveBox(Amount()-1);
        return last;
    }

    void LateUpdate() {
        for (int i = 0; i < Amount(); ++i) {
            if (boxList[i] == null)
                RemoveBox(i--);
            else if (boxList[i].type == BoxType.GREEN) {
                BoxesTurned.instance.Add(boxList[i]);
                RemoveBox(i--);
            }
            else if (boxList[i].inCluster) {
                RemoveBox(i--);
            }
        }
    }

    void RemoveBox(int index) {
        boxList.RemoveAt(index);
        GameManager.instance.data.touchedBoxes--;
    }
}
