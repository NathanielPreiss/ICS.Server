namespace ICS.Exercise;

public class ExerciseEngagementMuscleMap
{
    public ExerciseTypes ExerciseId { get; }
    public ExerciseEngagement ExerciseEngagements { get; }

    public static IReadOnlyDictionary<ExerciseTypes, ExerciseEngagementMuscleMap> Lookup { get; }
    public static IReadOnlyCollection<ExerciseEngagementMuscleMap> Values { get; }

    static ExerciseEngagementMuscleMap()
    {
        var valueList = ValueList();

        Lookup = new ReadOnlyDictionary<ExerciseTypes, ExerciseEngagementMuscleMap>(valueList.ToDictionary(value => value.ExerciseId));
        Values = new ReadOnlyCollection<ExerciseEngagementMuscleMap>(valueList);
    }

    private ExerciseEngagementMuscleMap(ExerciseTypes exerciseId, ExerciseEngagement exerciseEngagements)
    {
        ExerciseId = exerciseId;
        ExerciseEngagements = exerciseEngagements;
    }

    private static IList<ExerciseEngagementMuscleMap> ValueList()
    {
        return new List<ExerciseEngagementMuscleMap>
        {
            new(ExerciseTypes.NeckFlexion, new ExerciseEngagement(
                new (MuscleEngagementTypes.Target, MuscleTypes.Sternocleidomastoid),
                new (MuscleEngagementTypes.Synergist, Array.Empty<MuscleTypes>()))),
            new(ExerciseTypes.NeckExtension, new ExerciseEngagement(
                new(MuscleEngagementTypes.Target, MuscleTypes.Splenius),
                new(MuscleEngagementTypes.Synergist, MuscleTypes.Trapezius, MuscleTypes.LevatorScapulae,
                    MuscleTypes.ErectorSpinae, MuscleTypes.Sternocleidomastoid))),
            new(ExerciseTypes.FrontRaise, new ExerciseEngagement(
                new(MuscleEngagementTypes.Target, MuscleTypes.AnteriorDeltoid),
                new(MuscleEngagementTypes.Synergist, MuscleTypes.PectoralisMajor, MuscleTypes.LateralDeltoid,
                    MuscleTypes.Trapezius, MuscleTypes.SerratusAnterior))),
            new(ExerciseTypes.UprightRow, new ExerciseEngagement(
                new(MuscleEngagementTypes.Target, MuscleTypes.LateralDeltoid),
                new(MuscleEngagementTypes.Synergist, MuscleTypes.AnteriorDeltoid, MuscleTypes.Supraspinatus,
                    MuscleTypes.Brachialis, MuscleTypes.Brachioradialis, MuscleTypes.BicepsBrachii, MuscleTypes.Trapezius,
                    MuscleTypes.SerratusAnterior, MuscleTypes.Infraspinatus, MuscleTypes.Teres))),
            new(ExerciseTypes.RearRow, new ExerciseEngagement(
                new(MuscleEngagementTypes.Target, MuscleTypes.PosteriorDeltoid),
                new(MuscleEngagementTypes.Synergist, MuscleTypes.Infraspinatus, MuscleTypes.Teres, MuscleTypes.LateralDeltoid,
                    MuscleTypes.Trapezius, MuscleTypes.Brachialis, MuscleTypes.Brachioradialis, MuscleTypes.Rhomboids))),
            new(ExerciseTypes.FrontLateralRaise, new ExerciseEngagement(
                new(MuscleEngagementTypes.Target, MuscleTypes.Supraspinatus),
                new(MuscleEngagementTypes.Synergist, MuscleTypes.LateralDeltoid, MuscleTypes.AnteriorDeltoid, 
                    MuscleTypes.Trapezius, MuscleTypes.SerratusAnterior))),

            new(ExerciseTypes.ChestPress, new ExerciseEngagement(
                new(MuscleEngagementTypes.Target, MuscleTypes.PectoralisMajor),
                new(MuscleEngagementTypes.Synergist, MuscleTypes.PectoralisMinor, MuscleTypes.AnteriorDeltoid,
                    MuscleTypes.TricepsBrachii)))
        };
    }

    public class ExerciseEngagement : ReadOnlyDictionary<MuscleEngagementTypes, IReadOnlyCollection<MuscleTypes>>
    {
        public MuscleTypes Target => this[MuscleEngagementTypes.Target].Single();
        public IReadOnlyCollection<MuscleTypes> Synergists => this[MuscleEngagementTypes.Synergist];

        internal ExerciseEngagement(params EngagementMuscle[] engagementMuscles) :
            base(engagementMuscles.ToDictionary(x => x.MuscleEngagementId, x => x.MuscleIds))
        {
        }

        public class EngagementMuscle
        {
            public MuscleEngagementTypes MuscleEngagementId { get; }
            public IReadOnlyCollection<MuscleTypes> MuscleIds { get; }

            internal EngagementMuscle(MuscleEngagementTypes muscleEngagementId, params MuscleTypes[] muscleIds)
            {
                MuscleEngagementId = muscleEngagementId;
                MuscleIds = new ReadOnlyCollection<MuscleTypes>(muscleIds);
            }
        }
    }
}
