
dotnet ef database drop --project DataAccess --startup-project API

dotnet ef migrations add init --project DataAccess --startup-project API

dotnet ef database update --project DataAccess --startup-project API
