# TrainingWebsite
Do An Thuc Hanh
Xay dung website training system sử dụng .NET CORE MVC + MSSQL.
Cac chuc nang cua he thong:

+ Phân quyền.
+ Dang nhap/Dang ky.

**Trainee**
+ Xem danh sach khoa hoc, chi tiet khóa học(bài giảng, giảng viên, mục đích, list khóa học tương đồng, comment/reply)
+ Tìm kiếm khóa học
+ Enroll khóa học ( bắt buộc send mail cho Admin để đượ đăng ký)
+ Xem danh sách khóa học đã đăng ký
+ Chi tiết các khóa học đăng ký( bài giảng, giảng viên, mục đích, list khóa học tương đồng, comment/reply)
+ Danh sách tài liệu(của session) , bài tập, bài kiểm tra của khóa học
+ Chức năng nộp bài tập, bài kiểm tra
+ Xem kết quả chi tiết của khóa học.

**Trainer**
+ Quản lý khóa học (CRUD).
+ CRUD session cho khóa học
+ CRUD tài liệu/bài tập/ bài kiểm tra cho khóa học.
+ Xem danh sách trainee tham gia khóa học
+ CRUD kết quả của trainee.

**Admin**
+ Xem danh sách khóa học
+ CRUD lóp học.
+ Accept trainee tham gia khóa học.

**Manager**
+ Thống kế khóa học ( Tìm kiếm khóa học theo job, apartment, skill)
+ Tìm kiếm trainee theo skill/ theo kết quả đạo tạo.
