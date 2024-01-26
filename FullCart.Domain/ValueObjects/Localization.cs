namespace FullCart.Domain.ValueObjects
{
    public static class Localization
    {
        public static string ERROR_NOT_FOUND = "البيانات المطلوبة غير متوفرة";
        public static string INTERNAL_SERVER_ERROR = "عذرا عزيزي المستفيد، لم نتمكن من خدمتك بالشكل المطلوب";
        public static string ALREADY_EXISTS = "يوجد بيانات موجودة مسبقا بهذا الاسم {0}";
        public static string RELATED_DATA = "يوجد بيانات مرتبطة بهذا العنصر {0}";
        public static string WRONG_FILE_TYPE = "يجب أن يكون الملف من صيغة  xlsx";
        public static string LARGER_THAN_ALLOWED_SIZE = "حجم الملف يجب ان لا يتجاوز 4 ميجا بايت";
        public static string YEAR_GREATER_THAN = "يجب ان تكون السنة اكبر من {0}";
        public static string MONTH_VALIDATION = "ادخل قيمة صحيحة للشهر من 1 الى 12";
        public static string INVALID_COLUMNS_COUNT = "عدد الاعمدة غير صحيح";
        public static string REPORT_ALREADY_EXISTS = "يوجد {0} لشهر {1} عام {2}";
        public static string INVALID_CREDENTIALS = "اسم المستخدم غير صحيح ";
        public static string USER_UNAUTHORIZED = "لا يوجد لديك صلاحية";
        public static string BAD_REQUEST = "عزيزي المستفيد، يرجى التأكد من المدخلات بالشكل الصحيح";
        public static string NOT_AUTHORIZED = Localization.NOT_AUTHORIZED;
        public static string ERROR_NOT_FOUND_OR_APPROVED = "البيانات المطلوبة غير متوفرة غير معتمدة";
        public static string INVALID_ATTACHMENT = "حجم او صيغة المرفق لا يتوافق مع سياسة الموقع";
        public static string ATTACHMENT_UPLOAD_ERROR = "عزيزي المستخدم, تعذر خدمتكم لحدوث خطأ في حفظ الملفات";
        public static string ERROR_DUPLICATED_JOB = "لا يمكن تكرار الدور المختار";
        public static string ERROR_DUPLICATED_USER = "لا يمكن تكرار المستخدم";
        public static string ERROR_DUPLICATED_USER_INPROGRESSREQUESTS = "لا يمكن اضافة طلب نظرا لوجود طلب ";
    }
}
