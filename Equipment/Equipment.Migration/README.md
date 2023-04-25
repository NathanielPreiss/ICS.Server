dotnet ef migrations add InitialMigration --project Exercise.Migration --verbose
dotnet ef database update --project Exercise.Migration --verbose
dotnet ef migrations bundle --project Exercise.Migration --force --verbose 
efbundle.exe --connection "Data Source=localhost; Integrated Security=true; Initial Catalog=IronClanStudios;"