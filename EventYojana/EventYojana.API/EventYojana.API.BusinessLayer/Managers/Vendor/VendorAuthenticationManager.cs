using EventYojana.API.BusinessLayer.BusinessEntities.RequestModels.Vendor;
using EventYojana.API.BusinessLayer.BusinessEntities.ViewModel.Common;
using EventYojana.API.BusinessLayer.Interfaces.Vendor;
using EventYojana.API.DataAccess.DataEntities.Common;
using EventYojana.API.DataAccess.DataEntities.Vendor;
using EventYojana.API.DataAccess.Interfaces.Common;
using EventYojana.API.DataAccess.Interfaces.Vendor;
using EventYojana.Infrastructure.Core.Interfaces;
using System.Threading.Tasks;
using static EventYojana.Infrastructure.Core.Enum.SecurityEnum;

namespace EventYojana.API.BusinessLayer.Managers.Vendor
{
    public class VendorAuthenticationManager : IVendorAuthenticationManager
    {
        private readonly IVendorAuthenticationRepository _vendorAuthenticationRepository;
        private readonly IMessageSenderUtility _messageSenderUtility;
        private readonly ILoggingRepository _loggingRepository;
        public VendorAuthenticationManager(IVendorAuthenticationRepository vendorAuthenticationRepository, 
            IMessageSenderUtility messageSenderUtility,
            ILoggingRepository loggingRepository)
        {
            _vendorAuthenticationRepository = vendorAuthenticationRepository;
            _messageSenderUtility = messageSenderUtility;
            _loggingRepository = loggingRepository;
        }

        public async Task<PostResponseModel> RegisterVendor(RegisterVendorRequestModel vendorDetailsRequestModel)
        {
            PostResponseModel postResponseModel = new PostResponseModel();
            
            RegisterVendor registerUser = new RegisterVendor()
            {
                VendorName = vendorDetailsRequestModel.VendorName,
                VendorEmail = vendorDetailsRequestModel.VendorEmail,
                //UserType = (int)UserRoleEnum.Vendor.GetTypeCode(),
                VendorMobile = vendorDetailsRequestModel.VendorMobile,
                VendorLandline = vendorDetailsRequestModel.VendorLandline,
                AddressLine = vendorDetailsRequestModel.AddressLine,
                City = vendorDetailsRequestModel.City,
                State = vendorDetailsRequestModel.State,
                PinCode = vendorDetailsRequestModel.PinCode

                //PasswordSalt = AuthenticateUtility.CreatePasswordSalt()
            };

            //registerUser.Password = AuthenticateUtility.GeneratePassword(vendorDetailsRequestModel.Password, registerUser.PasswordSalt);
            var registerVendorResponse = await _vendorAuthenticationRepository.RegisterVendor(registerUser);

            postResponseModel.IsAlreadyExists = registerVendorResponse.IsUserExists;

            if(!postResponseModel.IsAlreadyExists && registerVendorResponse.Success)
            {
                var emailBody = "Hello !!!";
                var emailSubject = "Vendor Register";

                var emailResponse = await _messageSenderUtility.SendEmail(emailBody, emailSubject, vendorDetailsRequestModel.VendorEmail);
                postResponseModel.Success = emailResponse.IsEmailSend;

                //Log email
                EmailLogs emailLogDetails = new EmailLogs()
                {
                    FromEmailAddress = emailResponse.FromEmailAddress,
                    ToEmailAddress = emailResponse.ToEmailAddress,
                    Subject = emailSubject,
                    Body = emailBody,
                    IsProduction = emailResponse.IsProductionEnvironment,
                    IsSend = emailResponse.IsEmailSend,
                    ApplicationId = 1,
                    FromUserType = "System",
                    ToUserType = "Vendor",
                    ToUserId = registerVendorResponse.Content.VendorId
                };

                await _loggingRepository.LogEmailTransaction(emailLogDetails);
            }
            else
            {
                postResponseModel.Success = registerVendorResponse.Success;
            }

            return postResponseModel;
        }
    }
}
