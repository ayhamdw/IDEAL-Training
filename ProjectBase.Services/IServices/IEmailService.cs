﻿using Microsoft.AspNetCore.Mvc;

namespace ProjectBase.Services.IServices;

public interface IEmailService
{
    
   Task SendEmailAsync(string email, string subject, string body);
}