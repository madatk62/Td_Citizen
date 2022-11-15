﻿using Finbuckle.MultiTenant;
using TD.CitizenAPI.Application.Common.Events;
using TD.CitizenAPI.Application.Common.Interfaces;
using TD.CitizenAPI.Domain.Catalog;
using TD.CitizenAPI.Infrastructure.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace TD.CitizenAPI.Infrastructure.Persistence.Context;

public class ApplicationDbContext : BaseDbContext
{
    public ApplicationDbContext(ITenantInfo currentTenant, DbContextOptions options, ICurrentUser currentUser, ISerializerService serializer, IOptions<DatabaseSettings> dbSettings, IEventPublisher events)
        : base(currentTenant, options, currentUser, serializer, dbSettings, events)
    {
    }

    #region Other
    public DbSet<Attachment> Attachments => Set<Attachment>();
    public DbSet<EKYCAttachment> EKYCAttachments => Set<EKYCAttachment>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Notification> Notifications => Set<Notification>();
    public DbSet<Feedback> Feedbacks => Set<Feedback>();
    public DbSet<HomePageInfor> HomePageInfors => Set<HomePageInfor>();
    public DbSet<HuongDanSuDung> HuongDanSuDungs => Set<HuongDanSuDung>();


    #endregion Other

    #region Place
    public DbSet<PlaceType> PlaceTypes => Set<PlaceType>();
    public DbSet<Area> Areas => Set<Area>();
    public DbSet<Place> Places => Set<Place>();
    public DbSet<AreaInfor> AreaInfors => Set<AreaInfor>();
    public DbSet<AreaInforValue> AreaInforValues => Set<AreaInforValue>();

    #endregion Place

    #region Market
    public DbSet<MarketCategory> MarketCategories => Set<MarketCategory>();
    public DbSet<MarketProduct> MarketProducts => Set<MarketProduct>();
    #endregion Market

    #region Hotline
    public DbSet<HotlineCategory> HotlineCategories => Set<HotlineCategory>();
    public DbSet<Hotline> Hotlines => Set<Hotline>();
    #endregion Hotline

    #region Traffic
    public DbSet<VehicleType> VehicleTypes => Set<VehicleType>();
    public DbSet<CarPolicy> CarPolicies => Set<CarPolicy>();
    public DbSet<CarUtility> CarUtilities => Set<CarUtility>();
    public DbSet<Passenger> Passengers => Set<Passenger>();
    public DbSet<Ticket> Tickets => Set<Ticket>();
    public DbSet<CompanyCarPolicy> CompanyCarPolicies => Set<CompanyCarPolicy>();
    public DbSet<Vehicle> Vehicles => Set<Vehicle>();
    public DbSet<VehicleCarUtility> VehicleCarUtilities => Set<VehicleCarUtility>();
    public DbSet<Trip> Trips => Set<Trip>();
    public DbSet<TripRoute> TripRoutes => Set<TripRoute>();

    public DbSet<Carpool> Carpools => Set<Carpool>();
    #endregion Traffic

    #region Company
    public DbSet<Benefit> Benefits => Set<Benefit>();
    public DbSet<Degree> Degrees => Set<Degree>();
    public DbSet<Experience> Experiences => Set<Experience>();
    public DbSet<Industry> Industries => Set<Industry>();
    public DbSet<JobAge> JobAges => Set<JobAge>();
    public DbSet<JobName> JobNames => Set<JobName>();
    public DbSet<JobPosition> JobPositions => Set<JobPosition>();
    public DbSet<JobType> JobTypes => Set<JobType>();
    public DbSet<Salary> Salaries => Set<Salary>();
    public DbSet<Recruitment> Recruitments => Set<Recruitment>();
    public DbSet<RecruitmentBenefit> RecruitmentBenefits => Set<RecruitmentBenefit>();
    public DbSet<Company> Companies => Set<Company>();
    public DbSet<CompanyIndustry> CompanyIndustries => Set<CompanyIndustry>();
    public DbSet<JobApplication> JobApplications => Set<JobApplication>();
    public DbSet<JobApplied> JobApplieds => Set<JobApplied>();
    public DbSet<JobSaved> JobSaveds => Set<JobSaved>();
    #endregion Company

    #region Ecommerce
    public DbSet<Brand> Brands => Set<Brand>();
    public DbSet<EcommerceCategory> EcommerceCategories => Set<EcommerceCategory>();
    public DbSet<Domain.Catalog.Attribute> Attributes => Set<Domain.Catalog.Attribute>();
    public DbSet<AttributeValue> AttributeValues => Set<AttributeValue>();

    public DbSet<AttributeBoolean> AttributeBooleans => Set<AttributeBoolean>();
    public DbSet<AttributeDatetime> AttributeDatetimes => Set<AttributeDatetime>();
    public DbSet<AttributeDecimal> AttributeDecimals => Set<AttributeDecimal>();
    public DbSet<AttributeInt> AttributeInts => Set<AttributeInt>();
    public DbSet<AttributeText> AttributeTexts => Set<AttributeText>();
    public DbSet<AttributeVarchar> AttributeVarchars => Set<AttributeVarchar>();
    public DbSet<Product> Products => Set<Product>();

