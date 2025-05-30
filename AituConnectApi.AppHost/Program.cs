var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.AituConnectApi>("aituconnectapi");

builder.Build().Run();
