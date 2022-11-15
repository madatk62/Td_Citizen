using TD.CitizenAPI.Application.Common.Interfaces;
using TD.CitizenAPI.Application.Dashboard;
using TD.CitizenAPI.Infrastructure.Persistence.Context;

namespace TD.CitizenAPI.Infrastructure.Common.Services;

public class ThueNhaService : IThueNhaService
{
    private readonly ApplicationDbContext _db;

    public ThueNhaService(ApplicationDbContext db)
    {
        _db = db;
    }

    public List<ChartItem>? GroupByCategory()
    {

        var tmp = _db.ThueNhas.GroupBy(x => x.DienTichNha.Name).Select(g => new ChartItem
        {
                Name = g.Key,
                Value = g.Count()
        }).ToList();
        return tmp;
    }

    public List<ChartItem>? CongViecByMucLuong()
    {

        var tmp = _db.Recruitments.GroupBy(x => x.Salary.Name).Select(g => new ChartItem
        {
            Name = g.Key,
            Value = g.Count()
        })
            .ToList();


        return tmp;
    }
    public List<ChartItem>? CongViecByKinhNghiem()
    {


        var tmp = _db.Recruitments.GroupBy(x => x.Experience.Name ).Select(g => new ChartItem
        {
            Name = g.Key,
            Value = g.Count()
        })
            .ToList();


        return tmp;
    }
    public List<ChartItem>? CongViecByNgheNghiep()
    {


        var tmp = _db.Recruitments.GroupBy(x => x.JobName.Name).Select(g => new ChartItem
        {
            Name = g.Key,
            Value = g.Count()
        })
            .ToList();


        return tmp;
    }
    public List<ChartItem>? CongViecByGioiTinh()
    {


        var tmp = _db.Recruitments.GroupBy(x => x.Gender).Select(g => new ChartItem
        {
            Name = g.Key,
            Value = g.Count()
        })
            .ToList();


        return tmp;
    }
    public List<ChartItem>? SanPhamByDanhMuc()
    {
        var tmp = _db.MarketProducts.GroupBy(x => x.MarketCategory.Name).Select(g => new ChartItem
        {
            Name = g.Key,
            Value = g.Count()
        })
            .ToList();
        return tmp;
    }
    public List<ChartItem>? CongViecByLoaiHinhCV()
    {
        var tmp = _db.Recruitments.GroupBy(x => x.JobType.Name).Select(g => new ChartItem
        {
            Name = g.Key,
            Value = g.Count()
        })
            .ToList();


        return tmp;
    }
    public List<ChartItem>? CongViecByViTri()
    {
        var tmp = _db.Recruitments.GroupBy(x => x.JobPosition.Name).Select(g => new ChartItem
        {
            Name = g.Key,
            Value = g.Count()
        })
            .ToList();


        return tmp;
    }
    public List<ChartItem>? CongViecByAge()
    {
        var tmp = _db.Recruitments.GroupBy(x => x.JobAge.Name).Select(g => new ChartItem
        {
            Name = g.Key,
            Value = g.Count()
        })
            .ToList();


        return tmp;
    }
    public List<ChartItem>? CongViecByBangCap()
    {
        var tmp = _db.Recruitments.GroupBy(x => x.Degree.Name).Select(g => new ChartItem
        {
            Name = g.Key,
            Value = g.Count()
        })
            .ToList();


        return tmp;
    }    public List<ChartItem>? CongViecByCongty()
    {
        var tmp = _db.Recruitments.GroupBy(x => x.Company.Name).Select(g => new ChartItem
        {
            Name = g.Key,
            Value = g.Count()
        })
            .ToList();


        return tmp;
    }
    public List<ChartItem>? XeKhachByLoaiPT()
    {
        var tmp = _db.Vehicles.GroupBy(x => x.VehicleType.Name).Select(g => new ChartItem
        {
            Name = g.Key,
            Value = g.Count()
        })
            .ToList();


        return tmp;
    }
    public List<ChartItem>? XeKhachByCongTy()
    {
        var tmp = _db.Vehicles.GroupBy(x => x.Company.Name).Select(g => new ChartItem
        {
            Name = g.Key,
            Value = g.Count()
        })
            .ToList();


        return tmp; }



        public List<ChartItem>? TinTucByDanhMuc()
        {
            var tmp = _db.GovNews.GroupBy(x => x.GovNewCategory.Name).Select(g => new ChartItem
            {
                Name = g.Key,
                Value = g.Count()
            })
                .ToList();


            return tmp;
        }

    }





