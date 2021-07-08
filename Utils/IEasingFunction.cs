namespace peko.Utils
{
    public interface IEasingFunction
    {
        /// <summary>
        /// Applies the easing function to a time value.
        /// </summary>
        /// <param name="time">The time value to apply the easing to.</param>
        /// <returns>The eased time value.</returns>
        double ApplyEasing(double time);
    }
}