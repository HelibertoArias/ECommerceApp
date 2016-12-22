namespace ECommerceApp.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirtsName { get; set; }
        public string LastName { get; set; }
        public string Photo { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsCustomer { get; set; }
        public bool IdSupplier { get; set; }
    }
}