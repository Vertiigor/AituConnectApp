﻿namespace AituConnectApp.Settings.Api.AituConnect
{
    public class ApiSettings
    {
        public string BaseUrl { get; set; }
        public UsersEndpointsSettings UsersEndpoints { get; set; }
        public UniversitiesEndpointsSettings UniversitiesEndpoints { get; set; }
        public MajorsEndpointsSettings MajorsEndpoints { get; set; }
        public SubjectsEndpointsSettings SubjectsEndpoints { get; set; }
        public PostsEndpointsSettings PostsEndpoints { get; set; }
        public CommentsEndpointsSettings CommentsEndpoints { get; set; }
    }
}
