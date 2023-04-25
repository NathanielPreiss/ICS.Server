﻿namespace ICS.Exercise;

public class MuscleEngagement
{
    public MuscleEngagementTypes MuscleEngagementId { get; }
    public string MuscleEngagementName { get; }

    static MuscleEngagement()
    {
        var valueList = ValueList();

        Lookup = new ReadOnlyDictionary<MuscleEngagementTypes, MuscleEngagement>(valueList.ToDictionary(value => value.MuscleEngagementId));
        Values = new ReadOnlyCollection<MuscleEngagement>(valueList);
    }


    private MuscleEngagement(MuscleEngagementTypes muscleEngagementId, string muscleEngagementName)
    {
        MuscleEngagementId = muscleEngagementId;
        MuscleEngagementName = muscleEngagementName;
    }

    public static readonly IReadOnlyDictionary<MuscleEngagementTypes, MuscleEngagement> Lookup;
    public static readonly IReadOnlyCollection<MuscleEngagement> Values;

    private static IList<MuscleEngagement> ValueList()
    {
        return new List<MuscleEngagement>
        {
            new(MuscleEngagementTypes.Target, "Target"),
            new(MuscleEngagementTypes.Synergist, "Synergist")
        };
    }
}
