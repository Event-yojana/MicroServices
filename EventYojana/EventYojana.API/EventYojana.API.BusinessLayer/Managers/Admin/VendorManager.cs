using EventYojana.API.BusinessLayer.BusinessEntities.ViewModel.Common;
using EventYojana.API.BusinessLayer.BusinessEntities.ViewModel.Vendor;
using EventYojana.API.BusinessLayer.Interfaces.Admin;
using EventYojana.API.DataAccess.DataEntities.Common;
using EventYojana.API.DataAccess.Interfaces.Admin;
using EventYojana.API.DataAccess.Interfaces.Common;
using EventYojana.Infrastructure.Core.Helpers;
using EventYojana.Infrastructure.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventYojana.API.BusinessLayer.Managers.Admin
{
    public class VendorManager : IVendorManager
    {
        public readonly IVendorRepository _vendorRepository;
        private readonly IMessageSenderUtility _messageSenderUtility;
        private readonly ILoggingRepository _loggingRepository;

        public VendorManager(IVendorRepository vendorRepository, IMessageSenderUtility messageSenderUtility, ILoggingRepository loggingRepository)
        {
            _vendorRepository = vendorRepository;
            _messageSenderUtility = messageSenderUtility;
            _loggingRepository = loggingRepository;
        }
        public async Task<List<RegisteredVendorsResponseModel>> GetRegisteredVendorsList()
        {
            var vendorDetails = await _vendorRepository.GetRegisteredVendorList();

            return vendorDetails.ToList().Select(x => new RegisteredVendorsResponseModel() { 
                VendorId = x.VendorId,
                VendorName = x.VendorName,
                VendorEmail = x.VendorEmail,
                Mobile = x.Mobile,
                Landline = x.Landline,
                Address = x.FullAddress
            }).ToList();
        }
    
        public async Task<PostResponseModel> ConfirmRegistration(int vendorId)
        {
            PostResponseModel postResponseModel = new PostResponseModel();

            var passwordSalt = AuthenticateUtility.CreatePasswordSalt();
            var password = AuthenticateUtility.GetRandomAlphanumericString(8);
            var encryptedPassword = AuthenticateUtility.GeneratePassword(password, passwordSalt);

            var registerVendorResponse = await _vendorRepository.ConfirmRegistration(vendorId, encryptedPassword, passwordSalt);

            postResponseModel.IsAlreadyExists = registerVendorResponse.IsUserExists;
            if (!postResponseModel.IsAlreadyExists && registerVendorResponse.Success)
            {
                var emailBody = "Your password: " + password;
                var emailSubject = "Confirm Your Registration";

                var emailResponse = await _messageSenderUtility.SendEmail(emailBody, emailSubject, registerVendorResponse.Content.VendorEmail);
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
