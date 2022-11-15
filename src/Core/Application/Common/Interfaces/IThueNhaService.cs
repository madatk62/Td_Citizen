using TD.CitizenAPI.Application.Dashboard;

namespace TD.CitizenAPI.Application.Common.Interfaces;

public interface IThueNhaService : ITransientService
{
    List<ChartItem>? GroupByCategory();

    List<ChartItem>? CongViecByMucLuong();


    List<ChartItem>? CongViecByKinhNghiem();
    List<ChartItem>? CongViecByNgheNghiep();
    List<ChartItem>? CongViecByGioiTinh();
    List<ChartItem>? CongViecByAge();
    List<ChartItem>? CongViecByCongty();

    List<ChartItem>? SanPhamByDanhMuc();

    List<ChartItem>? CongViecByLoaiHinhCV();
    List<ChartItem>? CongViecByViTri();
    List<ChartItem>? CongViecByBangCap();

    List<ChartItem>? XeKhachByLoaiPT();
    List<ChartItem>? XeKhachByCongTy();
    List<ChartItem>? TinTucByDanhMuc();







}