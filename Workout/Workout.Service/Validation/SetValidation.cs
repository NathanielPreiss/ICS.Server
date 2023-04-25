namespace ICS.Workout;

internal static class SetValidation
{
    public static void Validate(this Set set)
    {
        if (set.RoutineId == Guid.Empty)
        {
            throw new Exception("");
        }
        if (set.SetId == Guid.Empty)
        {
            throw new Exception("");
        }
        if (set.Reps < 1)
        {
            throw new Exception("");
        }
        if (set.Weight < 1)
        {
            throw new Exception("");
        }
    }

    public static void Validate(this ICollection<Set> sets)
    {
        foreach (var set in sets)
        {
            set.Validate();
        }


        if (sets.GroupBy(x => x.RoutineId).Count() > 1)
        {
            throw new Exception("");
        }

        if (sets.DistinctBy(x => x.SetId).Count() != sets.Count)
        {
            throw new Exception("");
        }
    }
}
