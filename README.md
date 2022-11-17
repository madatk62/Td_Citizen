


dotnet ef migrations add Update_38_Education_ThongTinChinhQuyen --project .././Migrators/Migrators.MSSQL/ --context ApplicationDbContext -o Migrations/Application

dotnet ef migrations add InitialMigrations --project .././Migrators/Migrators.MSSQL/ --context TenantDbContext -o Migrations/Tenant

dotnet ef migrations add InitialMigrations --project .././Migrators/Migrators.MSSQL/ --context ApplicationDbContext -o Migrations/Application


dotnet ef migrations script {FROM_MIGRATION} {TO_MiGRATION} --context ApplicationDbContext -o ./script.sql

dotnet ef migrations script Update_36_Education Update_32_SeaGame --context ApplicationDbContext -o ./script.sql


dotnet ef migrations add Update_34_School --project .././Migrators/Migrators.MSSQL/ --context ApplicationDbContext -o Migrations/Application



dotnet ef migrations script Update_41_ThueNha Update_47_FixFeedback --context ApplicationDbContext -o ./script.sql


dotnet ef migrations add Update_48_UpdateVehicle --project .././Migrators/Migrators.MSSQL/ --context ApplicationDbContext -o Migrations/Application

dotnet ef migrations add Update_49_UpdateVehicle --project .././Migrators/Migrators.MSSQL/ --context ApplicationDbContext -o Migrations/Application

dotnet ef migrations add Update_50_UpdateTrip --project .././Migrators/Migrators.MSSQL/ --context ApplicationDbContext -o Migrations/Application
dotnet ef migrations add Update_51_UpdateTrip --project .././Migrators/Migrators.MSSQL/ --context ApplicationDbContext -o Migrations/Application
dotnet ef migrations add Update_52_UpdateTrip --project .././Migrators/Migrators.MSSQL/ --context ApplicationDbContext -o Migrations/Application
dotnet ef migrations add Update_53_UpdateTrip --project .././Migrators/Migrators.MSSQL/ --context ApplicationDbContext -o Migrations/Application


dotnet ef migrations script Update_47_FixFeedback Update_53_UpdateTrip --context ApplicationDbContext -o ./script.sql

dotnet ef migrations add Update_54_UpdateTripTicket --project .././Migrators/Migrators.MSSQL/ --context ApplicationDbContext -o Migrations/Application

dotnet ef migrations add Update_55 --project .././Migrators/Migrators.MSSQL/ --context ApplicationDbContext -o Migrations/Application
dotnet ef migrations add Update_56 --project .././Migrators/Migrators.MSSQL/ --context ApplicationDbContext -o Migrations/Application

dotnet ef migrations script Update_53_UpdateTrip Update_56 --context ApplicationDbContext -o ./script.sql

dotnet ef migrations add Update_57 --project .././Migrators/Migrators.MSSQL/ --context ApplicationDbContext -o Migrations/Application
dotnet ef migrations add Update_58 --project .././Migrators/Migrators.MSSQL/ --context ApplicationDbContext -o Migrations/Application
dotnet ef migrations add Update_59 --project .././Migrators/Migrators.MSSQL/ --context ApplicationDbContext -o Migrations/Application
dotnet ef migrations add Update_60 --project .././Migrators/Migrators.MSSQL/ --context ApplicationDbContext -o Migrations/Application
dotnet ef migrations add Update_61 --project .././Migrators/Migrators.MSSQL/ --context ApplicationDbContext -o Migrations/Application
dotnet ef migrations add Update_62 --project .././Migrators/Migrators.MSSQL/ --context ApplicationDbContext -o Migrations/Application
dotnet ef migrations add Update_63 --project .././Migrators/Migrators.MSSQL/ --context ApplicationDbContext -o Migrations/Application
dotnet ef migrations add Update_64 --project .././Migrators/Migrators.MSSQL/ --context ApplicationDbContext -o Migrations/Application

dotnet ef migrations script Update_63 Update_64 --context ApplicationDbContext -o ./script.sql

dotnet ef migrations add Update_65 --project .././Migrators/Migrators.MSSQL/ --context ApplicationDbContext -o Migrations/Application