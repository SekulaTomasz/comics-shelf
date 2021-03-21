using System;
using System.Collections.Generic;
using System.Text;

namespace comics_shelf_api.core.Consts
{
    public static class PasswordHasherError
    {
        public const string CANNOT_GENERATE_SALT = "Can not generate salt from an empty value.";
        public const string CANNOT_GENERATE_HASH = "Can not generate hash from an empty value.";
        public const string CANNOT_USE_EMPTY_SALT = "Can not use an empty salt from hashing value.";
    }

    public static class LoginError {
        public const string PASSWORD_INVALID = "Password is invalid";
    }

    public static class ComicsProviderError
    {
        public const string CANNOT_FETCH_COMICS = "Can not fetch comics. Try again later";
    }
    public static class PurchaseComicsServiceError {
        public const string CANNOT_FIND_PURCHASED_COMICS_WITH_ID = "Can not found comics with id {0}";
        public const string USER_HAVE_COMICS = "User have comics with id {0}";
        public const string CANNOT_RETURN_COMICS_WAS_BOUGHT_MORE_THAN_ONE_WEEK = "Can not return comics becouse was bougth more than one week";
    }
    public static class UserServiceError {
        public const string USER_NOT_FOUND = "Can not found user with id {0}";
    }

    public static class ComicsServiceErrror {
        public const string COMICS_NOT_FOUND = "Can not found comics with id {0}";
    }
}
