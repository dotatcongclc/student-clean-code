// Vd1Clean_Commented.java
// Refactor từ Vd1.cs (BAD CODE) -> CLEAN CODE với OOP
// MỌI chỗ sửa đều có comment chỉ rõ: lỗi cũ -> cách sửa -> loại lỗi (code smell / design issue)

import java.util.*;

// Sửa code lần 2: Tạo class Student, Teacher, Course, Enrollment, Grade 

// ========================== ENTITIES ==========================

// ❌ LỖI: Trước dùng ArrayList<String> lưu "id|name|age|gpa"
//        => Magic string / Stringly-typed data / Primitive Obsession
// ✅ SỬA: Tạo class Student với thuộc tính, getter/setter, toString (Encapsulation)
class Student {
    // ✅ Sửa: fields private => encapsulation (Information hiding)
    private String id;
    private String name;
    private int age;
    private double gpa;

    // ✅ Sửa: constructor rõ ràng
    public Student(String id, String name, int age, double gpa) {
        this.id = id; this.name = name; this.age = age; this.gpa = gpa;
    }

    // ✅ Sửa: getter/setter thay vì thao tác chuỗi
    public String getId() { return id; }
    public String getName() { return name; }
    public int getAge() { return age; }
    public double getGpa() { return gpa; }

    public void setName(String name) { this.name = name; }
    public void setAge(int age) { this.age = age; }
    public void setGpa(double gpa) { this.gpa = gpa; }

    @Override
    public String toString() {
        return "ID:" + id + " Name:" + name + " Age:" + age + " GPA:" + gpa;
    }
}

// Teacher entity
// ❌ LỖI: Trước lưu "id|name|major" => Stringly-typed, duplicate logic with students
// ✅ SỬA: Tạo class Teacher => giảm duplication, rõ ràng
class Teacher {
    private String id;
    private String name;
    private String major;

    public Teacher(String id, String name, String major) {
        this.id = id; this.name = name; this.major = major;
    }

    public String getId() { return id; }
    public String getName() { return name; }
    public String getMajor() { return major; }

    public void setName(String name) { this.name = name; }
    public void setMajor(String major) { this.major = major; }

    @Override
    public String toString() {
        return "ID:" + id + " Name:" + name + " Major:" + major;
    }
}

// Course entity
// ❌ LỖI: Trước lưu "id|name|credits" => poor structure
// ✅ SỬA: Class Course
class Course {
    private String id;
    private String name;
    private int credits;

    public Course(String id, String name, int credits) {
        this.id = id; this.name = name; this.credits = credits;
    }

    public String getId() { return id; }
    public String getName() { return name; }
    public int getCredits() { return credits; }

    public void setName(String name) { this.name = name; }
    public void setCredits(int credits) { this.credits = credits; }

    @Override
    public String toString() {
        return "ID:" + id + " Name:" + name + " Credits:" + credits;
    }
}

// Enrollment entity
class Enrollment {
    private String studentId;
    private String courseId;

    public Enrollment(String studentId, String courseId) {
        this.studentId = studentId; this.courseId = courseId;
    }

    public String getStudentId() { return studentId; }
    public String getCourseId() { return courseId; }

    @Override
    public String toString() {
        return "SV:" + studentId + " dang ky MH:" + courseId;
    }
}

// Grade entity
class Grade {
    private String studentId;
    private String courseId;
    private double score;

    public Grade(String studentId, String courseId, double score) {
        this.studentId = studentId; this.courseId = courseId; this.score = score;
    }

    public String getStudentId() { return studentId; }
    public String getCourseId() { return courseId; }
    public double getScore() { return score; }

    public void setScore(double score) { this.score = score; }

    @Override
    public String toString() {
        return "SV:" + studentId + " MH:" + courseId + " Diem:" + score;
    }
}

// ========================== MAIN PROGRAM ==========================
public class CleanSchoolProgram {
    // ❌ LỖI: Dùng ArrayList<String> & split ở nhiều nơi -> duplication, fragile parsing
    // ✅ SỬA: Dùng List<Entity> để tăng type-safety và maintainability
    private static List<Student> students = new ArrayList<>();
    private static List<Teacher> teachers = new ArrayList<>();
    private static List<Course> courses = new ArrayList<>();
    private static List<Enrollment> enrollments = new ArrayList<>();
    private static List<Grade> grades = new ArrayList<>();
    private static Scanner sc = new Scanner(System.in);