    public DbSet<EcommerceCategoryAttribute> EcommerceCategoryAttributes => Set<EcommerceCategoryAttribute>();
    public DbSet<EcommerceCategoryProduct> EcommerceCategoryProducts => Set<EcommerceCategoryProduct>();

    public DbSet<ProductReview> ProductReviews => Set<ProductReview>();
    public DbSet<ProductSaved> ProductSaveds => Set<ProductSaved>();


    #endregion Ecommerce

    #region Alert
    public DbSet<AlertCategory> AlertCategories => Set<AlertCategory>();
    public DbSet<AlertOrganization> AlertOrganizations => Set<AlertOrganization>();
    public DbSet<AlertInformation> AlertInformations => Set<AlertInformation>();

    #endregion Alert

    #region Enterprise
    public DbSet<LawData> LawDatas => Set<LawData>();
    public DbSet<EnterpriseForumCategory> EnterpriseForumCategories => Set<EnterpriseForumCategory>();
    public DbSet<EnterpriseForumComment> EnterpriseForumComments => Set<EnterpriseForumComment>();
    public DbSet<EnterpriseForumTopic> EnterpriseForumTopics => Set<EnterpriseForumTopic>();
    public DbSet<LaborMarketInformation> LaborMarketInformations => Set<LaborMarketInformation>();
    public DbSet<ProjectInvestCategory> ProjectInvestCategories => Set<ProjectInvestCategory>();
    public DbSet<ProjectInvestForm> ProjectInvestForms => Set<ProjectInvestForm>();
    public DbSet<ProjectInvestInformation> ProjectInvestInformations => Set<ProjectInvestInformation>();

    #endregion Enterprise

    #region Travel
    public DbSet<TourGuide> TourGuides => Set<TourGuide>();
    public DbSet<TravelHandbook> TravelHandbooks => Set<TravelHandbook>();

    #endregion Travel

    #region Agriculture
    public DbSet<AgriculturalEngineeringCategory> AgriculturalEngineeringCategories => Set<AgriculturalEngineeringCategory>();
    public DbSet<AgriculturalEngineering> AgriculturalEngineerings => Set<AgriculturalEngineering>();
    #endregion Agriculture

    #region Education
    public DbSet<School> Schools => Set<School>();
    public DbSet<SchoolType> SchoolTypes => Set<SchoolType>();
    public DbSet<EduDocumentType> EduDocumentTypes => Set<EduDocumentType>();
    public DbSet<EduDocumentCatalogue> EduDocumentCatalogues => Set<EduDocumentCatalogue>();
    public DbSet<EduDocumentCategory> EduDocumentCategories => Set<EduDocumentCategory>();
    public DbSet<EduDocument> EduDocument => Set<EduDocument>();

    #endregion Education

    #region Medical
    public DbSet<Disease> Diseases => Set<Disease>();
    public DbSet<MedicalHotline> MedicalHotlines => Set<MedicalHotline>();

    public DbSet<Drug> Drugs => Set<Drug>();
    
    #endregion Medical


    #region Food
    public DbSet<FoodWarning> FoodWarnings => Set<FoodWarning>();
    public DbSet<FoodFactory> FoodFactories => Set<FoodFactory>();
    #endregion Food


    public DbSet<SeaGame> SeaGames => Set<SeaGame>();

    #region ThueNha
    public DbSet<DienTichNha> DienTichNhas => Set<DienTichNha>();
    public DbSet<MucGiaThueNha> MucGiaThueNhas => Set<MucGiaThueNha>();
    public DbSet<ThoiGianThueNha> ThoiGianThueNhas => Set<ThoiGianThueNha>();
    public DbSet<LoaiNha> LoaiNhas => Set<LoaiNha>();
    public DbSet<ThueNha> ThueNhas => Set<ThueNha>();

    #endregion ThueNha

    #region Ho so dien tu Dien Bien
    public DbSet<HoSoDienTu> HoSoDienTus => Set<HoSoDienTu>();
    public DbSet<GiayToHoSoDienTu> GiayToHoSoDienTus => Set<GiayToHoSoDienTu>();
    public DbSet<NhomHoSoDienTu> NhomHoSoDienTus => Set<NhomHoSoDienTu>();
    public DbSet<LoaiHoSoDienTu> LoaiHoSoDienTus => Set<LoaiHoSoDienTu>();
    #endregion

    #region ThongTinChinhQuyen
    public DbSet<GovNewCategory> GovNewCategories => Set<GovNewCategory>();
    public DbSet<GovNew> GovNews => Set<GovNew>();

    #endregion ThongTinChinhQuyen

    public DbSet<AppConfig> AppConfigs => Set<AppConfig>();
    public DbSet<LoginLog> LoginLogs => Set<LoginLog>();

    public DbSet<OrganizationUnit> OrganizationUnits => Set<OrganizationUnit>();
    public DbSet<OrganizationUnitUser> OrganizationUnitUsers => Set<OrganizationUnitUser>();

    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema(SchemaNames.Catalog);
    }
}