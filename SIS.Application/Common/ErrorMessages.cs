using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Application.Common
{
    public static class ErrorMessages
    {
        public const string DepartmentHasProgrammes = "Bu departamentə bağlı proqramlar mövcuddur, əvvəlcə onları silin";

        public const string UserNotFound = "İstifadəçi tapılmadı";

        public const string EmailAlreadyExists = "Bu email artıq qeydiyyatdadır";

        public const string StudentNotFound = "Tələbə tapılmadı";
        public const string StudentEmailExists = "Bu email artıq qeydiyyatdadır";

        
        public const string TeacherNotFound = "Müəllim tapılmadı";

        
        public const string CourseNotFound = "Kurs tapılmadı";
        public const string CourseCodeExists = "Bu kurs kodu artıq mövcuddur";

       
        public const string EnrollmentNotFound = "Qeydiyyat tapılmadı";
        public const string AlreadyEnrolled = "Tələbə bu kursa artıq qeydiyyatdadır";

        
        public const string GradeNotFound = "Qiymət tapılmadı";
        public const string GradeAlreadyExists = "Bu kurs üçün qiymət artıq mövcuddur";

        public const string AttendanceNotFound = "Davamiyyət qeydi tapılmadı";
        public const string AttendanceAlreadyRecorded = "Bu gün üçün davamiyyət artıq qeyd edilib";

   
        public const string AnnouncementNotFound = "Elan tapılmadı";

       
        public const string DepartmentNotFound = "Departament tapılmadı";

        public const string ProgrammeNotFound = "Proqram tapılmadı";

        
        public const string TermNotFound = "Semestr tapılmadı";
        public const string NoActiveTerm = "Aktiv semestr mövcud deyil";

        
        public const string InvalidCredentials = "Email və ya şifrə yanlışdır";
        public const string Unauthorized = "Bu əməliyyat üçün icazəniz yoxdur";
    }
}