    public static void main(String[] args) {
        int menu = 0;
        while (menu != 99) {
            System.out.println("===== MENU CHINH =====");
            System.out.println("1. Quan ly Sinh vien");
            System.out.println("2. Quan ly Giao vien");
            System.out.println("3. Quan ly Mon hoc");
            System.out.println("4. Quan ly Dang ky hoc");
            System.out.println("5. Quan ly Diem");
            System.out.println("6. Bao cao tong hop");
            System.out.println("99. Thoat");
            System.out.print("Nhap lua chon: ");
            try {
                menu = Integer.parseInt(sc.nextLine());
            } catch (NumberFormatException ex) {
                System.out.println("Nhap so hop le!");
                continue;
            }

            // ❌ LỖI: Bản gốc viết mọi thứ trong main => God Method, vi phạm SRP
            // ✅ SỬA: Tách ra các method manageStudents(), manageTeachers(), ... (Single Responsibility)
            switch (menu) {
                case 1: manageStudents(); break;
                case 2: manageTeachers(); break;
                case 3: manageCourses(); break;
                case 4: manageEnrollments(); break;
                case 5: manageGrades(); break;
                case 6: report(); break;
                case 99: System.out.println("Thoat."); break;
                default: System.out.println("Lua chon khong hop le."); break;
            }
        }
    }

    // ========================== STUDENT ==========================
    private static void manageStudents() {
        int smenu = 0;
        while (smenu != 9) {
            System.out.println("--- QUAN LY SINH VIEN ---");
            System.out.println("1. Them SV");
            System.out.println("2. Xoa SV");
            System.out.println("3. Cap nhat SV");
            System.out.println("4. Hien thi tat ca SV");
            System.out.println("5. Tim SV GPA > 8");
            System.out.println("9. Quay lai");
            System.out.print("Lua chon: ");
            try {
                smenu = Integer.parseInt(sc.nextLine());
            } catch (NumberFormatException ex) {
                System.out.println("Nhap so hop le!");
                continue;
            }

            switch (smenu) {
                case 1: // Thêm SV
                    // ❌ LỖI cũ: students.add(id + "|" + name + "|" + age + "|" + gpa);
                    //    -> Stringly-typed; parsing fragile; violation of encapsulation
                    // ✅ SỬA: tạo Student object và add vào List<Student]
                    System.out.print("Nhap id: ");
                    String id = sc.nextLine();
                    System.out.print("Nhap ten: ");
                    String name = sc.nextLine();
                    System.out.print("Nhap tuoi: ");
                    int age = readIntSafe();
                    System.out.print("Nhap GPA: ");
                    double gpa = readDoubleSafe();
                    students.add(new Student(id, name, age, gpa));
                    break;

                case 2: // Xóa SV
                    // ❌ LỖI cũ: dùng for + split("|") để tìm rồi remove => verbose và dễ lỗi index
                    // ✅ SỬA: sử dụng removeIf với lambda (clean + safe)
                    System.out.print("Nhap id can xoa: ");
                    String delId = sc.nextLine();
                    // Lỗi có thể: không kiểm tra tồn tại -> ở đây removeIf handle luôn
                    students.removeIf(s -> s.getId().equals(delId));
                    break;

                case 3: // Cập nhật SV
                    // ❌ LỖI cũ: parse chuỗi và set lại string -> fragile
                    // ✅ SỬA: tìm object và gọi setter
                    System.out.print("Nhap id can cap nhat: ");
                    String upId = sc.nextLine();
                    boolean found = false;
                    for (Student s : students) {
                        if (s.getId().equals(upId)) {
                            found = true;
                            System.out.print("Nhap ten moi: "); s.setName(sc.nextLine());
                            System.out.print("Nhap tuoi moi: "); s.setAge(readIntSafe());
                            System.out.print("Nhap GPA moi: "); s.setGpa(readDoubleSafe());
                            System.out.println("Da cap nhat.");
                        }
                    }
                    if (!found) System.out.println("Khong tim thay SV.");
                    break;

                case 4: // Hiển thị tất cả SV
                    // ✅ Dùng forEach + toString (cohesion)
                    students.forEach(System.out::println);
                    break;

                case 5: // Tìm SV GPA > 8
                    // ❌ LỖI cũ: parse string -> dùng Double.parseDouble(...) nhiều chỗ
                    // ✅ SỬA: dùng object property, stream filter
                    students.stream().filter(s -> s.getGpa() > 8.0).forEach(System.out::println);
                    break;

                case 9:
                    break;

                default:
                    System.out.println("Lua chon khong hop le.");
            }
        }
    }

