using API.Core.Bases;
using API.Core.Features.Queries.Models;
using API.Core.Features.Queries.Responses;
using API.Service.Interfaces;
using AutoMapper;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace API.Core.Features.Queries.Handlers
{
    public class CustomerHandler : Response_Handler,
                                   IRequestHandler<GetCustomerListQuery, Response<List<GetCustomersResponse>>>,
                                   IRequestHandler<GetCustomerByID, Response<GetSingleCustomerResponse>>

    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;
        public CustomerHandler(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }
        public async Task<Response<List<GetCustomersResponse>>> Handle(GetCustomerListQuery request, CancellationToken cancellationToken)
        {
            var src= await _customerService.GetAll();
            var customerListMapped = _mapper.Map<List<GetCustomersResponse>>(src);
            return Success(customerListMapped);
        }

        public async Task<Response<GetSingleCustomerResponse>> Handle(GetCustomerByID request, CancellationToken cancellationToken)
        {
            var customer = _customerService.GetByIdAsync(request.Id);
            if(customer==null) return NotFound<GetSingleCustomerResponse>("object not found!!");
            var result=_mapper.Map<GetSingleCustomerResponse>(customer);
            return Success(result);
        }
    }
}
