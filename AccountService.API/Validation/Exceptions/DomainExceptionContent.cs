namespace AccountService.API.Validation.Exceptions;

public class DomainExceptionContent
{
    public static ExceptionModel IdCannotBeEmpty = new(1001, "Id alanı boş geçilemez.");
    public static ExceptionModel FirstNameCannotBeEmpty = new(1002, "Ad alanı boş geçilemez.");
    public static ExceptionModel LastNameCannotBeEmpty = new(1003, "Soyad alanı boş geçilemez.");
    public static ExceptionModel EmailCannotBeEmpty = new(1004, "E-mail alanı boş geçilemez.");
    public static ExceptionModel BirthDateCannotBeEmpty = new(1005, "Doğum günü alanı boş geçilemez.");
    public static ExceptionModel PasswordCannotBeEmpty = new(1006, "Şifre alanı boş geçilemez.");
    public static ExceptionModel ReEnteredPasswordCannotBeEmpty = new(1007, "Şifre alanı boş geçilemez.");
    public static ExceptionModel PasswordMismatch = new(1008, "Şifreler uyuşmuyor.");
    public static ExceptionModel PasswordFormatIncorrect = new(1009, "Şifre formatı hatalı. Lütfen yeni şifre giriniz.");
    public static ExceptionModel BirthYearMustBeFourChars = new(1010, "Doğum yılınızı hatalı girdiniz.");
    public static ExceptionModel SocialSecurityNumberCannotBeEmpty = new(1011, "Kimlik numarası alanı boş geçilemez.");
    public static ExceptionModel SocialSecurityNumberMustBeElevenChars = new(1012, "Kimlik numarasızı hatalı girdiniz.");
    public static ExceptionModel SocialSecurityAlreadyUsed= new(1013, "Bu kimlik numarası daha önce kullanılmış.");
    public static ExceptionModel SocialSecurityNotValidated = new(1014, "Girdiğiniz kimlik numarası doğrulanamadı. Bilgilerinizi kontrol ediniz.");
    public static ExceptionModel AccountCannotCreated = new(1015, "Hesap yaratılamadı. Daha sonra tekrar deneyiniz.");
}
