//using API.Core.Bases;
//using API.Core.Features.Queries.Models;
//using API.Core.Features.Queries.Responses;
//using API.Core.SharedResource;
//using API.Service.Interfaces;
//using AutoMapper;
//using MediatR;
//using Microsoft.Extensions.Localization;

//namespace API.Core.Features.Queries.Handlers
//{
//    public class CustomerHandler : Response_Handler,
//                                   IRequestHandler<GetCustomerListQuery, Response<List<GetCustomersResponse>>>,
//                                   IRequestHandler<GetCustomerByID, Response<GetSingleCustomerResponse>>

//    {
//        private readonly ICustomerService _customerService;
//        private readonly IStringLocalizer<SharedResources> _localizer;
//        private readonly IMapper _mapper;
//        public CustomerHandler(ICustomerService customerService, IMapper mapper,
//                                  IStringLocalizer<SharedResources> localizer) : base(localizer)

//        {
//            _customerService = customerService;
//            _mapper = mapper;
//            _localizer = localizer;
//        }
//        public async Task<Response<List<GetCustomersResponse>>> Handle(GetCustomerListQuery request, CancellationToken cancellationToken)
//        {
//            var src = await _customerService.GetAll();
//            var customerListMapped = _mapper.Map<List<GetCustomersResponse>>(src);
//            return Success(customerListMapped);
//        }

//        public async Task<Response<GetSingleCustomerResponse>> Handle(GetCustomerByID request, CancellationToken cancellationToken)
//        {
//            var customer = await _customerService.GetByIdAsync(request.Id);
//            if (customer == null) return NotFound<GetSingleCustomerResponse>("object not found!!");
//            var result = _mapper.Map<GetSingleCustomerResponse>(customer);
//            return Success(result);
//        }
//    }
//}
