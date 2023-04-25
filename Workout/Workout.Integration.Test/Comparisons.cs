namespace ICS.Workout.Test;

public static class Comparisons
{
    public static Comparer<Set> SetComparer =>
        Comparer<Set>.Create((expected, actual) =>
        {
            Assert.AreEqual(expected.RoutineId, actual.RoutineId);
            Assert.AreEqual(expected.SetId, actual.SetId);
            Assert.AreEqual(expected.Reps, actual.Reps);
            Assert.AreEqual(expected.Weight, actual.Weight);
            return 0;
        });
}
