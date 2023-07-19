using MediatR;

namespace apsys.heartbeat.services
{
    /// <summary>
    /// The abstract request base
    /// </summary>
    public abstract class RequestBase : IRequest
    {

        public RequestBase(string requestedBy, string queryString)
        {
            RequestedBy = requestedBy;
            QueryString = queryString;
        }

        public string RequestedBy { get; set; }
        public string QueryString { get; }
    }

    /// <summary>
    /// The abstract request base
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class RequestBase<T> : IRequest<T>
    {

        protected RequestBase(string requestedBy)
        {
            RequestedBy = requestedBy;
        }

        public string RequestedBy { get; set; }

        public TCommand As<TCommand, TResponse>() where TCommand : RequestBase<TResponse>
            => this as TCommand;

    }
}
