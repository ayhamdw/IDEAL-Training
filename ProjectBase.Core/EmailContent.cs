public static class EmailContent
{
    public static string GenerateEmailBody(string verificationUrl)
    {
        var body = $@"
<!DOCTYPE html>
<html lang='en'>
<head>
    <meta charset='UTF-8'>
    <title>Email Verification</title>
</head>
<body style='margin:0; padding:0; font-family:Segoe UI, Tahoma, Geneva, Verdana, sans-serif; background-color:#f4f4f4;'>
    <div style='max-width:600px; margin:0 auto; background-color:#ffffff; border-radius:8px; overflow:hidden; box-shadow:0 0 10px rgba(0,0,0,0.05);'>
        <div style='background-color:#4a6fa5; color:#ffffff; padding:20px; text-align:center;'>
            <h1 style='margin:0;'>Welcome to Ghadeer Future E-Commerce</h1>
        </div>
        <div style='padding:30px; color:#333333;'>
            <p style='font-size:16px;'>Hello there,</p>
            <p style='font-size:16px;'>Thank you for joining us! To complete your registration, please verify your email address by clicking the button below:</p>
            <div style='text-align:center; margin:30px 0;'>
                <a href='{verificationUrl}' style='display:inline-block; padding:12px 24px; background-color:#4a6fa5; color:#ffffff; text-decoration:none; border-radius:4px; font-weight:bold; font-size:16px;'>
                    Verify My Email
                </a>
            </div>
            <p style='font-size:14px;'>Or copy and paste this link into your browser:</p>
            <p style='word-break:break-all; font-size:14px; color:#4a6fa5;'><strong>{verificationUrl}</strong></p>
            <p style='font-size:14px; color:#555;'>This link will expire in 24 hours for your security.</p>
            <p style='font-size:14px;'>See you soon!</p>
            <p style='font-size:14px;'>– The Ghadeer Future E-Commerce Team</p>
        </div>
        <div style='padding:20px; text-align:center; font-size:12px; color:#999999; background-color:#f1f1f1;'>
            <p>If you didn't request this email, you can safely ignore it.</p>
        </div>
    </div>
</body>
</html>";
        return body;
    }
}