    // ========================== TEACHER ==========================
    private static void manageTeachers() {
        int tmenu = 0;
        while (tmenu != 9) {
            System.out.println("--- QUAN LY GIAO VIEN ---");
            System.out.println("1. Them GV");
            System.out.println("2. Xoa GV");
            System.out.println("3. Cap nhat GV");
            System.out.println("4. Hien thi GV");
            System.out.println("9. Quay lai");
            System.out.print("Lua chon: ");
            try { tmenu = Integer.parseInt(sc.nextLine()); } catch (Exception e) { System.out.println("Nhap so!"); continue; }

            switch (tmenu) {
                case 1:
                    System.out.print("Nhap id GV: "); String id = sc.nextLine();
                    System.out.print("Nhap ten GV: "); String name = sc.nextLine();
                    System.out.print("Nhap chuyen mon: "); String major = sc.nextLine();
                    teachers.add(new Teacher(id, name, major));
                    break;
                case 2:
                    System.out.print("Nhap id GV can xoa: "); String delId = sc.nextLine();
                    teachers.removeIf(t -> t.getId().equals(delId));
                    break;
                case 3:
                    System.out.print("Nhap id GV cap nhat: "); String upId = sc.nextLine();
                    boolean found = false;
                    for (Teacher t : teachers) {
                        if (t.getId().equals(upId)) {
                            found = true;
                            System.out.print("Nhap ten moi: "); t.setName(sc.nextLine());
                            System.out.print("Nhap chuyen mon moi: "); t.setMajor(sc.nextLine());
                            System.out.println("Da cap nhat GV.");
                        }
                    }
                    if (!found) System.out.println("Khong tim thay GV.");
                    break;
                case 4:
                    teachers.forEach(System.out::println);
                    break;
                case 9:
                    break;
                default:
                    System.out.println("Lua chon khong hop le.");
            }
        }
    }

    // ========================== COURSE ==========================
    private static void manageCourses() {
        int cmenu = 0;
        while (cmenu != 9) {
            System.out.println("--- QUAN LY MON HOC ---");
            System.out.println("1. Them MH");
            System.out.println("2. Xoa MH");
            System.out.println("3. Cap nhat MH");
            System.out.println("4. Hien thi MH");
            System.out.println("9. Quay lai");
            System.out.print("Lua chon: ");
            try { cmenu = Integer.parseInt(sc.nextLine()); } catch (Exception e) { System.out.println("Nhap so!"); continue; }

            switch (cmenu) {
                case 1:
                    System.out.print("Nhap id MH: "); String id = sc.nextLine();
                    System.out.print("Nhap ten MH: "); String name = sc.nextLine();
                    System.out.print("Nhap so tin chi: "); int tc = readIntSafe();
                    courses.add(new Course(id, name, tc));
                    break;
                case 2:
                    System.out.print("Nhap id MH can xoa: "); String delId = sc.nextLine();
                    courses.removeIf(c -> c.getId().equals(delId));
                    break;
                case 3:
                    System.out.print("Nhap id MH cap nhat: "); String upId = sc.nextLine();
                    boolean found = false;
                    for (Course c : courses) {
                        if (c.getId().equals(upId)) {
                            found = true;
                            System.out.print("Nhap ten moi: "); c.setName(sc.nextLine());
                            System.out.print("Nhap tin chi moi: "); c.setCredits(readIntSafe());
                            System.out.println("Da cap nhat MH.");
                        }
                    }
                    if (!found) System.out.println("Khong tim thay MH.");
                    break;
                case 4:
                    courses.forEach(System.out::println);
                    break;
                case 9:
                    break;
                default:
                    System.out.println("Lua chon khong hop le.");
            }
        }
    }

