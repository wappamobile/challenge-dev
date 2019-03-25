using System.Collections;
using System.Collections.Generic;
using MediatR;
using WappaMobile.Domain;

namespace WappaMobile.Application
{
    /// <summary>
    /// Query to list all drivers.
    /// </summary>
    public class ListDriversQuery : IRequest<IEnumerable<ViewDriverDto>>
    {
        public enum Sorting
        {
            FirstName,
            LastName
        }

        public Sorting OrderBy { get; }

        public ListDriversQuery(Sorting orderBy)
        {
            OrderBy = orderBy;
        }
    }
}
