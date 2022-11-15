namespace TD.CitizenAPI.Domain.Catalog;

public class School : AuditableEntity, IAggregateRoot
{
    public string Name { get; set; }
    public string? Code { get; set; }
    public string? PhoneNumber { get; set; }
    //Hieu truong
    public string? Principal { get; set; }
    //So dien thoai hieu truong
    public string? PrincipalPhone { get; set; }
    public string? Category { get; set; }
    //Loai hinh
    public string? Type { get; set; }
    //Phong giao duc va dao tao
    public string? Department { get; set; }
    //Quy Mo
    public string? Size { get; set; }
    //Chuan quoc gua muc do
    public string? Standard { get; set; }
    public string? Address { get; set; }
   

    public Guid? ProvinceId { get; set; }
    public Guid? DistrictId { get; set; }
    public Guid? CommuneId { get; set; }
    public Guid? SchoolTypeId { get; set; }

    

    public string? Description { get; set; }

    public string? Image { get; set; }

    public virtual Area? Province { get; set; }
    public virtual Area? District { get; set; }
    public virtual Area? Commune { get; set; }

    public School(string name, string? code, string? phoneNumber, string? principal, string? principalPhone, string? category, string? type, string? department, string? size, string? standard, string? address,string? description, string? image,Guid? provinceId, Guid? districtId, Guid? communeId, Guid? schoolTypeId)
    {
        Name = name;
        Code = code;
        PhoneNumber = phoneNumber;
        Principal = principal;
        PrincipalPhone = principalPhone;
        Category = category;
        Type = type;
        Department = department;
        Size = size;
        Standard = standard;
        Address = address;
       
        Description = description;
        Image = image;
        SchoolTypeId = schoolTypeId;
        ProvinceId = provinceId;
        DistrictId = districtId;
        CommuneId = communeId;
    }

    public School(string name, string? code, string? phoneNumber, string? principal, string? principalPhone, string? category, string? type, string? department, string? size, string? standard, string? address, string? description, string? image)
    {
        Name = name;
        Code = code;
        PhoneNumber = phoneNumber;
        Principal = principal;
        PrincipalPhone = principalPhone;
        Category = category;
        Type = type;
        Department = department;
        Size = size;
        Standard = standard;
        Address = address;
       
        Description = description;
        Image = image;
       
    }

    public School Update(string? name, string? code, string? phoneNumber, string? principal, string? principalPhone, string? category, string? type, string? department, string? size, string? standard, string? address, string? description, string? image, Guid? provinceId, Guid? districtId, Guid? communeId, Guid? schoolTypeId)
    {
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (code is not null && Code?.Equals(code) is not true) Code = code;
        if (image is not null && Image?.Equals(image) is not true) Image = image;
        if (description is not null && Description?.Equals(description) is not true) Description = description;

        if (phoneNumber is not null && PhoneNumber?.Equals(phoneNumber) is not true) PhoneNumber = phoneNumber;
        if (principal is not null && Principal?.Equals(principal) is not true) Principal = principal;
        if (principalPhone is not null && PrincipalPhone?.Equals(principalPhone) is not true) PrincipalPhone = principalPhone;
        if (category is not null && Category?.Equals(category) is not true) Category = category;
        if (type is not null && Type?.Equals(type) is not true) Type = type;
        if (department is not null && Department?.Equals(department) is not true) Department = department;
        if (size is not null && Size?.Equals(size) is not true) Size = size;
        if (standard is not null && Standard?.Equals(standard) is not true) Standard = standard;
        if (address is not null && Address?.Equals(address) is not true) Address = address;
       

        if (provinceId.HasValue && provinceId.Value != Guid.Empty && !ProvinceId.Equals(provinceId.Value)) ProvinceId = provinceId.Value;
        if (districtId.HasValue && districtId.Value != Guid.Empty && !DistrictId.Equals(districtId.Value)) DistrictId = districtId.Value;
        if (communeId.HasValue && communeId.Value != Guid.Empty && !CommuneId.Equals(communeId.Value)) CommuneId = communeId.Value;
        if (schoolTypeId.HasValue && schoolTypeId.Value != Guid.Empty && !SchoolTypeId.Equals(schoolTypeId.Value)) SchoolTypeId = schoolTypeId.Value;


        return this;

    }

    public School Update(string? name, string? code, string? phoneNumber, string? principal, string? principalPhone, string? category, string? type, string? department, string? size, string? standard, string? address,  string? description, string? image)
    {
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (code is not null && Code?.Equals(code) is not true) Code = code;
        if (image is not null && Image?.Equals(image) is not true) Image = image;
        if (description is not null && Description?.Equals(description) is not true) Description = description;

        if (phoneNumber is not null && PhoneNumber?.Equals(phoneNumber) is not true) PhoneNumber = phoneNumber;
        if (principal is not null && Principal?.Equals(principal) is not true) Principal = principal;
        if (principalPhone is not null && PrincipalPhone?.Equals(principalPhone) is not true) PrincipalPhone = principalPhone;
        if (category is not null && Category?.Equals(category) is not true) Category = category;
        if (type is not null && Type?.Equals(type) is not true) Type = type;
        if (department is not null && Department?.Equals(department) is not true) Department = department;
        if (size is not null && Size?.Equals(size) is not true) Size = size;
        if (standard is not null && Standard?.Equals(standard) is not true) Standard = standard;
        if (address is not null && Address?.Equals(address) is not true) Address = address;
       

      
        return this;
    }
}