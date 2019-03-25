using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WappaMobile.Domain;
using WappaMobile.Persistence;

namespace WappaMobile.Application
{
    /// <summary>
    /// Query handler for <see cref="ListDriversQuery"/>.
    /// </summary>
    public class ListDriversQueryHandler : IRequestHandler<ListDriversQuery, IEnumerable<ViewDriverDto>>
    {
        private readonly DriverContext _driverContext;
        private readonly IMapper _mapper;

        public ListDriversQueryHandler(DriverContext driverContext, IMapper mapper)
        {
            _driverContext = driverContext ?? throw new System.ArgumentNullException(nameof(driverContext));
            _mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
        }

        public Task<IEnumerable<ViewDriverDto>> Handle(ListDriversQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Driver> query = _driverContext.Drivers;

            switch (request.OrderBy)
            {
                case ListDriversQuery.Sorting.FirstName:
                    query = query.OrderBy(d => d.FirstName);
                    break;

                case ListDriversQuery.Sorting.LastName:
                    query = query.OrderBy(d => d.LastName);
                    break;

                default:
                    break;
            }

            return Task.FromResult(_mapper.ProjectTo<ViewDriverDto>(query).AsEnumerable());
        }
    }
}
