using WhatsBack.SharedKernal.Enum;

namespace WhatsBack.SharedKernal.ResourcesReader.Messages
{
    public class MessageResourceReader : BaseFileReader, IMessageResourceReader
    {
        public MessageResourceReader() : base(LocalizationType.Message)
        {
        }

        public string Success(string culture) => GetKeyValue("Success", culture);
        public string GeneralError(string culture) => GetKeyValue("GeneralError", culture);
        public string InvalidRequest(string culture) => GetKeyValue("InvalidRequest", culture);
        public string InvalidName(string culture) => GetKeyValue("InvalidName", culture);
        public string InvalidPassword(string culture) => GetKeyValue("InvalidPassword", culture);
        public string InvalidPasswordFormat(string culture) => GetKeyValue("InvalidPasswordFormat", culture);
        public string MailAlreadyExist(string culture) => GetKeyValue("MailAlreadyExist", culture);
        public string InvalidPhoneNumber(string culture) => GetKeyValue("InvalidPhoneNumber", culture);
        public string InvalidBirthDate(string culture) => GetKeyValue("InvalidBirthDate", culture);
        public string RegisterSuccess(string culture) => GetKeyValue("RegisterSuccess", culture);
        public string InvalidCredentials(string culture) => GetKeyValue("InvalidCredentials", culture);
        public string InvalidLocation(string culture) => GetKeyValue("InvalidLocation", culture);
        public string NameAlreadyExist(string culture) => GetKeyValue("NameAlreadyExist", culture);
        public string PhoneAlreadyExist(string culture) => GetKeyValue("PhoneAlreadyExist", culture);
        public string NotDataFound(string culture) => GetKeyValue("NotDataFound", culture);
        public string InvalidUser(string culture) => GetKeyValue("InvalidUser", culture);
        public string VerifyAccount(string culture) => GetKeyValue("VerifyAccount", culture);
        public string ForgetPasswordMailSent(string culture) => GetKeyValue("ForgetPasswordMailSent", culture);
        public string ForgetPasswordSMSSent(string culture) => GetKeyValue("ForgetPasswordSMSlSent", culture);
        public string ForgetPasswordMailSubject(string culture) => GetKeyValue("ForgetPasswordMailSubject", culture);
        public string ForgetPasswordMailBody(string culture) => GetKeyValue("ForgetPasswordMailBody", culture);
        public string ForgetPasswordDataNotFoundOrExpired(string culture) => GetKeyValue("ForgetPasswordDataNotFoundOrExpired", culture);
        public string ResetPasswordSuccessMessage(string culture) => GetKeyValue("ResetPasswordSuccessMessage", culture);
        public string InvalidVerificationCode(string culture) => GetKeyValue("InvalidVerificationCode", culture);




        public string InvalidLanguage(string culture) => GetKeyValue("InvalidLanguage", culture);
        public string PasswordChanged(string culture) => GetKeyValue("PasswordChanged", culture);
        public string PasswordChangeError(string culture) => GetKeyValue("PasswordChangeError", culture);
        public string InvalidProfileFileExtention(string culture) => GetKeyValue("InvalidProfileFileExtention", culture);
        public string InvalidProfileFileSize(string culture) => GetKeyValue("InvalidProfileFileSize", culture);
        public string InvalidEmail(string culture) => GetKeyValue("InvalidEmail", culture);
        public string CommitFailed(string culture) => GetKeyValue("CommitFailed", culture);
    }
}
