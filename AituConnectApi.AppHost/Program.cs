var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.AituConnectApi>("aituconnectapi");

builder.AddProject<Projects.NotificationService>("notificationservice");

builder.Build().Run();
