using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WorldSwitcher : MonoBehaviour
{

    public Camera cam;
    private bool down, up, left, right;
    public bool ifDown, ifUp, ifLeft, ifRight;

    private void Awake()
    {
        down = up = left = right = false;
    }

    void Update()
    {
        bool clear = Input.GetKey("o");

        if (down != Input.GetKey("k") && ifDown)
        {
            down = Input.GetKey("k");
            if(down) Refresh();
        }

        if (up != Input.GetKey("i") && ifUp)
        {
            up = Input.GetKey("i");
            if(up) Refresh();
        }

        if (left != Input.GetKey("j") && ifLeft)
        {
            left = Input.GetKey("j");
            if(left) Refresh();
        }

        if (right != Input.GetKey("l") && ifRight)
        {
            right = Input.GetKey("l");
            if(right) Refresh();
        }
        
    }

    private void Refresh()
    {
        Global.down_Pulling = down;
        Global.left_Pulling = left;
        Global.right_Pulling = right;
        Global.up_Pulling = up;
    }
}
