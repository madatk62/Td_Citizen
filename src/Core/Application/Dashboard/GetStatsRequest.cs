using TD.CitizenAPI.Application.Identity.Roles;
using TD.CitizenAPI.Application.Identity.Users;

namespace TD.CitizenAPI.Application.Dashboard;

public class GetStatsRequest : IRequest<StatsDto>
{
}

public class GetStatsRequestHandler : IRequestHandler<GetStatsRequest, StatsDto>
{
    private readonly IThueNhaService _thueNhaService;
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;
    private readonly IReadRepository<Place> _placeRepo;
    private readonly IReadRepository<Product> _productRepo;

    private readonly IReadRepository<ThueNha> _thueNhaRepo;
    private readonly IReadRepository<GovNew> _govNewRepo;
    private readonly IReadRepository<Recruitment> _recruitmentRepo;
    private readonly IReadRepository<AlertInformation> _alertInformationRepo;
    private readonly IReadRepository<Vehicle> _vehicleRepo;
    private readonly IReadRepository<EcommerceCategory> _categoryRepo;

    private readonly IStringLocalizer<GetStatsRequestHandler> _localizer;

    public GetStatsRequestHandler(IUserService userService, IRoleService roleService, IReadRepository<Place> placeRepo, IReadRepository<Product> productRepo, IReadRepository<ThueNha> thueNhaRepo, IReadRepository<GovNew> govNewRepo, IReadRepository<Recruitment> recruitmentRepo, IReadRepository<AlertInformation> alertInformationRepo, IReadRepository<Vehicle> vehicleRepo, IReadRepository<EcommerceCategory> categoryRepo, IStringLocalizer<GetStatsRequestHandler> localizer, IThueNhaService thueNhaService)
    {
        _thueNhaService = thueNhaService;
        _govNewRepo = govNewRepo;
        _recruitmentRepo = recruitmentRepo;
        _alertInformationRepo = alertInformationRepo;
        _vehicleRepo = vehicleRepo;
        _placeRepo = placeRepo;
        _userService = userService;
        _roleService = roleService;
        _thueNhaRepo = thueNhaRepo;
        _productRepo = productRepo;
        _categoryRepo = categoryRepo;
        _localizer = localizer;
    }

    public async Task<StatsDto> Handle(GetStatsRequest request, CancellationToken cancellationToken)
    {
        var stats = new StatsDto
        {
            CitizenIsActive = await _userService.GetCountFilterAsync(new UserListFilter() { Type = 0, IsActive = true }, cancellationToken),
            CitizenIsVerified = await _userService.GetCountFilterAsync(new UserListFilter() { Type = 0, IsVerified = true }, cancellationToken),
            UserIsActive = await _userService.GetCountFilterAsync(new UserListFilter() { Type = 1, IsActive = true }, cancellationToken),
            UserIsVerified = await _userService.GetCountFilterAsync(new UserListFilter() { Type = 1, IsVerified = true }, cancellationToken),
            UserSync = await _userService.GetCountFilterAsync(new UserListFilter() { Type = 1, IsSync = true }, cancellationToken),
            PlaceCount = await _placeRepo.CountAsync(cancellationToken),
            ProductCount = await _productRepo.CountAsync(cancellationToken),
            ThueNhaCount = await _thueNhaRepo.CountAsync(cancellationToken),
            GovNewCount = await _govNewRepo.CountAsync(cancellationToken),
            RecruitmentCount = await _recruitmentRepo.CountAsync(cancellationToken),
            AlertInformationCount = await _alertInformationRepo.CountAsync(cancellationToken),
            VehicleCount = await _vehicleRepo.CountAsync(cancellationToken),
            CategoryCount = await _categoryRepo.CountAsync(cancellationToken),

        };

        int selectedYear = DateTime.Now.Year;
        double[] productsFigure = new double[12];

        for (int i = 1; i <= 12; i++)
        {
            int month = i;
            var filterStartDate = new DateTime(selectedYear, month, 01);
            var filterEndDate = new DateTime(selectedYear, month, DateTime.DaysInMonth(selectedYear, month), 23, 59, 59); // Monthly Based

            var productSpec = new AuditableEntitiesByCreatedOnBetweenSpec<Product>(filterStartDate, filterEndDate);

            productsFigure[i - 1] = await _productRepo.CountAsync(productSpec, cancellationToken);
        }

        stats.DataEnterBarChart.Add(new ChartSeries { Name = _localizer["Products"], Data = productsFigure });

        stats.ThueNhaByGia = _thueNhaService.GroupByCategory();
        stats.CongViecByMucLuong = _thueNhaService.CongViecByMucLuong();
        stats.CongViecByKinhNghiem = _thueNhaService.CongViecByKinhNghiem();
        stats.CongViecByNgheNghiep = _thueNhaService.CongViecByNgheNghiep();
        stats.SanPhamByDanhMuc = _thueNhaService.SanPhamByDanhMuc();
        stats.CongViecByLoaiHinhCV = _thueNhaService.CongViecByLoaiHinhCV();
        stats.CongViecByViTri = _thueNhaService.CongViecByViTri();
        stats.CongViecByBangCap = _thueNhaService.CongViecByBangCap();
        stats.CongViecByGioiTinh = _thueNhaService.CongViecByGioiTinh();
        stats.CongViecByAge = _thueNhaService.CongViecByAge();
        stats.CongViecByCongty = _thueNhaService.CongViecByCongty();
        stats.XeKhachByLoaiPT = _thueNhaService.XeKhachByLoaiPT();
        stats.XeKhachByCongTy = _thueNhaService.XeKhachByCongTy();
        stats.TinTucByDanhMuc = _thueNhaService.TinTucByDanhMuc();
        return stats;
    }
}