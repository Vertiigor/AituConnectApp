﻿using AituConnectApp.Dto;

namespace AituConnectApp.Services.Abstractions
{
    public interface IMajorApiService
    {
        public Task<List<MajorResponseDto>> GetAllAsync();
    }
}
