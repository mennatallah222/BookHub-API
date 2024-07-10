﻿namespace API.Core.Features.UserFeatures.Queries.Response
{
    public class GetUsersListResponse
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
    }
}
