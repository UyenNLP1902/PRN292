## ADO.NET 

### Tutorial:
```
# Exporting json to SQLSERVER database
./exportJson2Database -j <<json_file>> -d <<database_name>>
Succesful: You've exported <<json_file>> to <<database_name>> in SQLSERVER
```

### Note:

```
You can change Server name (Data Source), User ID and Password in School.cs
Please put your json file in Release/netcoreapp3.1 folder
```
### Dựa vào dữ liệu đã được phát sinh từ lược đồ ER, hãy viết một ứng dụng dạng Command Line kết xuất (export) CSDL với chuẩn JSON vào hệ quản trị CSDL SQL Server

![ERD](https://raw.githubusercontent.com/tieuminh2510/SE1401-PRN292/master/LAB1/ERD.png)


### Các yêu cầu trong LAB
1. Chương trình dược viết trên mô hình OOP và được sử dụng yêu cầu sử dụng try, catch và exception để handle Error. 
2. Sử dụng công nghệ GIT để quản mã nguồn, ứng với mỗi hàm được viết mới hoặc cập nhật, học viên được yêu cầu phải thực hiện commit. Mã nguồn phải được xuất bản trên Github và lịch sử phát triển của mã nguồn phải thể hiện được quá trình liên tục. 
3. Ứng dụng cung cấp giao diện dòng lệnh cơ bản CLI, hổ trợ các cú pháp sau:

```
# Kết xuất json vào SQLSERVER database
./exportJson2Database -j <<json_file>> -d <<database_name>>
Succesful: You've exported <<json_file>> to <<database_name>> in SQLSERVER
```

4. Đầu ra của ứng dụng là **các bảng ** tương ứng các thông tin chứa trong file JSON.  

3. Mã nguồn buộc phải phát triển trên nền tảng .NET Core. Học viên phải hoàn tất việc release trên Visual Studio.
4. Với mỗi phương thức (method) và thuộc tính (property), lớp (class), học viên đều buộc phải viết ghi chú dưới dạng chuẩn XML (xem thêm tại https://docs.microsoft.com/en-us/dotnet/csharp/codedoc). Các tag yêu cầu bao gồm: param>, value>, summary>, returns>, exception.
5. Các ghi chú, tóm lược, điểm nỗi bật hoặc sáng tạo của trong project của học viên được trình bày trong file README.md bằng ngôn ngữ markdown.

**Deadline:** 23h00 26/06/2020. Sau deadline mọi commit sẽ không được xem xét. 

**NOTE**: Giảng viên sẽ loại những dự án không đảm bảo các yêu cầu kể trên. Điểm số xác định bằng số testcase mà ứng dụng của học viên có thể vượt qua (8 cases). Tất cả các source code được phần mềm phân tích là sao chép sẽ bị cấm thi. 