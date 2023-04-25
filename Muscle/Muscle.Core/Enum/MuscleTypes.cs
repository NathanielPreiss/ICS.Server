namespace ICS.Muscle;

public enum MuscleTypes
{
    [Description("Invalid")]
    Invalid = 0,

    // Neck
    [Description("Sternocleidomastoid")]
    Sternocleidomastoid = 111000,
    [Description("Splenius")]
    Splenius = 112000,

    // Shoulder
    [Description("Anterior Deltoid")]
    AnteriorDeltoid = 121000,
    [Description("Lateral Deltoid")]
    LateralDeltoid = 122000,
    [Description("Posterior Deltoid")]
    PosteriorDeltoid = 123000,
    [Description("Supraspinatus")]
    Supraspinatus = 124000,

    // Chest 
    [Description("Pectoralis Major")]
    PectoralisMajor = 131000,
    [Description("Pectoralis Minor")]
    PectoralisMinor = 132000,
    [Description("Serratus Anterior")]
    SerratusAnterior = 133000,

    // UpperBack 
    [Description("Latissimus Dorsi")]
    LatissimusDorsi = 141000,
    [Description("Teres")]
    Teres = 142000,
    [Description("Trapezius")]
    Trapezius = 143000,
    [Description("Levator Scapulae")]
    LevatorScapulae = 145000,
    [Description("Rhomboids")]
    Rhomboids = 146000,
    [Description("Infraspinatus")]
    Infraspinatus = 147000,
    [Description("Subscapularis")]
    Subscapularis = 148000,

    // UpperArm 
    [Description("Triceps Brachii")]
    TricepsBrachii = 151000,
    [Description("Biceps Brachii")]
    BicepsBrachii = 152000,
    [Description("Brachialis")]
    Brachialis = 153000,

    // Forearm 
    [Description("Brachioradialis")]
    Brachioradialis = 161000,
    [Description("Wrist Flexors")]
    WristFlexors = 162000,
    [Description("Wrist Extendors")]
    WristExtendors = 163000,
    [Description("Wrist Pronator")]
    WristPronator = 164000,
    [Description("Wrist Supinator")]
    WristSupinator = 165000,

    // LowerBack 
    [Description("Quadratus Lumborum")]
    QuadratusLumborum = 211000,
    [Description("Erector Spinae")]
    ErectorSpinae = 212000,

    // LowerBack 
    [Description("Rectus Abdominis")]
    RectusAbdominis = 221000,
    [Description("Transverse Abdominis")]
    TransverseAbdominis = 222000,
    [Description("Obliques")]
    Obliques = 223000,

    // Hip 
    [Description("Gluteus Maximus")]
    GluteusMaximus = 311000,
    [Description("Abductors")]
    Abductors = 312000,
    [Description("Flexors")]
    Flexors = 313000,

    // Thigh
    [Description("Quadriceps")]
    Quadriceps = 321000,
    [Description("Hamstrings")]
    Hamstrings = 322000,
    [Description("Adductors")]
    Adductors = 323000,

    // Calve
    [Description("Gastrocnemius")]
    Gastrocnemius = 331000,
    [Description("Soleus")]
    Soleus = 332000,
    [Description("Tibialis Anterior")]
    TibialisAnterior = 333000
}
