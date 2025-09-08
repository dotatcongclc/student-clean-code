using System;
using System.Collections.Generic;
using System.Linq;

// ========== Interface chung ==========
public interface IManager<T>
{
    void Add(T item);
    void Remove(string id);
    void Update(string id, T updatedItem);
    List<T> GetAll();
    T FindById(string id);
    List<T> FindByName(string name);
}

// ========== Entity: Student ==========
public class Student
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public double GPA { get; set; }

    public override string ToString()
    {
        return $"ID:{Id}, Name:{Name}, Age:{Age}, GPA:{GPA}";
    }
}

// ========== Manager: chỉ lo dữ liệu ==========
public class StudentManager : IManager<Student>
{
    private List<Student> students = new List<Student>();

    public void Add(Student s)
    {
        if (string.IsNullOrWhiteSpace(s.Name))
            throw new ArgumentException("⚠ Tên không được để trống!");
        if (s.Age <= 0)
            throw new ArgumentException("⚠ Tuổi phải > 0!");
        if (s.GPA < 0 || s.GPA > 10)
            throw new ArgumentException("⚠ GPA phải trong khoảng 0–10!");

        students.Add(s);
    }

    public void Remove(string id)
    {
        students.RemoveAll(s => s.Id == id);
    }

    public void Update(string id, Student updatedItem)
    {
        var st = students.FirstOrDefault(s => s.Id == id);
        if (st != null)
        {
            st.Name = updatedItem.Name;
            st.Age = updatedItem.Age;
            st.GPA = updatedItem.GPA;
        }
    }

    public List<Student> GetAll() => students;

    public Student FindById(string id) =>
        students.FirstOrDefault(s => s.Id == id);

    public List<Student> FindByName(string name) =>
        students.Where(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).ToList();

    public List<Student> FindExcellent() =>
        students.Where(s => s.GPA > 8).ToList();

    public void SortByName() =>
        students = students.OrderBy(s => s.Name).ToList();

    public void SortByGPA() =>
        students = students.OrderByDescending(s => s.GPA).ToList();
}

// ========== Menu: chỉ lo giao diện ==========
public class StudentMenu
{
    private readonly StudentManager manager;
    public StudentMenu(StudentManager mgr) { manager = mgr; }

    private int ReadInt(string msg)
    {
        Console.Write(msg);
        if (!int.TryParse(Console.ReadLine(), out int v) || v < 0)
            throw new Exception("⚠ Giá trị phải là số nguyên >= 0!");
        return v;
    }

    private double ReadDouble(string msg)
    {
        Console.Write(msg);
        if (!double.TryParse(Console.ReadLine(), out double v) || v < 0 || v > 10)
            throw new Exception("⚠ GPA phải từ 0–10!");
        return v;
    }

    public void Show()
    {
        int choice = 0;
        while (choice != 9)
        {
            Console.WriteLine("\n--- QUẢN LÝ SINH VIÊN ---");
            Console.WriteLine("1. Thêm SV");
            Console.WriteLine("2. Xoá SV");
            Console.WriteLine("3. Cập nhật SV");
            Console.WriteLine("4. Hiển thị tất cả SV");
            Console.WriteLine("5. Tìm SV theo tên");
            Console.WriteLine("6. Tìm SV GPA > 8");
            Console.WriteLine("7. Sắp xếp theo tên");
            Console.WriteLine("8. Sắp xếp theo GPA");
            Console.WriteLine("9. Quay lại");
            Console.Write("👉 Chọn: ");

            if (!int.TryParse(Console.ReadLine(), out choice)) choice = 0;

            try
            {
                switch (choice)
                {
                    case 1:
                        var s = new Student
                        {
                            Id = Guid.NewGuid().ToString("N").Substring(0, 5),
                            Name = Prompt("Tên: "),
                            Age = ReadInt("Tuổi: "),
                            GPA = ReadDouble("GPA: ")
                        };
                        manager.Add(s);
                        Console.WriteLine("✅ Thêm thành công!");
                        break;

                    case 2:
                        Console.Write("Nhập ID xoá: ");
                        manager.Remove(Console.ReadLine());
                        Console.WriteLine("✅ Đã xoá (nếu tồn tại).");
                        break;

                    case 3:
                        Console.Write("Nhập ID cần cập nhật: ");
                        string id = Console.ReadLine();
                        var up = new Student
                        {
                            Id = id,
                            Name = Prompt("Tên mới: "),
                            Age = ReadInt("Tuổi mới: "),
                            GPA = ReadDouble("GPA mới: ")
                        };
                        manager.Update(id, up);
                        Console.WriteLine("✅ Cập nhật xong.");
                        break;

                    case 4:
                        var all = manager.GetAll();
                        if (all.Count == 0) Console.WriteLine("⚠ Chưa có sinh viên nào!");
                        else all.ForEach(Console.WriteLine);
                        break;

                    case 5:
                        string name = Prompt("Nhập tên: ");
                        var found = manager.FindByName(name);
                        if (found.Count == 0) Console.WriteLine("⚠ Không tìm thấy!");
                        else found.ForEach(Console.WriteLine);
                        break;

                    case 6:
                        var excellent = manager.FindExcellent();
                        if (excellent.Count == 0) Console.WriteLine("⚠ Không có SV GPA > 8!");
                        else excellent.ForEach(Console.WriteLine);
                        break;

                    case 7:
                        manager.SortByName();
                        Console.WriteLine("✅ Đã sắp xếp theo tên.");
                        break;

                    case 8:
                        manager.SortByGPA();
                        Console.WriteLine("✅ Đã sắp xếp theo GPA.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    private string Prompt(string msg)
    {
        Console.Write(msg);
        return Console.ReadLine();
    }
}

// ========== Chương trình chính ==========
public class Program
{
    public static void Main(string[] args)
    {
        var manager = new StudentManager();
        var menu = new StudentMenu(manager);
        menu.Show();
    }
}
