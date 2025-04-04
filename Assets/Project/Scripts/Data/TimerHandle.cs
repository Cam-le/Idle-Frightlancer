namespace PettyFrightlancer.Core.Data
{
    /// <summary>
    /// Struct to safely reference timers.
    /// Includes a generation counter to prevent using cancelled timer IDs.
    /// </summary>
    public readonly struct TimerHandle
    {
        public readonly int Id;
        public readonly int Generation;

        public TimerHandle(int id, int generation)
        {
            Id = id;
            Generation = generation;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is TimerHandle other))
                return false;

            return Id == other.Id && Generation == other.Generation;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ Generation.GetHashCode();
        }

        public static bool operator ==(TimerHandle a, TimerHandle b)
        {
            return a.Id == b.Id && a.Generation == b.Generation;
        }

        public static bool operator !=(TimerHandle a, TimerHandle b)
        {
            return !(a == b);
        }

        public override string ToString()
        {
            return $"Timer[{Id}.{Generation}]";
        }
    }
}