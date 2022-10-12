namespace PolicyDetailsPdfGenerator.PersonPolicyService
{
    public interface IEmailService
    {
        bool SendEmail(byte[] streamResult);
    }
}
