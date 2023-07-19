namespace apsys.heartbeat.webapi
{
    /// <summary>
    /// Aggregate exception handler
    /// </summary>
    public static class AggregateExceptionExtender
    {

        /// <summary>
        /// Determine if contains an exception of type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="aggregateException"></param>
        /// <returns></returns>
        public static bool IsExceptionType<T>(this AggregateException aggregateException)
        {
            return aggregateException.InnerExceptions
                .All(x => x.GetType() == typeof(T));
        }

    }
}
