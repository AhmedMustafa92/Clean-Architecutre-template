namespace WhatsBack.SharedKernal.ResourcesReader.Messages
{
    public interface IMessageResourceReader
    {
        string Success(string culture);
        string GeneralError(string culture);
        string InvalidRequest(string culture);
        string InvalidName(string culture);
        string InvalidPassword(string culture);
        string InvalidPasswordFormat(string culture);
        string MailAlreadyExist(string culture);
        string InvalidPhoneNumber(string culture);
        string InvalidBirthDate(string culture);
        string InvalidEmail(string culture);
        string CommitFailed(string culture);
        string RegisterSuccess(string culture);
        string InvalidCredentials(string culture);
        string InvalidLocation(string culture);
        string NameAlreadyExist(string culture);
        string PhoneAlreadyExist(string culture);
        string NotDataFound(string culture);
        string InvalidUser(string culture);
        string VerifyAccount(string culture);
        string ForgetPasswordMailSent(string culture);
        string ForgetPasswordSMSSent(string culture);
        string ForgetPasswordMailSubject(string culture);
        string ForgetPasswordMailBody(string culture);
        string ForgetPasswordDataNotFoundOrExpired(string culture);
        string ResetPasswordSuccessMessage(string culture);
        string InvalidVerificationCode(string culture);



        string InvalidLanguage(string culture);
        string PasswordChanged(string culture);
        string PasswordChangeError(string culture);
        string InvalidProfileFileExtention(string culture);
        string InvalidProfileFileSize(string culture);

    }

}