    // ========================== ENROLLMENT ==========================
    private static void manageEnrollments() {
        int emenu = 0;
        while (emenu != 9) {
            System.out.println("--- QUAN LY DANG KY HOC ---");
            System.out.println("1. Dang ky mon hoc");
            System.out.println("2. Huy dang ky");
            System.out.println("3. Xem tat ca dang ky");
            System.out.println("9. Quay lai");
            System.out.print("Lua chon: ");
            try { emenu = Integer.parseInt(sc.nextLine()); } catch (Exception e) { System.out.println("Nhap so!"); continue; }

            switch (emenu) {
                case 1:
                    System.out.print("Nhap id SV: "); String sid = sc.nextLine();
                    System.out.print("Nhap id MH: "); String cid = sc.nextLine();
                    // ❌ LỖI cũ: Thêm chuỗi "sid|cid" -> khó truy vấn
                    // ✅ SỬA: Thêm Enrollment object
                    enrollments.add(new Enrollment(sid, cid));
                    break;
                case 2:
                    System.out.print("Nhap id SV: "); String rsid = sc.nextLine();
                    System.out.print("Nhap id MH: "); String rcid = sc.nextLine();
                    enrollments.removeIf(e -> e.getStudentId().equals(rsid) && e.getCourseId().equals(rcid));
                    break;
                case 3:
                    enrollments.forEach(System.out::println);
                    break;
                case 9:
                    break;
                default:
                    System.out.println("Lua chon khong hop le.");
            }
        }
    }

    // ========================== GRADE ==========================
    private static void manageGrades() {
        int gmenu = 0;
        while (gmenu != 9) {
            System.out.println("--- QUAN LY DIEM ---");
            System.out.println("1. Nhap diem");
            System.out.println("2. Cap nhat diem");
            System.out.println("3. Hien thi diem");
            System.out.println("9. Quay lai");
            System.out.print("Lua chon: ");
            try { gmenu = Integer.parseInt(sc.nextLine()); } catch (Exception e) { System.out.println("Nhap so!"); continue; }

            switch (gmenu) {
                case 1:
                    System.out.print("Nhap id SV: "); String sid = sc.nextLine();
                    System.out.print("Nhap id MH: "); String cid = sc.nextLine();
                    System.out.print("Nhap diem: "); double d = readDoubleSafe();
                    grades.add(new Grade(sid, cid, d));
                    break;
                case 2:
                    System.out.print("Nhap id SV: "); String usid = sc.nextLine();
                    System.out.print("Nhap id MH: "); String ucid = sc.nextLine();
                    boolean found = false;
                    for (Grade gr : grades) {
                        if (gr.getStudentId().equals(usid) && gr.getCourseId().equals(ucid)) {
                            found = true;
                            System.out.print("Nhap diem moi: "); gr.setScore(readDoubleSafe());
                            System.out.println("Da cap nhat diem.");
                        }
                    }
                    if (!found) System.out.println("Khong tim thay diem.");
                    break;
                case 3:
                    grades.forEach(System.out::println);
                    break;
                case 9:
                    break;
                default:
                    System.out.println("Lua chon khong hop le.");
            }
        }
    }

    // ========================== REPORT ==========================
    private static void report() {
        // ❌ LỖI cũ: nested loops + parsings -> messy, unreadable
        // ✅ SỬA: Duyệt object trực tiếp, logic rõ ràng (tuy vẫn nested, có thể tối ưu)
        System.out.println("=== BAO CAO ===");
        for (Student s : students) {
            System.out.println("Sinh vien: " + s.getName());
            for (Enrollment e : enrollments) {
                if (e.getStudentId().equals(s.getId())) {
                    for (Course c : courses) {
                        if (c.getId().equals(e.getCourseId())) {
                            System.out.print(" - Mon hoc: " + c.getName());
                            // Tìm điểm tương ứng
                            for (Grade g : grades) {
                                if (g.getStudentId().equals(s.getId()) && g.getCourseId().equals(c.getId())) {
                                    System.out.print(" | Diem: " + g.getScore());
                                }
                            }
                            System.out.println();
                        }
                    }
                }
            }
        }
    }

    // ========================== HELPERS ==========================
    // ✅ Thêm hàm đọc số an toàn: giảm NumberFormatException (tăng robustness)
    private static int readIntSafe() {
        while (true) {
            try { return Integer.parseInt(sc.nextLine()); }
            catch (NumberFormatException ex) { System.out.print("Nhap so nguyen hop le: "); }
        }
    }

    private static double readDoubleSafe() {
        while (true) {
            try { return Double.parseDouble(sc.nextLine()); }
            catch (NumberFormatException ex) { System.out.print("Nhap so thuc hop le: "); }
        }
    }
}
