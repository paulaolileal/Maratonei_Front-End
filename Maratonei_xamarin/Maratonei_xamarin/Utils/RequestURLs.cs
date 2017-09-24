using System;

namespace Maratonei_xamarin.Utils
{
    public static class RequestURLs
    {
        private const string BaseURL = "http://maratonei01.azurewebsites.net/";
        public static readonly string LoginURL = $"{BaseURL}api/Usuarios/Login";
        public static readonly string EditUserURL = $"{BaseURL}api/Usuarios/1";
        public static readonly string GetUserURL = $"{BaseURL}api/Usuarios/1";
        public static readonly string GetEnumerableUsersURL = $"{BaseURL}api/Usuarios/1";
        public static readonly string RegisterUserURL = $"{BaseURL}api/Usuarios/1";
    }
}