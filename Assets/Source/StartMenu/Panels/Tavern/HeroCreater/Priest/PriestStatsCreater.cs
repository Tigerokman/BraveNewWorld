using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PriestStats", menuName = "HeroCreater/StatsCreater/PriestStatsCreater")]
public class PriestStatsCreater : HeroStatsCreater
{
    protected override void DistributeLeftStats(int leftStats, CharacterStats characterStats)
    {
        if (leftStats >= 0)
            characterStats.AssignStat(0, 0, leftStats / 2 * HealthToPoint, leftStats / 2);
        else
            characterStats.AssignStat(leftStats / 2, leftStats / 2 / PointToDefense, 0, 0);
    }
}