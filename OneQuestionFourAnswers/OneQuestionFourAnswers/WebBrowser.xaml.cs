﻿using System.Windows;
using LoggingService;
using System;
using System.Web;
using System.Runtime.InteropServices;

namespace OneQuestionFourAnswers
{
    public partial class WebBrowser : Window
    {
        private enum InternetOptions
        {
            INTERNET_SUPPRESS_COOKIE_PERSIST = 3,
            INTERNET_OPTION_SUPPRESS_BEHAVIOR = 81,
            INTERNET_OPTION_END_BROWSER_SESSION = 42
        }

        private readonly MainWindowViewModel _vm;

        public WebBrowser()
        {
            InitializeComponent();
            _vm = (MainWindowViewModel)Application.Current.Resources["MainWindowVM"];
            Closing += (obj, args) => EndBrowserSession();
        }

        private void OnWebViewNavigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            try
            {
                var fix = new Uri(e.Uri.ToString().Replace("#", "?"));
                var query = fix.Query;
                GlobalLogger.Instance.Debug(fix.ToString());
                var dict = HttpUtility.ParseQueryString(query);
                if (Array.IndexOf(dict.AllKeys, "access_token") != -1)
                {
                    _vm.SaveToken(dict["access_token"], dict["user_id"]);
                    DialogResult = true;
                    _vm.ChangeSettings();
                    Close();
                    GlobalLogger.Instance.Info("Получен токен пользователя");
                }
                if (Array.IndexOf(dict.AllKeys, "error") != -1)
                {
                    DialogResult = false;
                    Close();
                    GlobalLogger.Instance.Info("Не удалось получить токен пользователя");
                }
            }
            catch (Exception error)
            {
                DialogResult = false;
                GlobalLogger.Instance.Error("Произошла ошибка " + error.Message + " при авторизации пользователя");
            }
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            SupressCookiePersist();
            WebView.Navigate(_vm.GetAuthUrl());
        }

        public static bool SupressCookiePersist()
        {
            return SetOption((int)InternetOptions.INTERNET_OPTION_SUPPRESS_BEHAVIOR, (int)InternetOptions.INTERNET_SUPPRESS_COOKIE_PERSIST);  
        }

        public static bool EndBrowserSession()
        {
            return SetOption((int)InternetOptions.INTERNET_OPTION_END_BROWSER_SESSION, null);
        }

        static bool SetOption(int settingCode, int? option)
        {
            IntPtr optionPtr = IntPtr.Zero;
            int size = 0;
            if (option.HasValue)
            {
                size = sizeof(int);
                optionPtr = Marshal.AllocCoTaskMem(size);
                Marshal.WriteInt32(optionPtr, option.Value);
            }

            bool success = InternetSetOption(0, settingCode, optionPtr, size);

            if (optionPtr != IntPtr.Zero) Marshal.Release(optionPtr);
            return success;
        }

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool InternetSetOption(
            int hInternet,
            int dwOption,
            IntPtr lpBuffer,
            int dwBufferLength
        );

    }
}
