﻿/*
Copyright (c) 2020 MrBacon470

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BreakInfinity;
using static BreakInfinity.BigDouble;
public class InfusionManager : MonoBehaviour
{
    public IdleGame game;

    private BigDouble Cost1 => 1e5 * Pow(1.5, game.data.infusionULevel1);
    private BigDouble Cost2 => 1e6 * Pow(1.5, game.data.infusionULevel2);
    private BigDouble Cost3 => 1e7 * Pow(2.5, game.data.infusionULevel3);

    public Text[] infusionsText = new Text[3];

    public BigDouble[] infusionUCosts;
    public BigDouble[] infusionULevels;

    public string[] costDesc;

    public void StartInfusion()
    {
        infusionUCosts = new BigDouble[3];
        infusionULevels = new BigDouble[3];
        costDesc = new string[] { "5% More Power Per Second", "1.5x Pollution Capacity", "5% More Transformers On Prestiging" };
    }

    public void Run()
    {
        var data = game.data;

        ArrayManager();
        UI();

        void UI()
        {
            for (var i = 0; i < infusionsText.Length; i++)
            {
                infusionsText[i].text = $"Level {infusionULevels[i]}\n{costDesc[i]}\nCost: {Methods.NotationMethod(infusionUCosts[i], "F0")} Transformers";
            }

        }
    }

    public void BuyUpgrade(int id)
    {
        switch (id)
        {
            case 0:
                Buy(ref game.data.infusionULevel1);
                break;

            case 1:
                Buy(ref game.data.infusionULevel2);
                break;

            case 2:
                Buy(ref game.data.infusionULevel3);
                break;
        }

        void Buy(ref BigDouble level)
        {

            if (game.data.transformers < infusionUCosts[id]) return;
            game.data.transformers -= infusionUCosts[id];
            level++;
        }

    }

    public void ArrayManager()
    {
        infusionUCosts[0] = Cost1;
        infusionUCosts[1] = Cost2;
        infusionUCosts[2] = Cost3;

        infusionULevels[0] = game.data.infusionULevel1;
        infusionULevels[1] = game.data.infusionULevel2;
        infusionULevels[2] = game.data.infusionULevel3;
    }
}
