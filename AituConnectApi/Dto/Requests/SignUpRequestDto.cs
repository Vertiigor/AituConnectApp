﻿namespace AituConnectApi.Dto.Requests
{
    public class SignUpRequestDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string UniversityId { get; set; }
        public string MajorId { get; set; }
    }
}
