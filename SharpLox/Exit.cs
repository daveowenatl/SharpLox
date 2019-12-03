namespace SharpLox
{
    public static class Exit
    {
        /// <summary>
        /// The command was used incorrectly, e.g., with the
        /// wrong number of arguments, a bad flag, a bad syntax
        /// in a parameter, or whatever.
        /// </summary>
        public static int Usage => 64;

        /// <summary>
        /// The input data was incorrect in some way.  This
        /// should only be used for user's data and not system
        /// files.
        /// </summary>
        public static int DataError => 65;

        /// <summary>
        /// An input file (not a system file) did not exist or
        /// was not readable.  This could also include errors
        /// like ``No message'' to a mailer (if it cared to
        /// catch it).
        /// </summary>
        public static int NoInput => 66;

        /// <summary>
        /// The user specified did not exist.  This might be
        /// used for mail addresses or remote logins.
        /// </summary>
        public static int NoUser => 67;

        /// <summary>
        /// The host specified did not exist.  This is used in
        /// mail addresses or network requests.
        /// </summary>
        public static int NoHost => 68;

        /// <summary>
        /// A service is unavailable.  This can occur if a sup­
        /// port program or file does not exist.  This can also
        /// be used as a catchall message when something you
        /// wanted to do doesn't work, but you don't know why.
        /// </summary>
        public static int Unavailable => 69;

        /// <summary>
        /// An internal software error has been detected.  This
        /// should be limited to non-operating system related
        /// errors as possible.
        /// </summary>
        public static int Software => 70;

        /// <summary>
        /// An operating system error has been detected.  This
        /// is intended to be used for such things as ``cannot
        /// fork'', ``cannot create pipe'', or the like.  It
        /// includes things like getuid returning a user that
        /// does not exist in the passwd file.
        /// </summary>
        public static int OsError => 71;

        /// <summary>
        /// Some system file (e.g., /etc/passwd, /var/run/utmp,
        /// etc.) does not exist, cannot be opened, or has some
        /// sort of error (e.g., syntax error).
        /// </summary>
        public static int OsFile => 72;

        /// <summary>
        /// A (user specified) output file cannot be created.
        /// </summary>
        public static int CantCreate => 73;

        /// <summary>
        /// An error occurred while doing I/O on some file.
        /// </summary>
        public static int IoError => 74;

        /// <summary>
        /// Temporary failure, indicating something that is not
        /// really an error.  In sendmail, this means that a
        /// mailer (e.g.) could not create a connection, and
        /// the request should be reattempted later.
        /// </summary>
        public static int TempFail => 75;

        /// <summary>
        /// The remote system returned something that was ``not
        /// possible'' during a protocol exchange.
        /// </summary>
        public static int Protocol => 76;

        /// <summary>
        /// You did not have sufficient permission to perform
        /// the operation.  This is not intended for file sys­
        /// tem problems, which should use NoInput or
        /// CantCreate, but rather for higher level permissions.
        /// </summary>
        public static int NoPermission => 77;

        /// <summary>
        /// Something was found in an unconfigured or miscon­-
        /// figured state.
        /// </summary>
        public static int Config => 78;
    }
}