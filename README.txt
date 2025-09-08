Đỗ Tất Công - CNTT K20 CLC
Đặng Lê Thanh Tú - CNTT K20 CLC
Đằng Văn Huy - CNTT K20 CLC
Dương Thành Đạt - CNTT K20 CLC

___ CÁCH DEMO ________________________________________

----1. CẤU HÌNH GIT (chỉ làm 1 lần trên máy).
git config --global user.name "dotatconglc"
git config --global user.email "congdo12042003@gmail.com"

---- 2. KHỞI TẠO REPO ( NẾU CHƯA)
cd C:\Users\dtcon\Desktop\gitcode
git init

----3.COMMIT LẦN 1 : code bẩn
git add Vd1.cs Vd2.cs
git commit -m "Initial commit: add dirty code Vd1.cs and Vd2.cs"

----4. COMMIT LẦN 2  FILE ĐỊNH DANH ( READ.ME)
echo "Đỗ Tất Công - CNTT K20 CLC" > README.txt
git add README.txt
git commit -m "Add identification file README.txt"

----5. COMMIT LẦN  3: code sạch (đã refactor)
git add Vd1.cs Vd2.cs
git commit -m "Refactor: clean code for Vd1.cs and Vd2.cs using OOP"


---- CẤC THANH VIEN TRONG NHÓM LAM----
B1: chạy lệnh để lấy code gốc:
git clone https://github.com/dotatconglc/student-clean-code.git
cd student-clean-code

B2:
 chạy lệnh:  
git checkout -b feature-fix-vd1
#tiến hành sửa code sửa code trong VS xong chạy lệnh sau.
git add Vd1.cs
git commit -m "Fix bug in Vd1 student update function"
git push origin feature-fix-vd1
====>   Lúc này GitHub sẽ có branch mới feature-fix-vd1.

B3.
Tạo Pull Request (PR)
Vào GitHub → chọn branch feature-fix-vd1 → New Pull Request.
Thảo luận trong PR → nhóm review code.
Nếu OK → Merge vào main.

B4.
CHẠY LỆNH 
git checkout main
git pull origin main





