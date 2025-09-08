// Vd2Clean_Commented.cs
// Refactor từ Vd2.cs (BAD CODE) -> CLEAN CODE
// Có comment rõ chỗ lỗi và loại lỗi (code smell/design issue)

using System;
using System.Collections.Generic;
using System.Linq;

// ========================== ENTITY CLASSES ==========================

// ❌ LỖI: Trước lưu nhân viên bằng string "id|name|age|salary"
//        -> Magic string, Primitive Obsession, khó bảo trì
// ✅ SỬA: Tạo class Employee với thuộc tính rõ ràng (Encapsulation)
class Employee
{
    // ✅ Sửa: private fields + property
    public string Id { get; set; }     
    public string Name { get; set; }
    public int Age { get; set; }
    public double Salary { get; set; }

    public Employee(string id, string name, int age, double salary)
    {
        Id = id;
        Name = name;
        Age = age;
        Salary = salary;
    }

    public override string ToString()
    {
        return $"ID:{Id} Name:{Name} Age:{Age} Salary:{Salary}";
    }
}

// ========================== MAIN PROGRAM ==========================

class CleanEmployeeProgram
{
    // ❌ LỖI: Trước dùng List<string> và split("|") để xử lý nhân viên
    // ✅ SỬA: Dùng List<Employee> -> type safety, dễ bảo trì
    private static List<Employee> employees = new List<Employee>();

    static void Main()
    {
        int menu = 0;
        while (menu != 99)
        {
            Console.WriteLine("===== MENU QUAN LY NHAN VIEN =====");
            Console.WriteLine("1. Them nhan vien");
            Console.WriteLine("2. Xoa nhan vien");
            Console.WriteLine("3. Cap nhat thong tin nhan vien");
            Console.WriteLine("4. Hien thi tat ca nhan vien");
            Console.WriteLine("5. Tim nhan vien co luong > 10tr");
            Console.WriteLine("99. Thoat");
            Console.Write("Nhap lua chon: ");

            // ❌ LỖI: Bản cũ không validate input -> crash nếu nhập sai
            // ✅ SỬA: Dùng int.TryParse để tránh crash
            if (!int.TryParse(Console.ReadLine(), out menu))
            {
                Console.WriteLine("Nhap so hop le!");
                continue;
            }

            switch (menu)
            {
                case 1: AddEmployee(); break;
                case 2: DeleteEmployee(); break;
                case 3: UpdateEmployee(); break;
                case 4: ShowEmployees(); break;
                case 5: FindHighSalary(); break;
                case 99: Console.WriteLine("Thoat chuong trinh."); break;
                default: Console.WriteLine("Lua chon khong hop le."); break;
            }
        }
    }

    // ========================== EMPLOYEE MANAGEMENT ==========================

    private static void AddEmployee()
    {
        Console.Write("Nhap id: ");
        string id = Console.ReadLine();
        Console.Write("Nhap ten: ");
        string name = Console.ReadLine();
        Console.Write("Nhap tuoi: ");
        int age = ReadIntSafe();
        Console.Write("Nhap luong: ");
        double salary = ReadDoubleSafe();

        employees.Add(new Employee(id, name, age, salary));
        Console.WriteLine("Them nhan vien thanh cong!");
    }

    private static void DeleteEmployee()
    {
        Console.Write("Nhap id can xoa: ");
        string id = Console.ReadLine();

        // ❌ LỖI: Bản cũ duyệt for + split để xóa
        // ✅ SỬA: dùng RemoveAll + lambda
        int removed = employees.RemoveAll(e => e.Id == id);
        if (removed > 0)
            Console.WriteLine("Xoa thanh cong.");
        else
            Console.WriteLine("Khong tim thay nhan vien.");
    }

    private static void UpdateEmployee()
    {
        Console.Write("Nhap id can cap nhat: ");
        string id = Console.ReadLine();

        var emp = employees.FirstOrDefault(e => e.Id == id);
        if (emp != null)
        {
            Console.Write("Nhap ten moi: ");
            emp.Name = Console.ReadLine();
            Console.Write("Nhap tuoi moi: ");
            emp.Age = ReadIntSafe();
            Console.Write("Nhap luong moi: ");
            emp.Salary = ReadDoubleSafe();
            Console.WriteLine("Cap nhat thanh cong!");
        }
        else
        {
            Console.WriteLine("Khong tim thay nhan vien.");
        }
    }

    private static void ShowEmployees()
    {
        if (employees.Count == 0)
        {
            Console.WriteLine("Danh sach rong.");
            return;
        }

        employees.ForEach(Console.WriteLine);
    }

    private static void FindHighSalary()
    {
        // ❌ LỖI: Bản cũ parse string để lọc
        // ✅ SỬA: dùng LINQ trực tiếp với property
        var list = employees.Where(e => e.Salary > 10000000).ToList();
        if (list.Count == 0)
            Console.WriteLine("Khong co nhan vien nao luong > 10tr.");
        else
            list.ForEach(Console.WriteLine);
    }

    // ========================== HELPERS ==========================

    // ❌ LỖI: Bản cũ dùng int.Parse, double.Parse -> crash nếu nhập sai
    // ✅ SỬA: helper safe input
    private static int ReadIntSafe()
    {
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out int value))
                return value;
            Console.Write("Nhap so nguyen hop le: ");
        }
    }

    private static double ReadDoubleSafe()
    {
        while (true)
        {
            if (double.TryParse(Console.ReadLine(), out double value))
                return value;
            Console.Write("Nhap so thuc hop le: ");
        }
    }
}
