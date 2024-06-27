﻿using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Options;
using PillPal.Application.Common.Interfaces.Auth;

namespace PillPal.Infrastructure.Auth;

public class FirebaseService : IFirebaseService
{
    public FirebaseService(IOptions<FirebaseSettings> settings)
    {
        FirebaseApp.Create(new AppOptions
        {
            Credential = GoogleCredential.FromJson(settings.Value.ServiceKey!)
        });
    }

    public async Task<string> GetEmailFromTokenAsync(string token)
    {
        var decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(token);
        return decodedToken.Claims["email"].ToString()!;
    }
}
