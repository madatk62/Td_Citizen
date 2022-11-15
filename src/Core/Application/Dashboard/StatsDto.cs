namespace TD.CitizenAPI.Application.Dashboard;

public class StatsDto
{
    public int CitizenIsActive { get; set; }
    public int CitizenIsVerified { get; set; }

    public int UserIsActive { get; set; }
    public int UserIsVerified { get; set; }
    public int UserSync { get; set; }


    public int PlaceCount { get; set; }
    public int ProductCount { get; set; }
    public int ThueNhaCount { get; set; }
    public int GovNewCount { get; set; }
    public int RecruitmentCount { get; set; }
    public int AlertInformationCount { get; set; }
    public int VehicleCount { get; set; }
    public int CategoryCount { get; set; }

    public int UserCount { get; set; }
    public int RoleCount { get; set; }

    

    public List<ChartSeries> DataEnterBarChart { get; set; } = new();
    public Dictionary<string, double>? ProductByBrandTypePieChart { get; set; }
    public List<ChartItem>? ThueNhaByGia { get; set; }
    public List<ChartItem>? CongViecByMucLuong { get; set; }
    public List<ChartItem>? CongViecByKinhNghiem { get; set; }
    public List<ChartItem>? CongViecByNgheNghiep { get; set; }
    public List<ChartItem>? CongViecByGioiTinh { get; set; }
    public List<ChartItem>? CongViecByAge { get; set; }
    public List<ChartItem>? CongViecByCongty { get; set; }
    public List<ChartItem>? SanPhamByDanhMuc { get; set; }
    public List<ChartItem>? CongViecByLoaiHinhCV { get; set; }
    public List<ChartItem>? CongViecByViTri { get; set; }
    public List<ChartItem>? CongViecByBangCap { get; set; }
    public List<ChartItem>? XeKhachByLoaiPT { get; set; }
    public List<ChartItem>? XeKhachByCongTy { get; set; }
    public List<ChartItem>? TinTucByDanhMuc { get; set; }

}

public class ChartSeries
{
    public string? Name { get; set; }
    public double[]? Data { get; set; }
}


public class ChartItem
{
    public string Name { get; set; }
    public int Value { get; set; }
}